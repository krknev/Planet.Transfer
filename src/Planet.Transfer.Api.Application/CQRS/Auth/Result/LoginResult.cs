namespace Planet.Transfer.Api.Application.CQRS.Auth.Result
{
    public class LoginResult
    {
        public required string Token { get; set; }
        public required AppUserDTO User { get; set; }
    }

    public class AppUserDTO
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public bool IsOnline { get; set; }
        public string? JoinDate { get; set; }
        public string? Speciality { get; set; }
        public int? Rating { get; set; }
        public int? Artworks { get; set; }
        public int? Followers { get; set; }
        public int? Purchases { get; set; }
        public int? TotalSpent { get; set; }
        public string? Balance { get; set; }
        public decimal? Usd { get; set; }
        public decimal? Glory { get; set; }
        public decimal? Usdc { get; set; }
        public decimal? Sol { get; set; }
    };
}
