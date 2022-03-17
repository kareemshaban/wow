using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class ChatRoomMsg
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom{ get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        public bool IsRadio { get; set; }
        [NotMapped]
        public string SenderName { get; set; }
        public int Type { get; set; } 

    }
}
