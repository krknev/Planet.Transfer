using Common.Lib.Endpoints;
using Common.Lib.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Google.MapDirections.Queries;

namespace Planet.Transfer.Api.Web.Features.SuppliersEndpoints.MyTransfer
{
    public class GoogleEndpoints : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/supplier/google/")
                  .WithTags("Suppliers: Google")
                  .WithOpenApi();

            group.MapGet("/maps/directions", async (
                [FromQuery] string? origin,
                [FromQuery] string? destination,
                [FromQuery] string? mode,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GoogleMapDirectionsQuery()
                {
                    Origin = origin,
                    Destination = destination,
                    Mode = mode
                }, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Google maps destiantions")
                .WithDisplayName("Google maps destiantions")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Google maps destiantions")
                .WithDescription(
                " Example values:" +
                "\"Origin\": \"42.13524037977961, 24.743697294910444\"," +
                "\"destination\": \"42.42640873834561,25.634309984958822\"," +
                "\"mode\":\"driving\"}");

        }
    }
}
