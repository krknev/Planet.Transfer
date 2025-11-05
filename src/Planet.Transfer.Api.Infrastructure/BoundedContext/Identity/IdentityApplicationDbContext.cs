using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Planet.Transfer.Api.Domain.BoundedContext.Identity;
using Planet.Transfer.Api.Infrastructure.BoundedContext.Identity.Configuration;

namespace Planet.Transfer.Api.Infrastructure.BoundedContext.Identity
{
    internal class IdentityApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
