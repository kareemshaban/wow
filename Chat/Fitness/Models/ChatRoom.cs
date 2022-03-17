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
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ImgUrl { get; set; } // >> Not  used  now
        public string ApplicationUserId { get; set; }//Chat room admin
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string Desc { get; set; }
        public int ChatRoomCategoryId { get; set; }
        public string OptionalPassword { get; set; }
        public string State { get; set; }// حاله
        public int BackgroundId{ get; set; }
        public string CustomBackground { get; set; }//هنا  ل ضاف خلفيه من  الموبايل
        public DateTime CustomBackgroundAdd { get; set; }// الوقت اللى ضاف فيه الخلفيه وهتتشال بعد شهر
        public decimal Balancea{ get; set; }// لما يبعت  هديه هيزيد رصيد الغرفه بعدد الكوينز اللى فى  الهدية
        public DateTime LastDate { get; set; } // اخر تاريخ للاضافة
        [NotMapped]
      public IFormFile Image { get; set; }    
    }
}
