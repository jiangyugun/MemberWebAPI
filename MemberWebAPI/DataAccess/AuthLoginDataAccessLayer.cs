using MemberWebAPI.DbContexts;
using MemberWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MemberWebAPI.DataAccess
{
    public class AuthLoginDataAccessLayer
    {
        readonly EldPlatContext _eldPlatContext;
        readonly TokenSettings _tokenSettings;
        readonly IHttpContextAccessor _httpcontextAccessor;

        public AuthLoginDataAccessLayer(IDbContextFactory<EldPlatContext> eldPlatContext, IOptions<TokenSettings> tokenSettings, IHttpContextAccessor httpcontextAccessor)
        {
            _eldPlatContext = eldPlatContext.CreateDbContext();
            _tokenSettings = tokenSettings.Value;
            _httpcontextAccessor = httpcontextAccessor;
        }
    }
}
