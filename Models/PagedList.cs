using Microsoft.EntityFrameworkCore;

public class PagedList<T> where T : class
{
    public List<T> Items { get; }
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList()
    {
        Items = new List<T>();
        CurrentPage = 1;
        TotalPages = 1;
        PageSize = 0;
        TotalCount = 0;
    }
    private PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public static async Task<PagedList<T>> ToPagedListAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
