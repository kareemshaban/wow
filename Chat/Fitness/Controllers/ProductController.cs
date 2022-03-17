using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Chat.Controllers
{
     
    public class ProductController : Controller
    {
        private readonly IFitnessRepositry<Product> db;
        private readonly IFitnessRepositry<Category> catDb;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProductController(IFitnessRepositry<Product> _db, IFitnessRepositry<Category> catDb, IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.catDb = catDb;
            this.hostingEnvironment = hostingEnvironment;
        }
        //[Authorize]
        public ActionResult Index()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            return View(db.IList("EldahbDefault"));
        }
        //[Authorize]
        public ActionResult Create()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            ViewBag.cats = fillCategory();
            return View();
        }
        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Product p, IFormFile imgUploader)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            if (p.CategoryId > 0 && !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
            {
                try
                {
                    string path = string.Empty;
                    string fileName = string.Empty;
                    if (imgUploader != null)
                    {
                        fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                        path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "productImgs");
                        path = System.IO.Path.Combine(path, fileName);
                        imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    if (!string.IsNullOrEmpty(path))
                    {
                        p.ImgUrl = fileName;
                        db.Add(p);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }else
            {
                ViewBag.cats = fillCategory();
                return View(p);
            }
        }
        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            if (id > 0)
            {
                ViewBag.cats = fillCategory();
                Product cat = db.firstOrdefault(id);
                return View(cat);
            }
            else
                return Redirect("/Product/Index");
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product p, IFormFile imgUploader)
        {
            try
            {
                if (  p.CategoryId > 0 && !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                {
                    Product update = db.firstOrdefault(p.Id);
                    if (update != null)
                    {
                        update.ProductName = p.ProductName;
                        update.Price = p.Price;
                        update.DaysCount = p.DaysCount;
                        update.CategoryId = p.CategoryId;
                        if (imgUploader != null)
                        {
                            string hostPath = hostingEnvironment.WebRootPath;
                            if (!string.IsNullOrEmpty(p.ImgUrl))
                            {
                                string oldPath = Path.Combine(hostPath, "productImgs");
                                oldPath = Path.Combine(oldPath, p.ImgUrl);
                                try
                                {
                                    System.IO.File.Delete(oldPath);
                                }
                                catch { }
                            }
                            string fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                            string path = Path.Combine(hostPath, "productImgs");
                            path = Path.Combine(path, fileName);
                            await imgUploader.CopyToAsync(new FileStream(path, FileMode.Create));
                            update.ImgUrl = fileName;
                        }
                        else
                        {
                            update.ImgUrl = p.ImgUrl;
                        }
                        await db.update(update);
                        return Redirect("/Product/Index");
                    }
                }
                ViewBag.cats = fillCategory();
                return View(p);
            }
            catch (Exception)
            {
                ViewBag.cats = fillCategory();
                return View(p);
            }
        }
        // POST: Category/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id,null,new List<Product>()))
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
        public List<Category> fillCategory()
        {
            List<Category> cats = catDb.list().Where(a => a.CatName != "EldahbDefault" && a.IsGift==false).ToList();
            cats.Insert(0, new Category { CatName = "يرجى اختيار فئه", Id = 0 });
            return cats;
        }
    }
}