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
    public class User2UserMsg
    {
        [Key]
        public int Id { get; set; }
        public string RecieverId { get; set; }
        public ApplicationUser Reciever { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        [Column(TypeName ="nvarchar(Max)")]
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? SeenDate { get; set; }
        public bool IsSeen { get; set; }
        public string FileName { get; set; }
        public bool IsAdminstationMsg { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        [NotMapped]
        public string SenderName { get; set;}
    }
}
