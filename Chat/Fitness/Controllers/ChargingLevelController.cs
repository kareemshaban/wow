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
     
    public class ChargingLevelController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<ChargingLevel> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public ChargingLevelController(IFitnessRepositry<ChargingLevel> _db, IHostingEnvironment hostingEnvironment)
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
        // POST: ChargingLevel/Create
        [HttpPost]
        public ActionResult Create(ChargingLevel ChargingLevel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                if (!string.IsNullOrEmpty(ChargingLevel.LevelName) && ChargingLevel.BalanceCount > 0)
                {
                    string path = string.Empty;
                    string fileName = string.Empty;
                    if (ChargingLevel.Img != null)
                    {
                        fileName = DateTime.Now.Second + DateTime.Now.Millisecond + ChargingLevel.Img.FileName;
                        path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "ChargingLevelImg");
                        path = System.IO.Path.Combine(path, fileName);
                        ChargingLevel.Img.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    if (!string.IsNullOrEmpty(path))
                    {
                        ChargingLevel.ImgUrl = fileName;
                        db.Add(ChargingLevel);
                        return Redirect("/ChargingLevel/Index");
                    }
                    else
                    {
                        ChargingLevel.ImgUrl = "";
                        db.Add(ChargingLevel);
                        return Redirect("/ChargingLevel/Index");
                    }
                }
                else
                    return View(ChargingLevel);
            }
            catch
            {
                return View();
            }
        }

        // GET: ChargingLevel/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                ChargingLevel ChargingLevel = db.firstOrdefault(id);
                return View(ChargingLevel);
            }
            else
                return Redirect("/ChargingLevel/Index");
        }

        // POST: ChargingLevel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChargingLevel ChargingLevel)
        {
            try
            {
                if (ChargingLevel.Id > 0)
                {
                    ChargingLevel update = db.firstOrdefault(ChargingLevel.Id);
                    if (update != null)
                    {
                        update.Color = ChargingLevel.Color;
                        update.BalanceCount = ChargingLevel.BalanceCount;
                        update.LevelName = ChargingLevel.LevelName;
                        if (ChargingLevel.Img != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(ChargingLevel.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "ChargingLevelImg");
                                oldPath = Path.Combine(oldPath, ChargingLevel.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch { }
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + ChargingLevel.Img.FileName;
                            string path = Path.Combine(hostPath, "ChargingLevelImg");
                            path = Path.Combine(path, fileName);
                            await ChargingLevel.Img.CopyToAsync(new FileStream(path, FileMode.Create));
                            update.ImgUrl = fileName;
                        }
                        else
                        {
                            update.ImgUrl = ChargingLevel.ImgUrl;
                        }
                        await db.update(update);
                        return Redirect("/ChargingLevel/Index");
                    }
                }
                return View(ChargingLevel);
            }
            catch (Exception)
            {
                return View(ChargingLevel);
            }
        }
        // POST: ChargingLevel/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id,null,new List<ChargingLevel>()))
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