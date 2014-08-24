using System.Collections.Generic;

namespace MvcMonitor.Models
{
    public class PagedList<T> where T : class
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public IEnumerable<T> Items { get; private set; }

        public PagedList(int page, int pageSize, int totalCount, IEnumerable<T> items)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }
    }
}