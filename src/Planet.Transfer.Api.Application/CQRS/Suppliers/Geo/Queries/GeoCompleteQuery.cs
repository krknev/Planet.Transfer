using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Queries
{
    public class GeoCompleteQuery : IRequest<Result<GeoCompleteResult>>
    {
        public string? Text { get; set; }
        public string? Type { get; set; }
        public string? Filter { get; set; }
        public class GeoCompleteHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GeoCompleteQuery, Result<GeoCompleteResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.GeoApiCompleteHttpClientName);

            public async Task<Result<GeoCompleteResult>> Handle(GeoCompleteQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"?format=json&apiKey={ApplicationConstatns.GeoApiSearch_API_KEY}";
                if (!string.IsNullOrWhiteSpace(request.Text))
                {
                    endpoint = endpoint + $"&text={request.Text}";
                }
                if (!string.IsNullOrWhiteSpace(request.Type))
                {
                    endpoint = endpoint + $"&type={request.Type}";
                }
                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    endpoint = endpoint + $"&filter={request.Filter}";
                }

                var response = await _httpClient.GetAsync(endpoint);

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GeoCompleteResult>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(GeoCompleteResult)}");
                return Result<GeoCompleteResult>.Success(resultValue);
            }
        }
    }
}
