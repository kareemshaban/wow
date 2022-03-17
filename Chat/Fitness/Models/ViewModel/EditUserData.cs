using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.ViewModel
{
    public class EditUserData
    {
        public string DateBirth { get; set; }
        public string Gender { get; set; }//1 > Male    . 2>Female   . 3>Other
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public string ApplicationUserId { get; set; }
        public IFormFile file { get; set; }
        public string fileName { get; set; }//اسم الصوره  او الملف المرسل
        public string PathName { get; set; }
        public string Tower { get; set; }
        public int CountryId { get; set; }
    }
}
