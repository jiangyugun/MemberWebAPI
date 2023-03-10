using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberWebAPI.Shared.Models
{
    public partial class Account
    {
        public int No { get; set; }

        [Key]
        public string UserNo { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Account1 { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Refreshtoken { get; set; } = null!;
        public DateTime? Refreshtokenexpiration { get; set; }
        public DateTime? Birthday { get; set; }
        public string Sex { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Tel { get; set; }
        public string? Plftyp { get; set; }
        public string UserStatus { get; set; } = null!;
        public int? LoginErr { get; set; }
        public DateTime? ErrDate { get; set; }
        public string Creid { get; set; } = null!;
        public DateTime Credate { get; set; }
        public string? Updid { get; set; }
        public DateTime? Upddate { get; set; }
    }
}
