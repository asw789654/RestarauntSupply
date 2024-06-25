using Core.Storages.Domain;
using System.Linq.Expressions;

namespace Storages.Application.Handlers.Queries;

internal static class ListStorageWhere
{
    public static Expression<Func<Storage, bool>> WhereForAll(ListStorageFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => freeText == null || e.Name.Contains(freeText);
    }
    public static Expression<Func<Storage, bool>> WhereForProducts(ListStorageFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => freeText == null || e.Products.Where(p => p.Name.Contains(freeText)) != null;
    }
}