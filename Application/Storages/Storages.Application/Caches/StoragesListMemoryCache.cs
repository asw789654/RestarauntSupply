using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Storages.Application.DTOs;

namespace Storages.Application.Caches;

public class StoragesListMemoryCache : BaseCache<BaseListDto<GetStorageDto>>;