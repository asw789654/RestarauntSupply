using System.Net;
using Core.Application.Abstractions;
using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mails.Applications.DTOs;
using Mails.Applications.Handlers.Commands.CreateMail;
using Mails.Applications.Handlers.Commands.UpdateMail;
using Mails.Applications.Handlers.Commands.UpdateMailStatus;
using Mails.Applications.Handlers.Queries.GetMail;
using Mails.Applications.Handlers.Queries.GetMails;
using Mails.Applications.Handlers.Queries.GetMailsCount;

namespace Mails.Api.Apis;

/// <summary>
/// Mails Api.
/// </summary>

[Authorize(Roles = "Admin")]
public class MailsApi : IApi
{
    const string Tag = "Mails";

    private string _apiUrl = default!;

    /// <summary>
    /// Register mails apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";

        #region Queries

        app.MapGet($"{_apiUrl}/{{id}}", GetMail)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get mail")
            .Produces<BaseListDto<GetMailDto>>()
            .RequireAuthorization();

        app.MapGet(_apiUrl, GetMails)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get mails")
            .Produces<GetMailDto[]>()
            .RequireAuthorization();

        app.MapGet($"{_apiUrl}/Count", GetMailsCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get mails count")
            .Produces<int>()
            .RequireAuthorization();

        #endregion

        #region Command

        app.MapPost(_apiUrl, PostMail)
            .WithTags(Tag)
            .Produces<GetMailDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create mail")
            .RequireAuthorization();

        app.MapPut($"{_apiUrl}/{{id}}", PutMail)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update mail")
            .RequireAuthorization()
            .Produces<GetMailDto>();

        app.MapPut($"{_apiUrl}/Status/{{id}}", PutMailStatus)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update mail status")
            .RequireAuthorization()
            .Produces<GetMailDto>();

        #endregion
    }

    private Task<int> GetMailsCount([FromServices] IMediator mediator, [AsParameters] GetMailsCountQuery query, CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }

    [Authorize]
    private static Task<GetMailDto> GetMail([FromServices] IMediator mediator, [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetMailQuery()
        {
            MailId = id
        }, cancellationToken);
    }

    [Authorize]
    private static async Task<GetMailDto[]> GetMails(HttpContext httpContext, [FromServices] IMediator mediator, [AsParameters] GetMailsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        //httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }

    [Authorize]
    private async Task<IResult> PostMail([FromServices] IMediator mediator, [FromBody] CreateMailCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"{_apiUrl}/{{}}", result);
    }

    [Authorize]
    private static Task<GetMailDto> PutMail([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateMailPayload payload,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMailCommand()
        {
            MailId = id,
            Products = payload.Products
        };
        return mediator.Send(command, cancellationToken);
    }

    [Authorize(Roles = "Admin")]
    private static Task<GetMailDto> PutMailStatus([FromServices] IMediator mediator, [FromBody] UpdateMailStatusCommand command,
        CancellationToken cancellationToken)
    {
        return mediator.Send(command, cancellationToken);
    }
}