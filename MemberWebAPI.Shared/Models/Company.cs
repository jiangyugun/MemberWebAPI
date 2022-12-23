using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberWebAPI.Shared.Models
{
    public partial class Company
    {
        public int No { get; set; }
        [Key]
        public string CompanyNo { get; set; } = null!;
        public int InvoiceNo { get; set; }
        public string ComName { get; set; } = null!;
        public string CompanyTyp { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public string? ComEmail { get; set; }
        public string? PlfTyp { get; set; }
        public string? PlanTyp { get; set; }
        public string SubStatus { get; set; } = null!;
        public DateTime SubTime { get; set; }
        public string Creid { get; set; } = null!;
        public DateTime Credate { get; set; }
        public string? Updid { get; set; }
        public DateTime? Upddate { get; set; }
    }
}
