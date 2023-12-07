using CartCraft.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel.AspNetCore;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace CartCraft.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddControllersWithViews();
            services.AddHttpClient("CartCraftAPIClient", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration["CartCraftApiRoot"]);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application.json");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "CartCraft Client");
            }).AddUserAccessTokenHandler();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => 
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:5001/";
                options.ResponseType = "code";
                options.ClientId = "cartcraftclient";
                options.SaveTokens = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "given_name",
                    RoleClaimType = "role"
                };
                options.ClientSecret = "secret";
                options.Scope.Add("profile");
                options.Scope.Add("roles");
                options.Scope.Add("country");
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.Remove("aud");
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.DeleteClaim("s_hash");
                options.ClaimActions.MapJsonKey("role", "role");
                options.ClaimActions.MapUniqueJsonKey("country", "country");
                options.Scope.Add("cartcraftapi.read");
                options.Scope.Add("cartcraftapi.write");
                options.Scope.Add("offline_access");
                options.CallbackPath = new PathString("/signin-oidc");
                
                //options.Scope.Add("cartcraftapi.read");
                //options.Scope.Add("cartcraftapi.write");
            });

            services.AddAccessTokenManagement();

            services.AddAuthorization(options =>
                options.AddPolicy("UserCanAddItem", AuthorizationPolicies.CanAddItem()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
