using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class OtherLevel
    {
        //المستويات للهدايا المستقبله
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int GiftRecieverCount { get; set; }//عدد الهدايا المستقبله
        [Column(TypeName = "nvarchar(250)")]
        public string LevelName { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }
        public string Color { get; set; }
    }
}
