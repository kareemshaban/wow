using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.ViewModel
{
    public class UploadFileviewModel
    {
        public IFormFile file { get; set; }
        public string fileName { get; set; }//اسم الصوره  او الملف المرسل
        public string PathName { get; set; }

    }
}
