using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string content { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImgUrl { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime publishDate { get; set; }
        public virtual List<Like> Likes{ get; set; }
    }
}
