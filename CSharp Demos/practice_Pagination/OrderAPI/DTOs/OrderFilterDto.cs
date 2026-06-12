namespace OrderAPI.DTOs
{
    public class OrderFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CustomerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SortBy { get; set; } = "OrderDate";
        public string? SortDirection { get; set; } = "desc";
    }
}