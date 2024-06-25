using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MailKit.Net.Smtp;
using Mails.Application.Caches;
using Mails.Application.DTOs;
using MediatR;
using MimeKit;

namespace Mails.Application.Handlers.Commands.SendMail;

internal class SendMailCommandHandler : IRequestHandler<SendMailCommand, GetMailDto>
{


    private readonly ICleanMailsCacheService _cleanMailsCacheService;

    private readonly ICurrentUserService _currentUserService;

    public SendMailCommandHandler(
        ICurrentUserService currentUserService,
        ICleanMailsCacheService cleanMailsCacheService)
    {
        _currentUserService = currentUserService;
        _cleanMailsCacheService = cleanMailsCacheService;
    }

    public async Task<GetMailDto> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("MailService", request.SenderMailAddress));
        message.To.Add(new MailboxAddress(request.RecipientName, request.RecipientMailAddress));
        message.Subject = request.Subject;
        message.Body = new TextPart("plain")
        {
            Text = request.Message
        };
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.yandex.ru", 587, false, cancellationToken);
            await client.AuthenticateAsync(request.SenderMailAddress, request.SenderPassword);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
        _cleanMailsCacheService.ClearListCaches();

        GetMailDto result = new GetMailDto()
        {
            Message = request.Message,

            SenderMailAddress = request.SenderMailAddress,

            RecipientMailAddress = request.RecipientMailAddress,

            RecipientName = request.RecipientName,

            Subject = request.Subject,

            SenderPassword = request.SenderPassword
        };

        return result;
    }
}