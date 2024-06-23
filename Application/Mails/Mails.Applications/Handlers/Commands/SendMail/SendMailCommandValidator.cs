using FluentValidation;

namespace Mails.Application.Handlers.Commands.SendMail;

internal class SendMailCommandValidator : AbstractValidator<SendMailCommand>
{
    public SendMailCommandValidator()
    {

    }
}