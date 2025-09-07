using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Queries
{
    public class GeoSearchQuery : IRequest<Result<GeoSearchResult>>
    {
        public string? Text { get; set; }
        public string? Filter { get; set; }

        public class GeoSearchHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GeoSearchQuery, Result<GeoSearchResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.GeoApiSearchHttpClientName);

            public async Task<Result<GeoSearchResult>> Handle(GeoSearchQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"?format=json&apiKey={ApplicationConstatns.GeoApiSearch_API_KEY}";
                if (!string.IsNullOrWhiteSpace(request.Text))
                {
                    endpoint = endpoint + $"&text={request.Text}";
                }
                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    endpoint = endpoint + $"&filter={request.Filter}";
                }
                var response = await _httpClient.GetAsync(endpoint);

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GeoSearchResult>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(GeoSearchResult)}");
                return Result<GeoSearchResult>.Success(resultValue);
            }
        }
    }
}
