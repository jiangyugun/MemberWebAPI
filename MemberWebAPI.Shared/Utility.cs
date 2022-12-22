using MemberWebAPI.Shared.Models;
using MemberWebAPI.Shared.InputType;
using MemberWebAPI.Shared.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = MemberWebAPI.Shared.Models.Group;

namespace MemberWebAPI.Shared
{
    internal class Utility : IUtility
    {
        /// <summary>
        /// 註冊-資料驗證
        /// </summary>
        /// <param name="registerInput"></param>
        /// <returns></returns>
        public string ResigstrationValidations(RegisterInputType registerInput)
        {
            if (string.IsNullOrEmpty(registerInput.EmailAddress))
            {
                return "Eamil can't be empty";
            }

            if (string.IsNullOrEmpty(registerInput.Password)
                || string.IsNullOrEmpty(registerInput.ConfirmPassword))
            {
                return "Password Or ConfirmPasswor Can't be empty";
            }

            if (registerInput.Password != registerInput.ConfirmPassword)
            {
                return "Invalid confirm password";
            }

            string emailRules = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            if (!Regex.IsMatch(registerInput.EmailAddress, emailRules))
            {
                return "Not a valid email";
            }

            // atleast one lower case letter
            // atleast one upper case letter
            // atleast one special character
            // atleast one number
            // atleast 8 character length
            string passwordRules = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
            if (!Regex.IsMatch(registerInput.Password, passwordRules))
            {
                return "Not a valid password";
            }

            return string.Empty;
        }

        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string PasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dbPassword"></param>
        /// <returns></returns>
        public bool ValidatePasswordHash(string password, string dbPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(dbPassword);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 產生Access Token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public string GetJWTAuthKey(Account account, List<Group> groups, TokenSettings tokenSettings)
        {
            var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key));

            var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim("Email", account.Account1));
            claims.Add(new Claim("UserName", account.UserName));
            if ((groups?.Count ?? 0) > 0)
            {
                foreach (var group in groups)
                {
                    claims.Add(new Claim(ClaimTypes.Role, group.GroupName));
                }
            }

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// 產生Refresh Token
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// 替換已過期的 Access Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken, TokenSettings tokenSettings)
        {
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

            var jwtScurityToken = securityToken as JwtSecurityToken;
            if (jwtScurityToken == null)
            {
                return null;
            }
            return principal;
        }

        /// <summary>
        /// set-Cookie refreshToken
        /// </summary>
        /// <param name="refreshToken"></param>
        public void SetCookieForRefreshToken(string? refreshToken, IHttpContextAccessor _httpContextAccessor)
        {
            if (_httpContextAccessor.HttpContext is not null && !string.IsNullOrWhiteSpace(refreshToken))
            {
                CookieOptions options = new()
                {
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTime.Now.AddDays(7),
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, options);
            }
        }
    }
}
