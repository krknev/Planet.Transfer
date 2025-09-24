namespace Planet.Transfer.Api.Application
{
    public static class ApplicationConstatns
    {
        public const string PrivateApiKey = "ApiKey";

        public const string MyTransferHttpClientName = "MyTransfersApi";
        public static string MyTransfer_API_KEY = "empty";

        public const string GeoApiCompleteHttpClientName = "GeoApiAutocomplete";
        public static string GeoApiComplete_API_KEY = "empty";

        public const string GeoApiSearchHttpClientName = "GeoApiSearch";
        public static string GeoApiSearch_API_KEY = "empty";

        public const string GoogleHttpClientName = "Google";
        public static string Google_API_KEY = "empty";

        public const string GoogleApisHttpClientName = "GoogleApis";
    }
    public class HttpClientsSettings : Dictionary<string, HttpClientSettings> { }
    public class HttpClientSettings
    {
        public string BaseAddress { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public Dictionary<string, string>? Headers { get; set; }
    }
}
