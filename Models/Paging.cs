using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Models
{
    public class Paging<T> : List<T>
    {
        public int PageSize { get; set; } = 5;
        public int PageNum { get; set; } = 1;
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        private Paging(List<T> items, int pageSize, int pageNum, int totalCount)
        {
            this.PageSize = pageSize;
            this.PageNum = PageNum;
            this.TotalCount = totalCount;
            this.TotalPage = (int)Math.Ceiling(totalCount * 1.0 / pageSize);
            this.HasNext = pageNum < TotalPage;
            this.HasPrevious = pageNum > 1;
            this.AddRange(items);
        }

        public static async Task<Paging<T>> CreatePagedList(IQueryable<T> items, int pageSize, int pageNum)
        {
            int totalCount = items.Count();
            List<T> list = await items.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paging<T>(list, pageSize, pageNum, totalCount);
        }
    }
}
