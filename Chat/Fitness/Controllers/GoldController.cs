using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
     
    public class GoldController : Controller
    {
        private readonly IFitnessRepositry<Gold> db;
        private readonly IFitnessRepositry<Category> catDb;

        public GoldController(IFitnessRepositry<Gold> _db, IFitnessRepositry<Category> catDb)
        {
            this.db = _db;
            this.catDb = catDb;
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
        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(Gold p)
        {
            p.CategoryId =await getGoldCategoryId();
            if (!string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
            {
                try
                {
                        await db.Add(p);
                        return Redirect("/Gold/Index");
                }
                catch
                {
                    return View(p);
                }
            }
            else
            {
                return View(p);
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
                Gold cat = db.firstOrdefault(id);
                return View(cat);
            }
            else
                return Redirect("/Gold/Index");
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Gold p)
        {
            try
            {
                if (p.CategoryId > 0 && !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                {
                    Gold update = db.firstOrdefault(p.Id);
                    if (update != null)
                    {
                        update.ProductName = p.ProductName;
                        update.Price = p.Price;
                        await db.update(update);
                        return Redirect("/Gold/Index");
                    }
                }
                return View(p);
            }
            catch (Exception)
            {
                return View(p);
            }
        }
        // POST: Category/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id,null,new List<Gold>()))
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
        private async Task<int> getGoldCategoryId()
        {
            Category cat = catDb.firstOrdefault(0, "EldahbDefault");
            if (cat != null)
                return cat.Id;
            else
            {
                Category eldhb = new Category()
                { CatName = "EldahbDefault", ImgUrl = "" };
                if (await catDb.Add(eldhb))
                    return eldhb.Id;
                else
                    return 0;
            }
        }
    }
}