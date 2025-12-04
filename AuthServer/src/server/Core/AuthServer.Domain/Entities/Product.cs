using AuthServer.Domain.Entities.Commons;

namespace AuthServer.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public User User { get; set; }
    }
}
