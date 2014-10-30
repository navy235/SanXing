using System;
using System.Collections.Generic;
using System.Linq;
using Mt.Core;

namespace SanXing.Data
{
    public static class Extensions
    {
        public static IPagedList<T> Paging<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            var result = new PagedList<T>(source, pageIndex, pageSize);
            return result;
        }
    }
}
