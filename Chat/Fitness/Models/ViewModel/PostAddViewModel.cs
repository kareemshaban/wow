using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.ViewModel
{
    public class PostAddViewModel
    {
        public int Id { get; set; }
        public string content { get; set; }
        public string ApplicationUserId { get; set; }
        public IFormFile file { get; set; }
        public string fileName { get; set; }//    
        public DateTime publishDate { get; set; }
        public string ImgUrl { get; set; }
        #region User
        public string FulName { get; set; }
        public string UserImgUrl { get; set; }
        #endregion
        public List<likers> Likers { get; set; }
    }
    public class likers
    {
        public string ApplicationUserId { get; set; }
    }
}
