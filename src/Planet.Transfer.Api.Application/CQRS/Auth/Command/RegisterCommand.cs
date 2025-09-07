using Common.Lib.Handler;

namespace Planet.Transfer.Api.Application.CQRS.Auth.Command
{
    public class RegisterCommand : IRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public class RegisterHandler : IRequestHandler<RegisterCommand>
        {
            public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await Task.CompletedTask;
                ;
            }
        }
    }
}
