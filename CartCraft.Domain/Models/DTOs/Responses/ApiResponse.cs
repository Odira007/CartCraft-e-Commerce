using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Models.DTOs.Responses
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static ApiResponse Success(string message) =>
            new ApiResponse() { Status = true, Message = message };

        public static ApiResponse Failure(string message) =>
            new ApiResponse() { Status = false, Message = message };
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public static ApiResponse<T> Success(T data, string message) =>
            new ApiResponse<T>() { Status = true, Message = message, Data = data };
    }
}
