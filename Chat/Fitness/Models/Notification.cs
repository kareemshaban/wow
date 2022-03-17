using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Notification 
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Type { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }

        public string NewUserId { get; set; }
        public virtual ApplicationUser NewUser { get; set; }
        public DateTime Create_at { get; set; }
    }
}
