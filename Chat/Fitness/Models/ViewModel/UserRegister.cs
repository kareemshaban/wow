using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class UserRegister
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public int  Gender { get; set; }//1 > Male    . 2>Female   . 3>Other
        [Required]
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public string Id { get; set; }
        public int UserId { get; set; }
        public string roleName { get; set; }
        public int CountryId { get; set; }
    }
}
