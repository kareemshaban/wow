using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class ChatRoomMember
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime JoinDate { get; set; }
        public int MemberType { get; set; }// 0 >>Super && 1 >Ordinary user
        public bool IsBloked { get; set; }
        public DateTime BlockDate { get; set; }
        public int BlockedTime { get; set; }// هيتعمله بلوك  لكام يوم
        public bool IsBanned { get; set; }// طرد
        public DateTime BannedDate { get; set; }
        public bool IsInvited { get; set; }
        public DateTime InviteDate { get; set; }
        public bool IsExist { get; set; }
        public int MicId { get; set; }

    }
}
