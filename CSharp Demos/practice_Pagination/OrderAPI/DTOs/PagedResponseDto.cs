using System.Collections.Generic;

namespace OrderAPI.DTOs
{
    public class PagedResponseDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}