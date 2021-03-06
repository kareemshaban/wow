using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
     
    public class levelController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<Level> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public levelController(IFitnessRepositry<Level> _db, IHostingEnvironment hostingEnvironment)
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
        // POST: Level/Create
        [HttpPost]
        public ActionResult Create(Level level)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                if (!string.IsNullOrEmpty(level.LevelName) && level.GiftSendCount > 0)
                {
                    string path = string.Empty;
                    string fileName = string.Empty;
                    if (level.Img != null)
                    {
                        fileName = DateTime.Now.Second + DateTime.Now.Millisecond + level.Img.FileName;
                        path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "LevelImg");
                        path = System.IO.Path.Combine(path, fileName);
                        level.Img.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    if (!string.IsNullOrEmpty(path))
                    {
                        level.ImgUrl = fileName;
                        db.Add(level);
                        return Redirect("/Level/Index");
                    }
                    else
                    {
                        level.ImgUrl = "";
                        db.Add(level);
                        return Redirect("/Level/Index");
                    }
                }
                else
                    return View(level);
            }
            catch
            {
                return View();
            }
        }

        // GET: Level/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                Level level = db.firstOrdefault(id);
                return View(level);
            }
            else
                return Redirect("/Level/Index");
        }

        // POST: Level/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Level level)
        {
            try
            {
                if (level.Id > 0)
                {
                    Level update = db.firstOrdefault(level.Id);
                    if (update != null)
                    {
                        update.Color = level.Color;
                        update.GiftSendCount = level.GiftSendCount;
                        update.LevelName = level.LevelName;
                        if (level.Img != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(level.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "LevelImg");
                                oldPath = Path.Combine(oldPath, level.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch { }
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + level.Img.FileName;
                            string path = Path.Combine(hostPath, "LevelImg");
                            path = Path.Combine(path, fileName);
                            await level.Img.CopyToAsync(new FileStream(path, FileMode.Create));
                            update.ImgUrl = fileName;
                        }
                        else
                        {
                            update.ImgUrl = level.ImgUrl;
                        }
                        await db.update(update);
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(level);
            }
            catch (Exception)
            {
                return View(level);
            }
        }
        // POST: Level/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id,null,new List<Level>()))
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