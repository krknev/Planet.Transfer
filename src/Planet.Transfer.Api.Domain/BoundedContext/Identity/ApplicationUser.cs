using Microsoft.AspNetCore.Identity;

namespace Planet.Transfer.Api.Domain.BoundedContext.Identity
{
    public class ApplicationUser : IdentityUser
    {
        //public int AccountId { get; set; }
        //public Account? Account { get; set; }

        public bool SubscribeNewsletter { get; set; }
        public bool AgreeToTerms { get; set; }
    }
}
