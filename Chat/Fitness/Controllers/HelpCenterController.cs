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
     
    public class HelpCenterController : Controller
    {
        private readonly IFitnessRepositry<HelpCenter> db;

        public HelpCenterController(IFitnessRepositry<HelpCenter> db)
        {
            this.db = db;
        }      
        public ActionResult Index()
        {
                return View(db.list());
        }
     
    }
}