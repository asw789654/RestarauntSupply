using Core.Application.Abstractions;
using Storages.Application.DTOs;

namespace Storages.Application.Caches;

public interface IStoragesMemoryCache : IBaseCache<GetStorageDto>
{
}
