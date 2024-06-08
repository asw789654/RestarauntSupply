using System.Net;
using Core.Application.Abstractions;
using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Applications.DTOs;
using Products.Applications.Handlers.Commands.CreateProduct;
using Products.Applications.Handlers.Commands.DeleteProduct;
using Products.Applications.Handlers.Commands.SpendProduct;
using Products.Applications.Handlers.Commands.UpdateProduct;
using Products.Applications.Handlers.Queries.GetProduct;
using Products.Applications.Handlers.Queries.GetProducts;
using Products.Applications.Handlers.Queries.GetProductsCount;

namespace Products.Api.Apis;

/// <summary>
/// Products Api.
/// </summary>

[Authorize(Roles = "Admin")]
public class ProductsApi : IApi
{
    const string Tag = "Products";

    private string _apiUrl = default!;

    /// <summary>
    /// Register products apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";

        #region Queries

        app.MapGet($"{_apiUrl}/{{id}}", GetProduct)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get product")
            .Produces<BaseListDto<GetProductDto>>()
            .RequireAuthorization();

        app.MapGet(_apiUrl, GetProducts)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get products")
            .Produces<GetProductDto[]>()
            .RequireAuthorization();

        app.MapGet($"{_apiUrl}/Count", GetProductsCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get products count")
            .Produces<int>()
            .RequireAuthorization();

        #endregion

        #region Command

        app.MapPost(_apiUrl, PostProduct)
            .WithTags(Tag)
            .Produces<GetProductDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create product")
            .RequireAuthorization();

        app.MapPut($"{_apiUrl}/{{id}}", PutProduct)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update product")
            .RequireAuthorization()
            .Produces<GetProductDto>();

        app.MapDelete($"{_apiUrl}/{{id}}", DeleteProduct)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Delete product")
            .RequireAuthorization();

        app.MapPut($"{_apiUrl}/ChangeVolume/{{id}}", ChangeVolumeProduct)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Delete product")
            .RequireAuthorization();
        //ChangeVolumeProductCommand
        #endregion
    }

    private Task<int> GetProductsCount([FromServices] IMediator mediator, [AsParameters] GetProductsCountQuery query, CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }

    [Authorize]
    private static Task<GetProductDto> GetProduct([FromServices] IMediator mediator, [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetProductQuery()
        {
            ProductId = id
        }, cancellationToken);
    }

    private static async Task<GetProductDto[]> GetProducts(HttpContext httpContext, [FromServices] IMediator mediator, [AsParameters] GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        //httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }

    private async Task<IResult> PostProduct([FromServices] IMediator mediator, [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"{_apiUrl}/{{}}", result);
    }

    private static Task<GetProductDto> PutProduct([FromServices] IMediator mediator, [FromRoute] int id, [FromBody] UpdateProductPayload payload,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand()
        {
            ProductId = id,
            Name = payload.Name,
            SpoilTime = payload.SpoilTime,
            Volume = payload.Volume,
            StorageTypeId = payload.StorageTypeId
        };
        return mediator.Send(command, cancellationToken);
    }

    private static Task DeleteProduct([FromServices] IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
    {
        return mediator.Send(new DeleteProductCommand { ProductId = id }, cancellationToken);
    }
    private static Task<GetProductDto> ChangeVolumeProduct([FromServices] IMediator mediator, [FromRoute] int id, [FromBody] SpendProductPayload payload,
        CancellationToken cancellationToken)
    {
        var command = new SpendProductCommand()
        {
            ProductId = id,
            Volume = payload.Volume
        };
        return mediator.Send(command, cancellationToken);
    }
}