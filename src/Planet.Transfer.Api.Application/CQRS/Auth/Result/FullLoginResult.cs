namespace Planet.Transfer.Api.Application.CQRS.Auth.Result
{
    public class FullLoginResult : LoginResult
    {
        public string RefreshToken { get; set; } = default!;
    }
}
