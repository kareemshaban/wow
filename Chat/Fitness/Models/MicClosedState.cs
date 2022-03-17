using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class MicClosedState
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int MicId { get; set; }
    }
}
