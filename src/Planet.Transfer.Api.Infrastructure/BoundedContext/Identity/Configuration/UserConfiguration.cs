using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planet.Transfer.Api.Domain.BoundedContext.Identity;

namespace Planet.Transfer.Api.Infrastructure.BoundedContext.Identity.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email).IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
