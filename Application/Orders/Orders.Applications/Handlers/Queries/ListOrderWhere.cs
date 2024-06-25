using Orders.Domain;
using System.Linq.Expressions;

namespace Orders.Application.Handlers.Queries;

internal static class ListOrderWhere
{
    public static Expression<Func<Order, bool>> WhereForAll(ListOrderFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => freeText == null || e.Products.Where(p => p.Name.Contains(freeText)) != null;
    }
    public static Expression<Func<Order, bool>> WhereForClient(ListOrderFilter filter, Guid currentUserId)
    {
        var freeText = filter.FreeText?.Trim();
        return e => e.UserId == currentUserId &&
        (freeText == null || e.Products.Where(p => p.Name.Contains(freeText)) != null);
    }
}