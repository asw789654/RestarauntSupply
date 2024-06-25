using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.CheckProductsSpoilTime;

[RequestAuthorize]
public class CheckProductsSpoilTimeCommand : IRequest<BaseListDto<GetProductDto>>
{
}