using Microsoft.EntityFrameworkCore;

namespace TVShowTracker.API.Helpers
{
    public static class IQueryableExtensions
    {
        // Apply pagination and return items + total count
        public static async Task<(IEnumerable<T> Items, long Total)> ApplyPagingAsync<T>(
            this IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var total = await query.LongCountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
