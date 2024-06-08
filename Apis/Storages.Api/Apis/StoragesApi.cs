using Core.Application.Abstractions;
using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storages.Application.DTOs;
using Storages.Application.Handlers.Commands.CreateStorage;
using Storages.Application.Handlers.Commands.DeleteStorage;
using Storages.Application.Handlers.Commands.UpdateStorage;
using Storages.Application.Handlers.Queries.GetStorage;
using Storages.Application.Handlers.Queries.GetStorages;
using Storages.Application.Handlers.Queries.GetStoragesCount;
using System.Net;

namespace Storages.Api.Apis;

/// <summary>
/// Storages Api.
/// </summary>

[Authorize(Roles = "Admin")]
public class StoragesApi : IApi
{
    const string Tag = "Storages";

    private string _apiUrl = default!;

    /// <summary>
    /// Register storages apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";

        #region Queries

        app.MapGet($"{_apiUrl}/{{id}}", GetStorage)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get storage")
            .Produces<BaseListDto<GetStorageDto>>()
            .RequireAuthorization();

        app.MapGet(_apiUrl, GetStorages)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get storages")
            .Produces<GetStorageDto[]>()
            .RequireAuthorization();

        app.MapGet($"{_apiUrl}/Count", GetStoragesCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get storages count")
            .Produces<int>()
            .RequireAuthorization();

        #endregion

        #region Command

        app.MapPost(_apiUrl, PostStorage)
            .WithTags(Tag)
            .Produces<GetStorageDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create storage")
            .RequireAuthorization();

        app.MapPut($"{_apiUrl}/{{id}}", PutStorage)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update storage")
            .RequireAuthorization()
            .Produces<GetStorageDto>();

        app.MapDelete($"{_apiUrl}/{{id}}", DeleteStorage)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Delete storage")
            .RequireAuthorization();

        #endregion
    }

    private Task<int> GetStoragesCount([FromServices] IMediator mediator, [AsParameters] GetStoragesCountQuery query, CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }

    [Authorize]
    private static Task<GetStorageDto> GetStorage([FromServices] IMediator mediator, [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetStorageQuery()
        {
            StorageId = id
        }, cancellationToken);
    }

    [Authorize]
    private static async Task<GetStorageDto[]> GetStorages(HttpContext httpContext, [FromServices] IMediator mediator, [AsParameters] GetStoragesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        //httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }

    private async Task<IResult> PostStorage([FromServices] IMediator mediator, [FromBody] CreateStorageCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"{_apiUrl}/{{}}", result);
    }

    private static Task<GetStorageDto> PutStorage([FromServices] IMediator mediator, [FromRoute] int id, [FromBody] UpdateStoragePayload payload,
        CancellationToken cancellationToken)
    {
        var command = new UpdateStorageCommand()
        {
            StorageId = id,
            Name = payload.Name,
            StorageTypeId = payload.StorageTypeId,
            Capacity = payload.Capacity,
        };
        return mediator.Send(command, cancellationToken);
    }

    private static Task DeleteStorage([FromServices] IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
    {
        return mediator.Send(new DeleteStorageCommand { StorageId = id }, cancellationToken);
    }
}