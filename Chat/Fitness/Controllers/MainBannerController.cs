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
     
    public class MainBannerController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<MainBanner> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public MainBannerController(IFitnessRepositry<MainBanner> _db, IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.hostingEnvironment = hostingEnvironment;
        }
        //[Authorize]
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
        // POST: MainBanner/Create
        [HttpPost]
        public ActionResult Create(MainBanner banner, IFormFile MainImage, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {           
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                string path = string.Empty;
                string fileName = string.Empty;
                if (MainImage != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + MainImage.FileName;
                    path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MainBanner");
                    path = System.IO.Path.Combine(path, fileName);
                    MainImage.CopyTo(new FileStream(path, FileMode.Create));
                    banner.MainImage = fileName;
                }
                else
                {
                    ViewBag.mainImage = "لابد من اضافه صوره اساسية";
                    return View();
                }
                if (img1 != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img1.FileName;
                    path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MainBanner");
                    path = System.IO.Path.Combine(path, fileName);
                    img1.CopyTo(new FileStream(path, FileMode.Create));
                    banner.Img1 = fileName;
                }
                else
                {
                    ViewBag.image1 = "لابد من اضافه صوره واحده على الاقل";
                    return View();
                }
                //
                if (img2 != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img2.FileName;
                    path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MainBanner");
                    path = System.IO.Path.Combine(path, fileName);
                    img2.CopyTo(new FileStream(path, FileMode.Create));
                    banner.Img2 = fileName;
                }
                if (img3 != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img3.FileName;
                    path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MainBanner");
                    path = System.IO.Path.Combine(path, fileName);
                    img3.CopyTo(new FileStream(path, FileMode.Create));
                    banner.Img3 = fileName;
                }
                if (img4 != null)
                {
                    fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img4.FileName;
                    path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MainBanner");
                    path = System.IO.Path.Combine(path, fileName);
                    img4.CopyTo(new FileStream(path, FileMode.Create));
                    banner.Img4 = fileName;
                }
                banner.Id = 0;
                    db.Add(banner);
                    return Redirect("/MainBanner/index");               
            }
            catch
            {
                return View();
            }
        }

        // GET: MainBanner/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                MainBanner cat = db.firstOrdefault(id);
                return View(cat);
            }
            else
                return Redirect("/MainBanner/Index");
        }

        // POST: MainBanner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MainBanner banner, IFormFile MainImage, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            try
            {
                if (banner.Id > 0)
                {
                    MainBanner update = db.firstOrdefault(banner.Id);
                    if (update != null)
                    {
                        update.actions =!string.IsNullOrEmpty(banner.actions)?banner.actions :update.actions;
                        string path = string.Empty;
                        string fileName = string.Empty;
                        if (MainImage != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + MainImage.FileName;
                            path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "MainBanner");
                            path = System.IO.Path.Combine(path, fileName);
                            MainImage.CopyTo(new FileStream(path, FileMode.Create));
                            update.MainImage = fileName;
                        }                       
                        if (img1 != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img1.FileName;
                            path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "MainBanner");
                            path = System.IO.Path.Combine(path, fileName);
                            img1.CopyTo(new FileStream(path, FileMode.Create));
                            update.Img1 = fileName;
                        }                      
                        //
                        if (img2 != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img2.FileName;
                            path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "MainBanner");
                            path = System.IO.Path.Combine(path, fileName);
                            img2.CopyTo(new FileStream(path, FileMode.Create));
                            update.Img2 = fileName;
                        }
                        if (img3 != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img3.FileName;
                            path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "MainBanner");
                            path = System.IO.Path.Combine(path, fileName);
                            img3.CopyTo(new FileStream(path, FileMode.Create));
                            update.Img3 = fileName;
                        }
                        if (img4 != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + img4.FileName;
                            path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "MainBanner");
                            path = System.IO.Path.Combine(path, fileName);
                            img4.CopyTo(new FileStream(path, FileMode.Create));
                            update.Img4 = fileName;
                        }
                        //
                        await db.update(update);
                        return Redirect("/MainBanner/index");
                    }
                }
                return View(banner);
            }
            catch (Exception)
            {
                return View(banner);
            }
        }
        // POST: MainBanner/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id, null, new List<MainBanner>()))
                {
                    return Json(new { msg = "Done" });
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
