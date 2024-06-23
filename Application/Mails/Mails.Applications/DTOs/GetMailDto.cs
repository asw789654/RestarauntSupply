using Core.Application.Abstractions.Mappings;
using Mails.Domain;

namespace Mails.Application.DTOs;

public class GetMailDto
{
    public string Message { get; set; } = default!;

    public string SenderMailAddress { get; set; } = default!;

    public string RecipientMailAddress { get; set; } = default!;

    public string RecipientName { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string SenderPassword { get; set; } = default!;
}