using FluentValidation;

namespace Mails.Applications.Handlers.Commands.SendMail;

internal class SendMailCommandValidator : AbstractValidator<SendMailCommand>
{
    public SendMailCommandValidator()
    {

    }
}