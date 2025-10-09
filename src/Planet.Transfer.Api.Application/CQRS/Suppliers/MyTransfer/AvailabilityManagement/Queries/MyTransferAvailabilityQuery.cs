using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.AvailabilityManagement.Results;
using System.Globalization;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.AvailabilityManagement.Queries
{
    /// <summary>
    /// Request object for MyTransfer availability.
    /// </summary>
    public class MyTransferAvailabilityQuery : IRequest<Result<MyTransferAvailabilityResult>>
    {
        public required int Adults { get; set; } = 0;
        public int? Childs { get; set; } = 0;

        /// <summary>
        /// Latitude of the destination. Example Value:51.6074
        /// </summary>
        public required double DestinationLat { get; set; } = 51.6074;

        /// <summary>Longitude of the destination. Example Value: 0.1378</summary>
        public required double DestinationLng { get; set; } = 0.1378;
        public int? Infants { get; set; } = null;

        /// <summary>Latitude of the origin. Example Value:51.5074 </summary>
        public required double OriginLat { get; set; } = 51.5074;

        /// <summary>Longitude of the origin. Example Value:0.1278 </summary>
        public required double OriginLng { get; set; } = 0.1278;
        public DateTime PickupDate { get; set; }
        public string? PromoCode { get; set; }
        public bool? ОneWay { get; set; }

        public class MyTransferAvailabilityHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferAvailabilityQuery, Result<MyTransferAvailabilityResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferAvailabilityResult>> Handle(MyTransferAvailabilityQuery request, CancellationToken cancellationToken)
            {
                string formattedDate = request.PickupDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                string encodedDate = Uri.EscapeDataString(formattedDate);

                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/availabilities" +
                     $"?adults={request.Adults}&childs={request.Childs}" +
                     $"&destinationLat={request.DestinationLat}&destinationLng={request.DestinationLng}" +
                     $"&infants={request.Infants}" +
                     "&lang=EN" +
                      $"&оneWay={request.ОneWay}" +
                     $"&originLat={request.OriginLat}&originLng={request.OriginLng}" +
                     $"&pickupDate={encodedDate}";
                if (!string.IsNullOrWhiteSpace(request.PromoCode))
                {
                    endpoint = endpoint + $"&promoCode={request.PromoCode}";
                }

                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTransferAvailabilityResult>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferAvailabilityResult)}");
                return Result<MyTransferAvailabilityResult>.Success(resultValue);
            }


        }
    }
}
