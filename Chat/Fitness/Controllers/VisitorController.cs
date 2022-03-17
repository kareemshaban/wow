using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IFitnessRepositry<ApplicationUser> userManager;
        private readonly IFitnessRepositry<Visitor> db;
        public VisitorController(IFitnessRepositry<ApplicationUser> userManager,IFitnessRepositry<Visitor> Db)
        {
            this.userManager = userManager;
            this.db =Db;
        }
        [HttpPost("add")]
        public async Task<object> Follow(FollowBlockViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUser))
            {
                if (db.firstOrdefault(0, model.ApplicationUserId, model.NewUser) == null)
                {
                    Visitor f = new Visitor();
                    f.ApplicationUserId = model.ApplicationUserId;
                    f.date = DateTime.Now;
                    f.MainUserId = model.NewUser;
                    try
                    {
                        if (await db.Add(f))
                        {
                            return (new { message = "Success" });
                        }
                        return (new { message = "Error : User  not added" });
                    }
                    catch (Exception e)
                    {
                        return (new { message = "Error : Execption" + e.Message });
                    }
                }
                else
                    return (new { message = "Error : You are following this user befor" });
            }
            return (new { message = "Error : Data not complete" });
        }
    }
}
