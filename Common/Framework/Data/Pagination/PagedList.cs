namespace Framework.Data.Pagination;

[Serializable]
public class PagedList<T>
{
    public PagedList(IEnumerable<T> data)
    {
        Data = data;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    public IEnumerable<T> Data { get; set; }

    public PagedList<T> From<TSource>(PagedList<TSource> pagedList)
    {
        PageNumber = pagedList.PageNumber;
        PageSize = pagedList.PageSize;
        TotalRecords = pagedList.TotalRecords;
        TotalPages = pagedList.TotalPages;
        NextPage = pagedList.NextPage;
        PreviousPage = pagedList.PreviousPage;

        return this;
    }
}