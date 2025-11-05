
using Common.Lib;
using Common.Lib.Handler;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
{
    public class RegisterCommand : IRequest<Common.Lib.Result>
    {

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool AgreeToTerms { get; set; }
        public bool SubscribeNewsletter { get; set; }

        public class RegisterHandler(IIdentityService identitySevice) : IRequestHandler<RegisterCommand, Common.Lib.Result>
        {
            public async Task<Common.Lib.Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await identitySevice.SignUpUserAsync(request, cancellationToken);
                    return Common.Lib.Result.Accept();
                }
                catch (Exception ex)
                {
                    return Common.Lib.Result.Fail(new Error("Something went wrong", ex.Message));
                }
            }
        }
    }
}
