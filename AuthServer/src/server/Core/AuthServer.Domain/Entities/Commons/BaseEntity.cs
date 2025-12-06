namespace AuthServer.Domain.Entities.Commons
{
    public abstract class BaseEntity : IIsActive, ICreated, IUpdated
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
