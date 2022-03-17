using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Lucky 
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
        public string ApplicationUserId{ get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime Create_at { get; set; }
        public int Type { get; set; } 
        public decimal Value { get; set; } 
        public decimal ActualValue { get; set; }
    }
}
