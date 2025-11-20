namespace AuthServer.Domain.Entities.Commons
{
    public abstract class BaseEntity : IIsActive, ICreated, IUpdated
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
