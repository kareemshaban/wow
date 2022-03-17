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
     
    public class EmosionController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<Emosion> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public EmosionController(IFitnessRepositry<Emosion> _db,IHostingEnvironment hostingEnvironment)
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
            return View(new Emosion());
        }
        // POST: Emosion/Create
        [HttpPost]
        public ActionResult Create(Emosion cat,IFormFile imgUploader)
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
                    path = System.IO.Path.Combine(path, "EmosionImgs");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                if (!string.IsNullOrEmpty(path))
                {
                    cat.ImgUrl = fileName;
                    db.Add(cat);
                    return Redirect("/Emosion/Index");
                }
                else
                {
                    cat.ImgUrl = "";
                    db.Add(cat);
                    return Redirect("/Emosion/Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Emosion/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                Emosion cat=db.firstOrdefault(id);
                return View(cat);
            }else
            return Redirect("/Emosion/Index");
        }

        // POST: Emosion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Emosion cat, IFormFile imgUploader)
        {
            try
            {
                if (cat.Id > 0)
                {
                    Emosion update = db.firstOrdefault(cat.Id);
                    if (update != null)
                    {
                        if (imgUploader != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(cat.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "EmosionImgs");
                                oldPath = Path.Combine(oldPath, cat.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch {}
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                           string path = Path.Combine(hostPath, "EmosionImgs");
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
                        return Redirect("/Emosion/Index");

                    }
                }
                return View(cat);
            }
            catch(Exception )
            {
                return View(cat);
            }
        }      
        // POST: Emosion/Delete/5
        [HttpPost]
        public async Task <JsonResult> Delete(int id)
        {
            try
            {
                if ( await db.Delete(id,null,new List<Emosion>()))
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