namespace RunningJac.IDP.Entities
{
    public class UserClaim : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}