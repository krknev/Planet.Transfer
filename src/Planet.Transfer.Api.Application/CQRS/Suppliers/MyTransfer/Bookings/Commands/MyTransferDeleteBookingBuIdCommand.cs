using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Commands
{
    public class MyTransferDeleteBookingBuIdCommand : IRequest<Result<MyTransferBookingBuIdResult>>
    {
        public int BookingId { get; set; }
        public class MyTransferDeleteBookingBuIdHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferDeleteBookingBuIdCommand, Result<MyTransferBookingBuIdResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferBookingBuIdResult>> Handle(MyTransferDeleteBookingBuIdCommand request, CancellationToken cancellationToken)
            {
                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/bookings/{request.BookingId}";
                var response = await _httpClient.DeleteAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTransferBookingBuIdResult>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferBookingBuIdResult)}");
                return Result<MyTransferBookingBuIdResult>.Success(resultValue);
            }
        }
    }
}
