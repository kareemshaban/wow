using Fitness.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class UserGift
    {
        /// <summary>
        /// الهدايا  من  هنا  ...
        /// ApplicationUserId   اللى بعت الهديه  .. 
        // NewUserId   الشخص اللى  اتبعتله الهديه

        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Column(TypeName = "int")]
        public int GiftId { get; set; }
        public virtual Gift Gift { get; set; }
        public string NewUserId { get; set; }
        public virtual ApplicationUser NewUser { get; set; }
        public DateTime date { get; set; }// التاريخ اللى  تم فيه شراء  المنتج او ارساله كهديه
        public bool Used { get; set; }//لو  ترو  تم استخدامه لنفسه لكن  لو  هديه  هتفضل زى ما  هى فلس  لكن  نيو  يوزر  هيبقى بالاى دى الجديد
        public int DaysCount { get; set; }
        [NotMapped]
        public decimal ConvertGift2Diamond { get; set; }
        public int? ChatRoomID { get; set; }

        public virtual ChatRoom chatRoom{ get; set; }
    }
}
