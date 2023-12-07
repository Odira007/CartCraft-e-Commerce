namespace RunningJac.IDP.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}