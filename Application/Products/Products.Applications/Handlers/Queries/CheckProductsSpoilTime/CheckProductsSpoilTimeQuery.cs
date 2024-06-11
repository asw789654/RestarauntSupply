using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Applications.DTOs;

namespace Products.Applications.Handlers.Queries.CheckProductsSpoilTime;

[RequestAuthorize]
public class CheckProductsSpoilTimeQuery : IRequest<BaseListDto<GetProductDto>>
{
}