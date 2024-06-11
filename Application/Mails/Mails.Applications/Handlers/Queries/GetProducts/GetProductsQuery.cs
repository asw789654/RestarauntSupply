using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Mails.Applications.DTOs;

namespace Mails.Applications.Handlers.Queries.GetMails;

[RequestAuthorize]
public class GetMailsQuery : ListProductFilter, IBasePaginationFilter, IRequest<BaseListDto<GetProductDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}