namespace Five.Best.Api.Application.CQRS.Auth
{
    public class FullLoginResult //: LoginResult
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
