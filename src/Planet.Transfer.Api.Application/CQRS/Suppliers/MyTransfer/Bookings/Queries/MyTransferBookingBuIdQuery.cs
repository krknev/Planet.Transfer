using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Queries
{
    public class MyTransferBookingBuIdQuery : IRequest<Result<MyTransferBookingBuIdResult>>
    {
        public int BookingId { get; set; }
        public class MyTransferBookingBuIdHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferBookingBuIdQuery, Result<MyTransferBookingBuIdResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferBookingBuIdResult>> Handle(MyTransferBookingBuIdQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/bookings/{request.BookingId}";
                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTransferBookingBuIdResult>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferBookingBuIdResult)}");
                return Result<MyTransferBookingBuIdResult>.Success(resultValue);
            }
        }
    }
}
