using Common.Lib;
using Common.Lib.Handler;

namespace Five.Best.Api.Application.CQRS.Auth.Command
{
    public class ConfirmLoginCommand : IRequest<Result<FullLoginResult>>
    {
        public string? ConfirmCode { get; set; }

        public class ConfirmLoginHandler : IRequestHandler<ConfirmLoginCommand, Result<FullLoginResult>>
        {
            public async Task<Result<FullLoginResult>> Handle(ConfirmLoginCommand request, CancellationToken cancellationToken)
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
