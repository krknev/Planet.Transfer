using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Query
{
    public class MyTransferExtrasQuery : IRequest<Result<MyTransferExtrasResult>>
    {
        public class MyTransferExtrasHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferExtrasQuery, Result<MyTransferExtrasResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferExtrasResult>> Handle(MyTransferExtrasQuery request, CancellationToken cancellationToken)
            {

                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/extras";
                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyTransfersExtra>>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferExtrasResult)}");
                return Result<MyTransferExtrasResult>.Success(new MyTransferExtrasResult { Extra = resultValue });
            }
        }
    }
}
