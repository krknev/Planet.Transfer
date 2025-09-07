using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Transports.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Transports.Queries
{
    public class MyTransferTransportsQuery : IRequest<Result<MyTransferTransportsResult>>
    {
        public class MyTransferTransportHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferTransportsQuery, Result<MyTransferTransportsResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferTransportsResult>> Handle(MyTransferTransportsQuery request, CancellationToken cancellationToken)
            {
                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/destinations";
                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyTransferTransport>>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferTransportsResult)}");
                return Result<MyTransferTransportsResult>.Success(new MyTransferTransportsResult { Transports = resultValue });
            }
        }
    }
}
