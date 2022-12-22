using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberWebAPI.Shared.Models
{
    public class TokenResponseModel
    {
        public string Message { get; set; }
        public string AccessToken { get; set; }
    }
}
