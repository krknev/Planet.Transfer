using Common.Lib.Endpoints;
using Common.Lib.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Queries;

namespace Planet.Transfer.Api.Web.Features.SuppliersEndpoints.Geo
{
    public class GeoEndpoints : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/supplier/geo")
                  .WithTags("Suppliers: Geo")
                  .WithOpenApi();

            group.MapGet("/complete", async (
                [AsParameters] GeoCompleteQuery request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(request, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Geo Complete")
                .WithDisplayName("Geo Complete")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Geo Api Complete")
                .WithDescription("");

            group.MapGet("/search", async (
                 [AsParameters] GeoSearchQuery request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(request, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Geo Search")
                .WithDisplayName("Geo Search")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Geo Api Search")
                .WithDescription("");
        }
    }
}
