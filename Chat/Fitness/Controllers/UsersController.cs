using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    public class UsersController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<ApplicationUser> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public UsersController(IFitnessRepositry<ApplicationUser> _db,IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.hostingEnvironment = hostingEnvironment;
        }
         
        public ActionResult Administration()// 
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
             return View(db.list());
        }
        public ActionResult Index()//  
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            return View(db.list());
        }
         
        public ActionResult Create()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            return View();
        }
  

    }
}