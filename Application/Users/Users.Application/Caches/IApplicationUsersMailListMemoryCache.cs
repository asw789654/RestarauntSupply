using Core.Application.Abstractions;
using Core.Application.DTOs;
using Users.Application.Dtos;

namespace Users.Application.Caches;

public interface IApplicationUsersMailListMemoryCache : IBaseCache<BaseListDto<GetUserMailDto>>
{
}
