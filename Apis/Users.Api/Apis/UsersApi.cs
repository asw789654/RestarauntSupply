using Core.Application.Abstractions;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Users.Application.Dtos;
using Users.Application.Handlers.Commands.CreateUser;
using Users.Application.Handlers.Commands.DeleteUser;
using Users.Application.Handlers.Commands.UpdateUser;
using Users.Application.Handlers.Commands.UpdateUserPassword;
using Users.Application.Handlers.Queries.GetUser;
using Users.Application.Handlers.Queries.GetUsers;
using Users.Application.Handlers.Queries.GetUsersCount;
using Users.Application.Handlers.Queries.GetUsersMails;

namespace Users.Api.Apis;

/// <summary>
/// Users Api.
/// </summary>

[Authorize(Roles = "Admin")]
public class UsersApi : IApi
{
    const string Tag = "Users";

    private string _apiUrl = default!;

    /// <summary>
    /// Register users apis.
    /// </summary>
    /// <param name="app">App.</param>
    /// <param name="baseApiUrl">Base url for apis.</param>
    public void Register(WebApplication app, string baseApiUrl)
    {
        _apiUrl = $"{baseApiUrl}/{Tag}";

        #region Queries

        app.MapGet(_apiUrl, GetUsers)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get users")
            .Produces<GetUserDto>()
            .RequireAuthorization(AuthorizationPoliciesEnum.AdminGreetings.ToString());

        app.MapGet($"{_apiUrl}/Mails", GetUsersMails)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get users mails")
            .Produces<GetUserMailDto>()
            .RequireAuthorization(AuthorizationPoliciesEnum.AdminGreetings.ToString());

        app.MapGet($"{_apiUrl}/{{id}}", GetUser)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get user")
            .Produces<GetUserDto>();

        app.MapGet($"{_apiUrl}/Count", GetUsersCount)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Get users count")
            .Produces<int>()
            .RequireAuthorization(AuthorizationPoliciesEnum.AdminGreetings.ToString()); ;

        #endregion

        #region Command

        app.MapPost(_apiUrl, PostUser)
            .WithTags(Tag)
            .Produces<GetUserDto>((int)HttpStatusCode.Created)
            .WithOpenApi()
            .WithSummary("Create user");

        app.MapPut($"{_apiUrl}/{{id}}", PutUser)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update user")
            .RequireAuthorization()
            .Produces<GetUserDto>();

        app.MapPatch($"{_apiUrl}/{{id}}/Password", PatchUserPassword)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Update user password")
            .RequireAuthorization();

        app.MapDelete($"{_apiUrl}/{{id}}", DeleteUser)
            .WithTags(Tag)
            .WithOpenApi()
            .WithSummary("Delete user")
            .RequireAuthorization();

        #endregion
    }

    [Authorize]
    private Task PatchUserPassword([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateUserPasswordPayload payload, CancellationToken cancellationToken)
    {
        var command = new UpdateUserPasswordCommand()
        {
            UserId = id,
            Password = payload.Password
        };
        return mediator.Send(command, cancellationToken);
    }

    private static Task<int> GetUsersCount([FromServices] IMediator mediator, [AsParameters] GetUsersCountQuery query,
        CancellationToken cancellationToken)
    {
        return mediator.Send(query, cancellationToken);
    }


    private static Task<GetUserDto> GetUser([FromServices] IMediator mediator, [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetUserQuery
        {
            Id = id
        }, cancellationToken);
    }

    private static async Task<GetUserDto[]> GetUsers(HttpContext httpContext, [FromServices] IMediator mediator,
        [AsParameters] GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }

    private static async Task<GetUserMailDto[]> GetUsersMails(HttpContext httpContext, [FromServices] IMediator mediator,
       [AsParameters] GetUsersMailsQuery query,
       CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        httpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        return result.Items;
    }

    private static Task DeleteUser([FromServices] IMediator mediator, [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
    }

    private async Task<IResult> PostUser([FromServices] IMediator mediator, [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Created($"{_apiUrl}/{result.ApplicationUserId}", result);
    }

    private static Task<GetUserDto> PutUser([FromServices] IMediator mediator, [FromRoute] string id,
        [FromBody] UpdateUserPayload payload,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new UpdateUserCommand
        {
            Id = id,
            Login = payload.Login
        }, cancellationToken);
    }
}