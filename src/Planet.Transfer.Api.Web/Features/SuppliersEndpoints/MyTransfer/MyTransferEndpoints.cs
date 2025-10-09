using Common.Lib.Endpoints;
using Common.Lib.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.AvailabilityManagement.Queries;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.AvailabilityManagement.Results;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Commands;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Queries;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Results;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Destinations.Queries;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Destinations.Results;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Query;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Results;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Transports.Queries;
using Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Transports.Results;

namespace Planet.Transfer.Api.Web.Features.SuppliersEndpoints.MyTransfer
{
    public class MyTransferEndpoints : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/supplier/mytransfer")
                  .WithTags("Suppliers: My Transfer")
                  .WithOpenApi();

            group.MapPost("/availability", async (
                        [FromBody] MyTransferAvailabilityQuery query,
                        IMediator mediator,
                        CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(query, cancellationToken);
                        return Results.Ok(result);
                    })
                        .WithName("MyTransfer Availability management")
                        .WithDisplayName("My Transfer Availability management")
                        .Accepts<MyTransferAvailabilityQuery>("application/json")
                        .Produces<MyTransferAvailabilityResult>(StatusCodes.Status200OK)
                        .ProducesProblem(StatusCodes.Status400BadRequest)
                        .WithSummary("MyTransfer Availability management.")
                        .WithDescription(
                        " Example values:" +
                        "{  \"adults\": 3,\"childs\": 0,\"destinationLat\": 51.6074,\"destinationLng\":  0.1478,\"originLat\": 51.6074,\"originLng\":0.1678,\"pickupDate\":\"2025-08-06T12:06\"}");

            group.MapGet("/extras", async (
                              IMediator mediator,
                             CancellationToken cancellationToken) =>
                      {
                          var result = await mediator.Send(new MyTransferExtrasQuery(), cancellationToken);
                          return Results.Ok(result);
                      })
                        .WithName("GetExtras")
                        .WithDisplayName("MyTransfer Extras")
                        .Produces<MyTransferExtrasResult>(StatusCodes.Status200OK)
                        .ProducesProblem(StatusCodes.Status500InternalServerError)
                        .WithSummary("Fetches available extras from MyTransfer.")
                        .WithDescription("Calls extras endpoint from MyTransfer API.");

            group.MapGet("/destinations", async (
                             IMediator mediator,
                            CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(new MyTransferDestinationsQuery(), cancellationToken);
                        return Results.Ok(result);
                    })
                       .WithName("GetDestinations")
                       .WithDisplayName("MyTransfer Destiantions")
                       .Produces<MyTransferDestinationsResult>(StatusCodes.Status200OK)
                       .ProducesProblem(StatusCodes.Status500InternalServerError)
                       .WithSummary("Fetches available destinations from MyTransfer.")
                       .WithDescription("Calls destination endpoint from MyTransfer API.");

            group.MapGet("/transports", async (
                            IMediator mediator,
                           CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(new MyTransferTransportsQuery(), cancellationToken);
                        return Results.Ok(result);
                    })
                      .WithName("GetTransports")
                      .WithDisplayName("MyTransfer Transports")
                      .Produces<MyTransferTransportsResult>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status500InternalServerError)
                      .WithSummary("Fetches available Transports from MyTransfer.")
                      .WithDescription("Calls Transports endpoint from MyTransfer API.");

            group.MapGet("/booking/{bookingId}", async (
                          [FromRoute] int bookingId,
                          IMediator mediator,
                          CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(new MyTransferBookingBuIdQuery() { BookingId = bookingId }, cancellationToken);
                        return Results.Ok(result);
                    })
                     .WithName("Get Booking By Id")
                     .WithDisplayName("MyTransfer BookingById")
                     .Produces<MyTransferBookingBuIdResult>(StatusCodes.Status200OK)
                     .ProducesProblem(StatusCodes.Status500InternalServerError)
                     .WithSummary("Fetches available BookingById from MyTransfer.")
                     .WithDescription("Calls BookingById endpoint from MyTransfer API.");

            group.MapDelete("/booking/{bookingId}", async (
                         [FromRoute] int bookingId,
                         IMediator mediator,
                         CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(new MyTransferDeleteBookingBuIdCommand() { BookingId = bookingId }, cancellationToken);
                        return Results.Ok(result);
                    })
                    .WithName("Delete Booking By Id")
                    .WithDisplayName("MyTransfer Delete BookingById")
                    .Produces<MyTransferBookingBuIdResult>(StatusCodes.Status200OK)
                    .ProducesProblem(StatusCodes.Status500InternalServerError)
                    .WithSummary("Fetches delete available BookingById from MyTransfer.")
                    .WithDescription("Calls delete BookingById endpoint from MyTransfer API.");

            group.MapPost("/booking ", async (
                        [FromBody] MyTransferCreateBookingCommand request,
                        IMediator mediator,
                        CancellationToken cancellationToken) =>
                    {
                        var result = await mediator.Send(request, cancellationToken);
                        return Results.Ok(result);
                    })
                   .WithName("Create Booking By Id")
                   .WithDisplayName("MyTransfer Create Booking")
                   .Produces<MyTransferBookingBuIdResult>(StatusCodes.Status200OK)
                   .ProducesProblem(StatusCodes.Status500InternalServerError)
                   .WithSummary("Fetches create new Booking from MyTransfer.")
                   .WithDescription("Calls create Booking endpoint from MyTransfer API.");
        }
    }
}
