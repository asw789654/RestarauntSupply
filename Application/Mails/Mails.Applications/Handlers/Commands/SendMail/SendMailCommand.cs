using Core.Auth.Application.Attributes;
using Mails.Applications.DTOs;
using MediatR;

namespace Mails.Applications.Handlers.Commands.SendMail;

[RequestAuthorize]
public class SendMailCommand : IRequest<GetMailDto>
{
    public string Message { get; set; } = default!;

    public string SenderMailAddress { get; set; } = default!;

    public string RecipientMailAddress { get; set; } = default!;

    public string RecipientName { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string SenderPassword { get; set; } = default!;
    
}