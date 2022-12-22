using MemberWebAPI.Shared.InputType;
using MemberWebAPI.Shared.Models;

namespace MemberWebAPI.Interface
{
    public interface IAuthLogin
    {
        TokenResponseModel Login(LoginInputType loginInput);
    }
}