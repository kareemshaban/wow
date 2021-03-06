using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
     
    public class BackgroundController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<Background> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public BackgroundController(IFitnessRepositry<Background> _db,IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.hostingEnvironment = hostingEnvironment;
        }      
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View(db.list());
        }
        //[Authorize]
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View(new Background());
        }
        // POST: Background/Create
        [HttpPost]
        public ActionResult Create(Background cat,IFormFile imgUploader)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                if (imgUploader== null)
                {
                    return View(cat);
                }
                string path = string.Empty;
                string fileName = string.Empty;
                if (imgUploader != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                     path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "BackgroundImgs");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                if (!string.IsNullOrEmpty(path))
                {
                    cat.ImgUrl = fileName;
                    db.Add(cat);
                    return Redirect("/Background/Index");
                }
                else
                {
                    cat.ImgUrl = "";
                    db.Add(cat);
                    return Redirect("/Background/Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Background/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                Background cat=db.firstOrdefault(id);
                return View(cat);
            }else
            return Redirect("/Background/Index");
        }

        // POST: Background/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Background cat, IFormFile imgUploader)
        {
            try
            {
                if (cat.Id > 0)
                {
                    Background update = db.firstOrdefault(cat.Id);
                    if (update != null)
                    {
                        if (imgUploader != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(cat.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "BackgroundImgs");
                                oldPath = Path.Combine(oldPath, cat.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch {}
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                           string path = Path.Combine(hostPath, "BackgroundImgs");
                            path = Path.Combine(path,fileName);
                            await imgUploader.CopyToAsync(new FileStream(path, FileMode.Create));
                            update.ImgUrl = fileName;                          
                        }
                        else
                        {
                            update.ImgUrl = cat.ImgUrl;
                        }
                        update.Price = cat.Price;
                        await db.update(update);
                        return Redirect("/Background/Index");

                    }
                }
                return View(cat);
            }
            catch(Exception )
            {
                return View(cat);
            }
        }      
        // POST: Background/Delete/5
        [HttpPost]
        public async Task <JsonResult> Delete(int id)
        {
            try
            {
                if ( await db.Delete(id,null,new List<Background>()))
                {
                    return Json(new {msg="Done" });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }
    }
}