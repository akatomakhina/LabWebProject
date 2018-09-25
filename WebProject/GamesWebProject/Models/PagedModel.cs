using System.Collections.Generic;

namespace EntertainmentPortal.Web.Models.Sports
{
    public class PagedModel<T>
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public string PrevPage { get; set; }
        public string NextPage { get; set; }

        public IEnumerable<T> Values { get; set; }
    }
}