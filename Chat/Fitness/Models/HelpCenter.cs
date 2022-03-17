using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class HelpCenter 
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int issueid { get; set; }
        public string details { get; set; }
        public string phoneNumber { get; set; }
        public string issueTitle { get; set; }
        public DateTime create_at { get; set; }
    }
}
