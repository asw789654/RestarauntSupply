using Core.Application.Abstractions;

namespace Users.Application.Caches;

public interface IApplicationUsersCountMemoryCache : IBaseCache<int>
{
}
