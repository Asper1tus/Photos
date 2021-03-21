using System.Linq;

namespace Photos.API.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> SkipOrAll<T>(this IQueryable<T> query, int? skip)
        {
            return skip.HasValue ? query.Skip(skip.Value) : query;
        }
        public static IQueryable<T> TakeOrAll<T>(this IQueryable<T> query, int? take)
        {
            return take.HasValue ? query.Take(take.Value) : query;
        }
    }
}
