using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class SendMsgViewModel
    {
        public string RecieverId { get; set; }
        public string SenderId { get; set; }
        public string Msg { get; set; }
        public string fileName { get; set; }
        public IFormFile file { get; set; }
        public string SenderName { get; set; }
    }
}
