using Common.Lib;
using Common.Lib.Handler;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Destinations.Results;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Results;

namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Destinations.Queries
{
    public class MyTransferDestinationsQuery : IRequest<Result<MyTransferDestinationsResult>>
    {
        public class MyTransferDestinationsHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<MyTransferDestinationsQuery, Result<MyTransferDestinationsResult>>
        {
            private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApplicationConstatns.MyTransferHttpClientName);

            public async Task<Result<MyTransferDestinationsResult>> Handle(MyTransferDestinationsQuery request, CancellationToken cancellationToken)
            {

                var endpoint = $"{ApplicationConstatns.MyTransfer_API_KEY}/destinations";
                var response = await _httpClient.GetAsync(endpoint);
                //    response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyTransferDestination>>(content)
                    ?? throw new AggregateException($"Unable desialize {nameof(MyTransferExtrasResult)}");
                return Result<MyTransferDestinationsResult>.Success(new MyTransferDestinationsResult { Destiantions = resultValue });
            }
        }
    }
}
