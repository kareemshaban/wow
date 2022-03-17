using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class ChargingFuncDetails
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public decimal ValueDollar { get; set; }
        public decimal Coins { get; set; }
        public string Notes { get; set; }
        public string OtherNotes { get; set; }
        public string Signature { get; set; }
        public bool IsFromSite2Agncy { get; set; } = false;
        public string ApplicatioUserId { get; set; }
        public virtual ApplicationUser ApplicatioUser { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User{ get; set; }
        public DateTime create_at { get; set; }
    }

}
