using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class ConnectionIdTbl
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime createAt { get; set; }
    }
}
