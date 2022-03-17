using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public DateTime create_at{ get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }      
    }
}
