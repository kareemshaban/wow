using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class UserImageLike
    {
         public int Id { get; set; }
        public DateTime date{ get; set; }
        public string ApplicationUserId { get; set; }  
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string LikerId { get; set; }
        public virtual ApplicationUser Liker { get; set; }

    }
}
