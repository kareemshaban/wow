using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class ChatRoomMsgViewModel
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserImg { get; set; }
        public string UserFullName { get; set; }
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }
        public string FileName { get; set; }
        public int Type { get; set; }
    }
}
