namespace Planet.Transfer.Api.Application.CQRS.Auth.Result
{
    public class LoginResult
    {
        public required DateTime ExpiresAt { get; set; }
        public required string IdToken { get; set; }
    }
}
