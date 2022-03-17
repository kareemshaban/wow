using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class RolletViewModel
    {
        public int Id { get; set; }
        public int MemberCount { get; set; }
        public decimal SubscribtionValue { get; set; }
       public List<RolletMemberViewModel> members { get; set; }
        public int ActualMemberCount { get; set; }
        //
        public int ChatRoomId { get; set; }
        public bool IsBegin { get; set; }
        public bool IsFinished { get; set; }
    }
}
