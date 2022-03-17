using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class SiteSetting
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="decimal(8,2)")]
        public decimal ConvertGift2Diamond { get; set; }//نسبه تحويل الهديه الى ماس
        [Column(TypeName = "decimal(8,2)")]
        public decimal Buy100Diamond { get; set; }//سعر الشراء لكل 100 ماسه\
        [Column(TypeName = "decimal(8,2)")]
        public decimal PostPrice { get; set; }//سعر اضافه البوست  للمستخدم
        [Column(TypeName = "decimal(8,2)")]
        public decimal UserIdPrice { get; set; }//سعر الاى دى للمستخدم
        public decimal FestivalBannerPrice { get; set; }
        public decimal CustomBackgroundPrice { get; set; }
        public decimal ChargingAgencyAdminPer { get; set; } 
    }
}
