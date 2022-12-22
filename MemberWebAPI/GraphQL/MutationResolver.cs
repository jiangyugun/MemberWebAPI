using MemberWebAPI.Interface;
using MemberWebAPI.Shared.InputType;
using MemberWebAPI.Shared.Models;

namespace MemberWebAPI.GraphQL
{
    public class MutationResolver
    {
        public TokenResponseModel Login([Service] IAuthLogin authLogic, LoginInputType loginInput)
        {
            return authLogic.Login(loginInput);
        }
    }
}
