using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.ViewModel
{
    public class NotificationViewModel
    {   
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string NewUserId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Type { get; set; }

        public DateTime Create_at { get; set; }

        public ApplicationUser NewUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
