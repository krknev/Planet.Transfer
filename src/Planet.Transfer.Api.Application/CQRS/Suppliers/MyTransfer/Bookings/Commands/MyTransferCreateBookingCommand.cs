using Common.Lib;
using Common.Lib.Handler;
using System.ComponentModel.DataAnnotations;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Commands
{
    //  public class MyTransferCreateBookingCommand : IRequest<Result<MyTransferBookingBuIdResult>>
    public class MyTransferCreateBookingCommand : IRequest<Result<string>>
    {
        public string? ArrivalLine { get; set; }
        public string? ArrivalLocator { get; set; }
        public DataType? ArrivalPickUpTime { get; set; }
        public required string CustomerCountry { get; set; }

        [EmailAddress]
        public required string CustomerEmail { get; set; }
        public required string CustomerFirstName { get; set; }
        public required string CustomerLastName { get; set; }
        public required string CustomerPhone { get; set; }
        public string? DepartureLine { get; set; }
        public string? DepartureLocator { get; set; }
        public DateTime? DeparturePickUpTime { get; set; }
        public string? DestinationAddress { get; set; }
        public required string DestinationType { get; set; }
        public ICollection<MyTransferExtra> Extrass { get; set; } = [];
        public DateTime? FinalPickupDate { get; set; }
        public required string OriginType { get; set; }
        public string? OriginAddress { get; set; }
        public string? PickupExternalReference { get; set; }
        public required string SessionId { get; set; }
        public string? SpecialRequirements { get; set; }
        public required string TransferId { get; set; }

        public class MyTransferCreateBookingHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferCreateBookingCommand, Result<string>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<string>> Handle(MyTransferCreateBookingCommand request, CancellationToken cancellationToken)
            {
                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/bookings";
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                var httpContent = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, httpContent);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                //var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(content)
                //?? throw new AggregateException($"Unable desialize {nameof(MyTransferBookingBuIdResult)}");
                return Result<string>.Success(content);
            }
        }
    }

    public class MyTransferExtra
    {
        public int? ExtraId { get; set; }
        public decimal? Amount { get; set; }
    }
}
