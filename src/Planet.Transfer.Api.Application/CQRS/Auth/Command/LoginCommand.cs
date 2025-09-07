using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
{
    public class LoginCommand : IRequest<Result<LoginResult>>
    {
        public string? Username { get; set; }

        public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResult>>
        {
            public async Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await Task.CompletedTask;
                return Result<LoginResult>.Success(new LoginResult() { IdToken = Guid.NewGuid().ToString(), ExpiresAt = DateTime.UtcNow.AddMinutes(3) });
            }
        }
    }
}
