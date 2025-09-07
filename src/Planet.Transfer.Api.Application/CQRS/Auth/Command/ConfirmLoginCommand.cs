using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
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
                    IdToken = Guid.NewGuid().ToString(),
                    RefreshToken = Guid.NewGuid().ToString(),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(3)
                });
            }
        }
    }
}
