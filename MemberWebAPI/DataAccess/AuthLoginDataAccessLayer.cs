using MemberWebAPI.DbContexts;
using MemberWebAPI.Interface;
using MemberWebAPI.Shared.InputType;
using MemberWebAPI.Shared.Interface;
using MemberWebAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MemberWebAPI.DataAccess
{
    public class AuthLoginDataAccessLayer : IAuthLogin
    {
        readonly EldPlatContext _eldPlatContext;
        readonly TokenSettings _tokenSettings;
        readonly IHttpContextAccessor _httpcontextAccessor;
        readonly IUtility _utility;

        public AuthLoginDataAccessLayer(IDbContextFactory<EldPlatContext> eldPlatContext,
            IOptions<TokenSettings> tokenSettings, IHttpContextAccessor httpcontextAccessor, IUtility utility)
        {
            _eldPlatContext = eldPlatContext.CreateDbContext();
            _tokenSettings = tokenSettings.Value;
            _httpcontextAccessor = httpcontextAccessor;
            _utility = utility;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        public TokenResponseModel Login(LoginInputType loginInput)
        {
            var result = new TokenResponseModel { Message = "Success", AccessToken = "" };
            if (string.IsNullOrEmpty(loginInput.Account) || string.IsNullOrEmpty(loginInput.Password))
            {
                result.Message = "Invalid Credentials";
                return result;
            }

            var user = _eldPlatContext.Accounts.FirstOrDefault(x => x.Account1 == loginInput.Account);

            if (user == null)
            {
                result.Message = "查無使用者";
                return result;
            }

            bool checkHours = true;
            if (user.ErrDate is not null)
            {
                var totalHours = DateTime.Now.Subtract(user.ErrDate.Value);
                if (totalHours.TotalHours < 1)
                {
                    result.Message = "登入錯誤超過3次,請於一小時後登入";
                    return result;
                }
                else
                {
                    checkHours = false;
                    user.LoginErr = 0;
                    user.ErrDate = null;
                }
            }

            if (user.LoginErr > 2 && checkHours)
            {
                result.Message = "登入錯誤超過3次,請於一小時後登入";
                user.ErrDate = DateTime.Now.AddHours(1);
                _eldPlatContext.Accounts.Update(user);
                _eldPlatContext.Entry(user).Property(x => x.No).IsModified = false;
                _eldPlatContext.SaveChanges();

                return result;
            }

            if (!_utility.ValidatePasswordHash(loginInput.Password, user.Password))
            {
                user.LoginErr += 1;
                result.Message = "帳號或密碼已經錯誤" + user.LoginErr.ToString() + "次";
                _eldPlatContext.Accounts.Update(user);
                _eldPlatContext.Entry(user).Property(x => x.No).IsModified = false;
                _eldPlatContext.SaveChanges();

                return result;
            }

            var roles = _eldPlatContext.Groups.Where(_ => _.UserNo == user.UserNo).ToList();
            result.AccessToken = _utility.GetJWTAuthKey(user, roles, _tokenSettings);
            var refreshToken = _utility.GenerateRefreshToken();

            _utility.SetCookieForRefreshToken(refreshToken, _httpcontextAccessor);

            user.Refreshtoken = refreshToken;
            user.Refreshtokenexpiration = DateTime.Now.AddDays(1);

            _eldPlatContext.Accounts.Update(user);
            _eldPlatContext.Entry(user).Property(x => x.No).IsModified = false;
            _eldPlatContext.SaveChanges();

            return result;
        }
    }
}
