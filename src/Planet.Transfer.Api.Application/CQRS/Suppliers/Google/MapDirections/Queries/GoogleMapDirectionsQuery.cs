using Common.Lib;
using Common.Lib.Handler;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Google.MapDirections.Queries
{
    public class GoogleMapDirectionsQuery : IRequest<Result<string>>
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Mode { get; set; }
        public class GoogleMapDirectionsHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GoogleMapDirectionsQuery, Result<string>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.GoogleHttpClientName);

            public async Task<Result<string>> Handle(GoogleMapDirectionsQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"maps/embed/v1/directions?key={ApplicationConstatns.Google_API_KEY}";

                if (!string.IsNullOrEmpty(request.Origin))
                {
                    endpoint += $"&origin={request.Origin}";
                }

                if (!string.IsNullOrEmpty(request.Destination))
                {
                    endpoint += $"&destination={request.Destination}";
                }

                if (!string.IsNullOrEmpty(request.Mode))
                {
                    endpoint += $"&mode={request.Mode}";
                }
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
