using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Music
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string MusicName { get; set; }
        [NotMapped]
        public IFormFile MusicFile { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
