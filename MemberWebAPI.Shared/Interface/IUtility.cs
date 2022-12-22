using MemberWebAPI.Shared.Models;
using MemberWebAPI.Shared.InputType;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MemberWebAPI.Shared.Interface
{
    public interface IUtility
    {
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken, TokenSettings tokenSettings);
        bool ValidatePasswordHash(string password, string dbPassword);
        string GetJWTAuthKey(Account account, List<Group> groups, TokenSettings tokenSettings);
        string PasswordHash(string password);
        string ResigstrationValidations(RegisterInputType registerInput);
        void SetCookieForRefreshToken(string? refreshToken, IHttpContextAccessor _httpContextAccessor);
    }
}