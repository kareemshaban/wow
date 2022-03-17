using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
     
    public class ShowPostsController : Controller
    {
        private readonly IFitnessRepositry<Post> Db;
        public ShowPostsController(IFitnessRepositry<Post> Db)
        {
            this.Db = Db;
        }
        [HttpGet]
        public IActionResult All()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            return View(Db.IList());
        }
        [HttpGet]
        public IActionResult Details(int id)
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
                Post p = Db.firstOrdefault(id);
                if (p != null)
                {
                    return View(p);
                }
                else
                {
                    return RedirectToAction("All");
                }
            }
            return RedirectToAction("All");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int postId)
        {
            if (postId >0 )
            {
                   if( await Db.Delete(postId, null, null))
                {
                    return Json(new { msg = "Done" });
                }
            }
            return Json(new { msg = "Error" });
        }

    }
}