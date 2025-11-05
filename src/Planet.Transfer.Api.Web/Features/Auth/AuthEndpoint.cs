using Common.Lib.Endpoints;
using Common.Lib.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Planet.Transfer.Api.Application.CQRS.Auth.Command;
using Planet.Transfer.Api.Application.CQRS.Auth.Result;

namespace Planet.Transfer.Api.Application.Features.Auth
{
    public class AuthEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth")
                  .WithTags("Auth")
                  .WithOpenApi();

            //group.MapPost("/signin/confirm", async (
            //    [FromBody] ConfirmLoginCommand command,
            //    IMediator mediator,
            //    HttpContext httpContext,
            //    CancellationToken cancellationToken) =>
            //{
            //    var result = await mediator.Send(command, cancellationToken);
            //    return Results.Ok(result);
            //})
            //    .WithName("Confirm Login")
            //    .WithDisplayName("Confirm Login")
            //    .Produces<FullLoginResult>(StatusCodes.Status200OK)
            //    .ProducesProblem(StatusCodes.Status400BadRequest)
            //    .WithSummary("Confirms a login via code")
            //    .WithDescription("Endpoint to confirm user login via email code.");

            group.MapPost("/signin", async (
                [FromBody] LoginCommand command,
                IMediator mediator,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return Results.Ok(result);
            })
                .WithName("Login")
                .WithDisplayName("Login")
                .Produces<LoginResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Login via Phone number or email")
                .WithDescription("Endpoint to login user by email or phone number.");

            //group.MapPost("/signin/refresh", async (
            //   [FromBody] RefreshLoginCommand command,
            //   IMediator mediator,
            //   HttpContext httpContext,
            //   CancellationToken cancellationToken) =>
            //{
            //    var result = await mediator.Send(command, cancellationToken);
            //    return Results.Ok(result);
            //})
            //   .WithName("Refresh Login")
            //   .WithDisplayName("Refresh Login")
            //   .Produces<FullLoginResult>(StatusCodes.Status200OK)
            //   .ProducesProblem(StatusCodes.Status400BadRequest)
            //   .WithSummary("Refresh Login")
            //   .WithDescription("Endpoint to Refresh login user.");

            group.MapPost("/signup", async (
                  [FromBody] RegisterCommand command,
                  IMediator mediator,
                  HttpContext httpContext,
                  CancellationToken cancellationToken) =>
                {
                    await mediator.Send(command, cancellationToken);
                    return Results.Ok();
                })
                   .WithName("Register")
                   .WithDisplayName("Register")
                   .Produces(StatusCodes.Status200OK)
                   .ProducesProblem(StatusCodes.Status400BadRequest)
                   .WithSummary("Register")
                   .WithDescription("Endpoint to Register user.");
        }
    }
}
