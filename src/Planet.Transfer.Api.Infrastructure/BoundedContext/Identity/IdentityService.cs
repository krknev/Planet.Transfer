using Microsoft.AspNetCore.Identity;
using Planet.Transfer.Api.Application.CQRS.Auth;
using Planet.Transfer.Api.Application.CQRS.Auth.Command;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;
using Planet.Transfer.Api.Domain.BoundedContext.Identity;

namespace Planet.Transfer.Api.Infrastructure.BoundedContext.Identity
{
    internal class IdentityService(SignInManager<ApplicationUser> identityManager) : IIdentityService
    {
        public async Task<LoginResult> SignInUserAsync(LoginCommand request, CancellationToken cancellationToken)
        {

            var user = await identityManager.UserManager.FindByEmailAsync(request.Email!)
                ?? throw new NullReferenceException("User not found");

            var result = await identityManager.PasswordSignInAsync(user, request.Password!, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            //TOdo add jwt roles and claims into tokens
            // request remeberMe with unlimited lifetime
            var roles = await identityManager.UserManager.GetRolesAsync(user);
            return new LoginResult
            {
                Token = "id_token",
                User = new AppUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Name = user.UserName,
                    Avatar = "avatar",
                    Role = roles.FirstOrDefault() ?? "User",
                    IsOnline = true,
                    JoinDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),//Todo add audit entity
                }
            };

        }

        public async Task SignUpUserAsync(RegisterCommand request, CancellationToken cancellationToken)
        {
            await identityManager.UserManager.CreateAsync(new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                AgreeToTerms = request.AgreeToTerms,
                SubscribeNewsletter = request.SubscribeNewsletter,

            }, request.Password!);
        }
    }
}
