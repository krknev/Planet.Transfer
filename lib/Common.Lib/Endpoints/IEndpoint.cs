using Microsoft.AspNetCore.Routing;

namespace Common.Lib.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
