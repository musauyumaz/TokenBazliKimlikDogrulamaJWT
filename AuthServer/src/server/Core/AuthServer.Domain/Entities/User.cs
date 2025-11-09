using AuthServer.Domain.Entities.Commons;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthServer.Domain.Entities
{
    public sealed class User : IdentityUser, IIsActive, ICreated, IUpdated
    {
        public string City { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
