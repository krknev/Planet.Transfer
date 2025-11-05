using Common.Lib;
using Common.Lib.Handler;

namespace Five.Best.Api.Application.CQRS.Auth.Command
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
                    Token = Guid.NewGuid().ToString(),
                    RefreshToken = Guid.NewGuid().ToString(),
                });
            }
        }
    }
}
