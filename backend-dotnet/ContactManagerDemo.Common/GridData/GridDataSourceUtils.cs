using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Common.GridData;

public static class GridDataSourceUtils
{
    public static async Task<GridDataSource<T>> ToGridDataSourceAsync<T>(this IQueryable<T> query, GridDataRequest request) where T : IGridDataItem
    {
        var count = await query.CountAsync();
        query = EnsureOrderBy(query, request);
        var items = await query.Skip(request.Skip).Take(request.Take).ToListAsync();

        return new GridDataSource<T>(items, request.Skip, request.Take, count);
    }

    public static GridDataSource<T> ToGridDataSource<T>(this IQueryable<T> query, GridDataRequest request) where T : IGridDataItem
    {
        var count = query.Count();
        query = EnsureOrderBy(query, request);
        var items = query.Skip(request.Skip).Take(request.Take).ToList();

        return new GridDataSource<T>(items, request.Skip, request.Take, count);
    }

    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, GridDataRequest request, Func<string, string, Expression<Func<T, bool>>?> filterExpression)
    {
        if (string.IsNullOrEmpty(request.MagicFilter) || request.ColumnsToFilter.Count == 0)
        {
            return query;
        }

        var predicate = PredicateBuilder.False<T>();
        var nonNullFilters = request.ColumnsToFilter
            .Select(column => filterExpression(column, request.MagicFilter))
            .Where(filter => filter != null);

        predicate = nonNullFilters
            .Aggregate(predicate, (current, filter) => current.Or(filter!));

        return query.Where(predicate);
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, GridDataRequest request)
    {
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderBy($"{request.OrderBy} {(request.OrderDescending ? "descending" : "ascending")}");
        }
        return query;
    }

    private static IQueryable<T> EnsureOrderBy<T>(IQueryable<T> query, GridDataRequest request)
    {
        if (!query.Expression.ToString().Contains("OrderBy"))
        {
            query = query.OrderBy("Id");
        }
        return query;
    }
}
