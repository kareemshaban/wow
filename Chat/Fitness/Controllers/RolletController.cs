using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolletController : ControllerBase
    {
        public const decimal rate = .9M;
        private readonly IFitnessRepositry<Rollet> rolletDb;
        private readonly IFitnessRepositry<RolletMember> rolletMemberDb;
        private readonly IFitnessRepositry<Wallet> walletDb;

        public RolletController(IFitnessRepositry<Rollet> rolletDb, IFitnessRepositry<RolletMember> rolletMemberDb, IFitnessRepositry<Wallet> WalletDb)
        {
            this.rolletDb = rolletDb;
            this.rolletMemberDb = rolletMemberDb;
            walletDb = WalletDb;
        }

        [HttpPost("Members")] 
        public object Members(RolletMember rollet)
        {
            if (rollet.RolletId > 0)
            {
                var members= rolletMemberDb.IList().Select(a=>new {
                    Id=a.Id,
                    RolletId=a.RolletId,
                    FulName=a.ApplicationUser.FulName,
                    ImgUrl=a.ApplicationUser.ImgUrl
                }).ToList();
                if ( members.Count > 0 )
                {
                    return (new { message = "Success ", members = members });
                }
                else
                    return (new { message = "Error : Rollet dosn't contains members" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
         
         }
    }
