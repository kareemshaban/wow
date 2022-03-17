using Fitness.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }
        public DateTime LastUpdate{ get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalBalance { get; set; }
        public decimal DiamonadBalance { get; set; }// رصيد المستخدم من الماس او  الفل
    }
}
