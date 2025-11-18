using AuthServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Persistence.EntityMappings
{
    public class UserRefreshTokenMapping : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(urt => urt.UserId);
            builder.Property(urt => urt.Code).IsRequired().HasMaxLength(200);
        }
    }

}
