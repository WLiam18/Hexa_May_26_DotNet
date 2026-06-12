namespace Api_Pagination_Sorting_Demo.Dtos.Common
{
    public class ApiErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorType { get; set; }
        public string? TraceId { get; set; }
        public string? Details { get; set; }
    }
}
