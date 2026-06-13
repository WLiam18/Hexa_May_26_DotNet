using Api_Pagination_Sorting_Demo.Models;

namespace Api_Pagination_Sorting_Demo.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user,List<string> roles,out DateTime expiresAt);
    }
}
