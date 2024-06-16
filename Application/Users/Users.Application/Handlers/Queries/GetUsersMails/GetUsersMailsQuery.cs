using Core.Application.DTOs;
using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUsersMails;

public class GetUsersMailsQuery : ListUserFilter, IBasePaginationFilter, IRequest<BaseListDto<GetUserMailDto>>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}