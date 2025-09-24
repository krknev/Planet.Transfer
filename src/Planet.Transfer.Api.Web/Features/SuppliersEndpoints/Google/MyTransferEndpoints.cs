using Common.Lib.Endpoints;
using Common.Lib.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Google.MapAutocomplete;
using Planet.Transfer.Api.Application.CQRS.Suppliers.Google.MapDetails;
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

            group.MapGet("/maps/autocomplete", async (
                [FromQuery] string? input,
                [FromQuery] string? language,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GoogleMapAutocompleteQuery()
                {
                    Input = input,
                    Language = language
                }, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Google maps autocomplete")
                .WithDisplayName("Google maps autocomplete")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Google maps autocomplete")
                .WithDescription(
                " Example values:" +
                "\"Input\": \"YOUR_QUERY\"," +
                "\"Language\":\"en\"}");

            group.MapGet("/maps/details", async (
                [FromQuery] string? placeId,
                [FromQuery] string? language,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GoogleMapDetailQuery()
                {
                    PlaceId = placeId,
                    Language = language
                }, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Google maps details")
                .WithDisplayName("Google maps details")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Google maps details")
                .WithDescription(
                " Example values:" +
                "\"placeId\": \"PLACE_ID\"," +
                "\"Language\":\"en\"}");
        }
    }
}
