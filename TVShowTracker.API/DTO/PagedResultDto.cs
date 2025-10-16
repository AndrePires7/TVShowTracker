namespace TVShowTracker.API.DTOs
{
    //This class is used to standardize pagination for any type of data (TVShows, Actors, etc.).
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }

        //The subset of items for the current page
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }
}