using Common.Lib;
using Common.Lib.Handler;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Google.MapAutocomplete
{
    public class GoogleMapAutocompleteQuery : IRequest<Result<string>>
    {
        public string? Input { get; set; }
        public string? Language { get; set; } = "en";
        public class GoogleMapAutocompleteHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GoogleMapAutocompleteQuery, Result<string>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.GoogleApisHttpClientName);

            public async Task<Result<string>> Handle(GoogleMapAutocompleteQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"maps/api/place/autocomplete/json?key={ApplicationConstatns.Google_API_KEY}";
                if (!string.IsNullOrEmpty(request.Input))
                {
                    endpoint += $"&input={request.Input}";
                }

                if (!string.IsNullOrEmpty(request.Language))
                {
                    request.Language = "en";
                }
                endpoint += $"&Language={request.Language}";
                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                //var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTransferBookingBuIdResult>(content)
                //    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferBookingBuIdResult)}");
                return Result<string>.Success(content ?? string.Empty);
            }
        }
    }
}
