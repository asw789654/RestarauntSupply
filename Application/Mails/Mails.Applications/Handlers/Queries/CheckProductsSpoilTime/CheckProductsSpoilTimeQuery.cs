using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Mails.Applications.DTOs;

namespace Mails.Applications.Handlers.Queries.CheckMailsSpoilTime;

[RequestAuthorize]
public class CheckMailsSpoilTimeQuery : IRequest<BaseListDto<GetProductDto>>
{
}