using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
{
    public class LoginCommand : IRequest<Result<LoginResult>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }

        public class LoginHandler(IIdentityService identityService) : IRequestHandler<LoginCommand, Result<LoginResult>>
        {
            public async Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await identityService.SignInUserAsync(request, cancellationToken);

                    return Result<LoginResult>.Success(result);
                }
                catch (Exception ex)
                {
                    return Result<LoginResult>.Failure(new Error("Unauthorized", ex.Message));
                }
            }
        }
    }
}
