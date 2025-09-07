using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
{
    public class RefreshLoginCommand : IRequest<Result<FullLoginResult>>
    {
        public string? RefreshToken { get; set; }

        public class RefreshLoginHandler : IRequestHandler<RefreshLoginCommand, Result<FullLoginResult>>
        {
            public async Task<Result<FullLoginResult>> Handle(RefreshLoginCommand request, CancellationToken cancellationToken)
            {
                await Task.CompletedTask;
                return Result<FullLoginResult>.Success(new FullLoginResult()
                {
                    IdToken = Guid.NewGuid().ToString(),
                    RefreshToken = Guid.NewGuid().ToString(),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(3)
                });
            }
        }
    }
}
