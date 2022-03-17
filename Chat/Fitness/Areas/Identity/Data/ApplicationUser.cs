using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public int Gender { get; set; }//1 > Male    . 2>Female   . 3>Other
        [Required]
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public int? LevelId { get; set; } 
        public virtual Level Level { get; set; }
        public int OtherLevelId { get; set; } 
        public virtual OtherLevel OtherLevel { get; set; }
        public virtual ICollection<Post> posts{ get; set; }
        public virtual ICollection<Wallet> wallet { get; set; }
        public int UserId { get; set; } // بيبدا من  1000
        public string about { get; set; } 
        public DateTime RegisterDate { get; set; }
        public bool userblocked { get; set; }
        public int CountryId { get; set; }
        public int ChargingLevelId { get; set; } // المستوى الخاص بعدد الهدايا المرسله
        public virtual ChargingLevel ChargingLevel { get; set; }
        public string Tower { get; set; }
        public int InterstedWith { get; set; } // مهتم ب  - ذكر  - انثى
        public bool ReceiveMsg { get; set; } // يستقبل اشعارات رسايل
        public bool ReceiveInvitation { get; set; }// يستقبل دعوات وطلبات الصداقه
        public bool ReceiveChatRoomMsg { get; set; } // يستقبل اشعارات رسائل  الغرفه
        public int days { get; set; } // عدد الايام اللى  المستخدم  هيتحظر فيه  من  دخول الابلكيشن
        public DateTime blockedStartDate { get; set; } 
        public virtual List<UserImage> UserImage { get; set; } 
    }
}
