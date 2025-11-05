using Microsoft.AspNetCore.Identity;
using Planet.Transfer.Api.Domain.Common.Enums;

namespace Planet.Transfer.Api.Domain.BoundedContext.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public UserSubscription Subscription { get; set; }
    }
}
