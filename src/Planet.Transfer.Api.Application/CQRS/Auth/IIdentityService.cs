using Planet.Transfer.Api.Application.CQRS.Auth.Command;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.CQRS.Auth
{
    public interface IIdentityService
    {
        Task<LoginResult> SignInUserAsync(LoginCommand request, CancellationToken cancellationToken);
        Task SignUpUserAsync(RegisterCommand request, CancellationToken cancellationToken);
    }
}
