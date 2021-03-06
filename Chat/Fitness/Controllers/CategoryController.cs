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
     
    public class CategoryController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<Category> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public CategoryController(IFitnessRepositry<Category> _db,IHostingEnvironment hostingEnvironment)
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
            if (User.IsInRole("ChargingAgency"))
            {

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
            return View();
        }
        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category cat,IFormFile imgUploader)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (cat.CatName == "EldahbDefault")
                return View(cat);
            try
            {
                string path = string.Empty;
                string fileName = string.Empty;
                if (imgUploader != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                     path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "categoryImgs");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                if (!string.IsNullOrEmpty(path))
                {
                    cat.ImgUrl = fileName;
                    db.Add(cat);
                    return Redirect("/Category/Index");
                }
                else
                {
                    cat.ImgUrl = "";
                    db.Add(cat);
                    return Redirect("/Category/Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                Category cat=db.firstOrdefault(id);
                return View(cat);
            }else
            return Redirect("/Category/Index");
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Category cat, IFormFile imgUploader)
        {
            try
            {
                if (cat.Id > 0)
                {
                    Category update = db.firstOrdefault(cat.Id);
                    if (update != null)
                    {
                        update.CatName = cat.CatName;
                        update.IsGift = cat.IsGift;
                        if (imgUploader != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(cat.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "categoryImgs");
                                oldPath = Path.Combine(oldPath, cat.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch {}
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                           string path = Path.Combine(hostPath, "categoryImgs");
                            path = Path.Combine(path,fileName);
                            await imgUploader.CopyToAsync(new FileStream(path, FileMode.Create));
                            update.ImgUrl = fileName;                          
                        }
                        else
                        {
                            update.ImgUrl = cat.ImgUrl;
                        }
                        await db.update(update);
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(cat);
            }
            catch(Exception )
            {
                return View(cat);
            }
        }      
        // POST: Category/Delete/5
        [HttpPost]
        public async Task <JsonResult> Delete(int id)
        {
            try
            {
                if ( await db.Delete(id,null,new List<Category>()))
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