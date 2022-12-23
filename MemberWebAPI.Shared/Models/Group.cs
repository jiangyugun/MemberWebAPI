using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberWebAPI.Shared.Models
{
    public partial class Group
    {
        public int No { get; set; }
        [Key]
        public string GroupNo { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string UserNo { get; set; } = null!;
        public string CompanyNo { get; set; } = null!;
        public string GroupStatus { get; set; } = null!;
        public string Creid { get; set; } = null!;
        public DateTime Credate { get; set; }
        public string? Updid { get; set; }
        public DateTime? Upddate { get; set; }
    }
}
