using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public DateTime date { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
