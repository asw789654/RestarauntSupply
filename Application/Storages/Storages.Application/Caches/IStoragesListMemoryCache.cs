using Core.Application.Abstractions;
using Core.Application.DTOs;
using Storages.Application.DTOs;

namespace Storages.Application.Caches;

public interface IStoragesListMemoryCache : IBaseCache<BaseListDto<GetStorageDto>>
{
}
