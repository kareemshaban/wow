using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class RolletMember
    {
        public int Id { get; set; }
        public int RolletId { get; set; }
        public virtual Rollet Rollet { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime create_at { get; set; }
        public bool IsWinner { get; set; } 
        public bool IsLoser { get; set; } // خسر

    }
}
