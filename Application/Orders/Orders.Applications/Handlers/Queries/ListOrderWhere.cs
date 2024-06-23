using System.Linq.Expressions;
using Orders.Domain;

namespace Orders.Application.Handlers.Queries;

internal static class ListOrderWhere
{
    public static Expression<Func<Order, bool>> WhereForAll(ListOrderFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => freeText == null || e.Products[0].Name.Contains(freeText);
    }
}