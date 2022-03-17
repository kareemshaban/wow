using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }// الزائر
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string MainUserId { get; set; }// الشخص نفسه
        public virtual ApplicationUser MainUser { get; set; }
        public DateTime date { get; set; }
    }
}
