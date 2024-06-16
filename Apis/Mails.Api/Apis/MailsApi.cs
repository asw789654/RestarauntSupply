using Core.Application.Abstractions;
using Mails.Applications.DTOs;
using Mails.Applications.Handlers.Commands.SendMail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        #region Command

        app.MapPost($"{_apiUrl}/Send", SendMail)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Send mail")
            .RequireAuthorization()
            .Produces<GetMailDto>();

        #endregion
    }

    private async Task<IResult> SendMail([FromServices] IMediator mediator, [FromBody] SendMailCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}