using System.Linq.Expressions;
using Core.Products.Domain;

namespace Products.Application.Handlers.Queries;

internal static class ListProductWhere
{
    public static Expression<Func<Product, bool>> WhereForAll(ListProductFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => freeText == null || e.Name.Contains(freeText);
    }
}