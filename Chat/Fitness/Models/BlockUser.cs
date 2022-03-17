using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class BlockUser
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string blockedUserId { get; set; }
        public virtual ApplicationUser blockedUser { get; set; }
        public DateTime date { get; set; }
    }
}
