using Core.Application.Abstractions;
using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.DTOs;
using Orders.Application.Handlers.Commands.CancelOrder;
using Orders.Application.Handlers.Commands.CreateOrder;
using Orders.Application.Handlers.Commands.UpdateOrder;
using Orders.Application.Handlers.Commands.UpdateOrderStatus;
using Orders.Application.Handlers.Queries.GetOrder;
using Orders.Application.Handlers.Queries.GetOrders;
using Orders.Application.Handlers.Queries.GetOrdersCount;
using System.Net;

namespace Orders.Api.Apis;

/// <summary>
/// Orders Api.
/// </summary>

[Authorize(Roles = "Admin")]
public class OrdersApi : IApi
{
    const string Tag = "Orders";

    private string _apiUrl = default!;

    /// <summary>
    /// Register orders apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";

        #region Queries

        app.MapGet($"{_apiUrl}/{{id}}", GetOrder)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get order")
            .Produces<BaseListDto<GetOrderDto>>()
            .RequireAuthorization();

        app.MapGet(_apiUrl, GetOrders)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get orders")
            .Produces<GetOrderDto[]>()
            .RequireAuthorization();

        app.MapGet($"{_apiUrl}/Count", GetOrdersCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get orders count")
            .Produces<int>()
            .RequireAuthorization();

        #endregion

        #region Command

        app.MapPost(_apiUrl, PostOrder)
            .WithTags(Tag)
            .Produces<GetOrderDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create order")
            .RequireAuthorization();

        app.MapPut($"{_apiUrl}/{{id}}", PutOrder)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update order")
            .RequireAuthorization()
            .Produces<GetOrderDto>();

        app.MapPut($"{_apiUrl}/Status/{{id}}", PutOrderStatus)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update order status")
            .RequireAuthorization()
            .Produces<GetOrderDto>();

        app.MapPut($"{_apiUrl}/Cancel/{{id}}", CancelOrder)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Cancel Order")
            .RequireAuthorization()
            .Produces<GetOrderDto>();

        #endregion
    }

    private Task<int> GetOrdersCount([FromServices] IMediator mediator, [AsParameters] GetOrdersCountQuery query, CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }

    [Authorize]
    private static Task<GetOrderDto> GetOrder([FromServices] IMediator mediator, [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetOrderQuery()
        {
            OrderId = id
        }, cancellationToken);
    }

    [Authorize]
    private static async Task<GetOrderDto[]> GetOrders(HttpContext httpContext, [FromServices] IMediator mediator, [AsParameters] GetOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return result.Items;
    }

    [Authorize]
    private async Task<IResult> PostOrder([FromServices] IMediator mediator, [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"{_apiUrl}/{{}}", result);
    }


    private static Task<GetOrderDto> PutOrder([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateOrderPayload payload,
        CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand()
        {
            OrderId = id,
            Products = payload.Products
        };
        return mediator.Send(command, cancellationToken);
    }


    private static Task<GetOrderDto> PutOrderStatus([FromServices] IMediator mediator, [FromBody] UpdateOrderStatusCommand command,
        CancellationToken cancellationToken)
    {
        return mediator.Send(command, cancellationToken);
    }

    [Authorize]
    private static Task<GetOrderDto> CancelOrder([FromServices] IMediator mediator, [FromBody] CancelOrderCommand command,
        CancellationToken cancellationToken)
    {
        return mediator.Send(command, cancellationToken);
    }
}