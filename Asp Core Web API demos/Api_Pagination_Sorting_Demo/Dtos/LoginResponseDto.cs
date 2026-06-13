namespace Api_Pagination_Sorting_Demo.Dtos
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new List<string>();

        public string Token { get; set; } = string.Empty;

        public DateTime TokenExpiresAt { get; set; }
    }
}
