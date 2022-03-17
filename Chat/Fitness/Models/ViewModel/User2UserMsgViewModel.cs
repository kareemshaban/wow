using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class User2UserMsgViewModel
    {
        public int Id { get; set; }
        public string RecieverId { get; set; }
        public string FulName { get; set; } 
        public string ImgUrl { get; set; } 
        public string SenderId { get; set; }
        public string SenderFulName { get; set; }
        public string SenderImgUrl { get; set; } 
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }
        public string FileName { get; set; }
    }
}
