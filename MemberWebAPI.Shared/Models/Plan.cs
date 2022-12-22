using System;
using System.Collections.Generic;

namespace MemberWebAPI.Shared.Models
{
    public partial class Plan
    {
        public int No { get; set; }
        public string PlansNo { get; set; } = null!;
        public string? PlansName { get; set; }
        public string CompanyNo { get; set; } = null!;
        public string PlanTyp { get; set; } = null!;
        public DateTime? StaDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Amount { get; set; }
        public string Creid { get; set; } = null!;
        public DateTime Credate { get; set; }
        public string? Updid { get; set; }
        public DateTime? Upddate { get; set; }
    }
}
