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
     
    public class CountryController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<Country> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public CountryController(IFitnessRepositry<Country> _db,IHostingEnvironment hostingEnvironment)
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
            return View();
        }
        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(Country cat,IFormFile imgUploader)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                //if (ShardClass.programType != 1 )
                //{
                //    return View();
                //}
                string path = string.Empty;
                string fileName = string.Empty;
                if (imgUploader != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                     path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "CountryImgs");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                if (!string.IsNullOrEmpty(path))
                {
                    cat.ImgUrl = fileName;
                    db.Add(cat);
                    return Redirect("/Country/Index");
                }
                else
                {
                    cat.ImgUrl = "";
                    db.Add(cat);
                    return Redirect("/Country/Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                Country cat=db.firstOrdefault(id);
                return View(cat);
            }else
            return Redirect("/Country/Index");
        }

        // POST: Country/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Country cat, IFormFile imgUploader)
        {
            try
            {
                //if (ShardClass.programType != 1)
                //{
                //    return View();
                //}
                if (cat.Id > 0)
                {
                    Country update = db.firstOrdefault(cat.Id);
                    if (update != null)
                    {
                        update.Name = cat.Name;
                        if (imgUploader != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(cat.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "CountryImgs");
                                oldPath = Path.Combine(oldPath, cat.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch {}
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                           string path = Path.Combine(hostPath, "CountryImgs");
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
        // POST: Country/Delete/5
        [HttpPost]
        public async Task <JsonResult> Delete(int id)
        {
            try
            {
                if ( await db.Delete(id,null,new List<Country>()))
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