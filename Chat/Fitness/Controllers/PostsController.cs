using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models.ViewModel;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Fitness.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<Like> dblike;
        private readonly IFitnessRepositry<Post> Db;
        private readonly IFitnessRepositry<SiteSetting> settingDb;
        private readonly IFitnessRepositry<Wallet> walletDb;

        public PostsController(IHostingEnvironment hostingEnvironment, IFitnessRepositry<Like> Dblike, IFitnessRepositry<Post> Db, IFitnessRepositry<SiteSetting> settingDb, IFitnessRepositry<Wallet> walletDb)
        {
            this.hostingEnvironment = hostingEnvironment;
            dblike = Dblike;
            this.Db = Db;
            this.settingDb = settingDb;
            this.walletDb = walletDb;
        }
        [HttpPost("UploadImage")]
        public object UploadImage(IFormFile userImage)
        {
            try
            {
                if (userImage != null)
                {
                    string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + userImage.FileName;
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "PostsImage");
                    path = System.IO.Path.Combine(path, fileName);
                    userImage.CopyTo(new FileStream(path, FileMode.Create));
                    return (new { message = "Success", FileName = fileName });
                }
                return (new { message = "Error : file not found" });
            }
            catch (Exception)
            {
                return (new { message = "Error : upload not complete." });
            }
        }
        [HttpPost("AddPost")]
        public async Task<object> AddPost([FromForm] PostAddViewModel model)
        {
            try
            {
                if (model.file != null)
                {
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "PostsImage");
                    path = System.IO.Path.Combine(path, model.fileName);
                    model.file.CopyTo(new FileStream(path, FileMode.Create));
                }
                Post post = new Post();
                post.ImgUrl = model.fileName;
                post.content = string.IsNullOrEmpty(model.content) ? "" : model.content;
                post.ApplicationUserId = model.ApplicationUserId;
                post.publishDate = DateTime.Now;
                SiteSetting setting = settingDb.firstOrdefault(0);
                Wallet wallet = walletDb.firstOrdefault(0, model.ApplicationUserId);
                if (setting != null && wallet != null)
                {
                    if (wallet.Balance >= setting.PostPrice)
                    {
                        if (await Db.Add(post))
                        {
                            wallet.Balance = wallet.Balance - setting.PostPrice;
                            try
                            {
                                if (await walletDb.update(wallet))
                                    return (new { message = "Success", Id = post.Id });
                                else
                                {
                                    await Db.Delete(post.Id, null, null);
                                    return (new { message = "Error : Not add", Id = post.Id });
                                }
                                   
                            }
                            catch (Exception)
                            {
                                await Db.Delete(post.Id, null, null);
                                return (new { message = "Error :Exception wallet", Id = post.Id });
                            }
                        }
                        return (new { message = "Error :  " });
                    }
                    return (new { message = "Error : Balance not  enought" });
                }
                else
                {
                    return (new { message = "Error : Data not complete" });
                }
            }
            catch (Exception x)
            {
                return (new { message = "Error :Exception" });
            }
        }

        [HttpGet("posts")]
        public List<PostAddViewModel> posts()
        {
            try
            {      
                List<Like> _likes = dblike.list();
                List< PostAddViewModel > _posts= Db.IList().Select(a => new PostAddViewModel { Id = a.Id, ApplicationUserId = a.ApplicationUserId, content = a.content, ImgUrl = a.ImgUrl, UserImgUrl = a.ApplicationUser.ImgUrl, FulName = a.ApplicationUser.FulName, publishDate = a.publishDate }).ToList();
                foreach (var item in _posts)
                {
                    item.Likers = _likes.Where(a => a.PostId == item.Id).Select(x => new likers { ApplicationUserId = x.ApplicationUserId }).ToList();
                }
                return _posts;
            }
            catch (Exception x)
            {
                return Db.IList().Select(a => new PostAddViewModel { content = a.content, publishDate = a.publishDate }).ToList();

            }
        }
        [HttpGet("UserPosts")]
        public List<PostAddViewModel> UsersPosts(UserInfoModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.ApplicationUserId))
                    return Db.list().Where(a => a.ApplicationUserId == model.ApplicationUserId).Select(a => new PostAddViewModel { ApplicationUserId = a.ApplicationUserId, content = a.content == null ? "" : a.content, ImgUrl = a.ImgUrl }).ToList();
                else
                    return new List<PostAddViewModel>();
            }
            catch (Exception)
            {
                return new List<PostAddViewModel>();
            }
        }
    }
}