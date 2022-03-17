using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletApiController : ControllerBase
    {
        private readonly IFitnessRepositry<Wallet> db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFitnessRepositry<ChargingLevel> dbCharging;

        public WalletApiController(IFitnessRepositry<Wallet> db, UserManager<ApplicationUser> userManager, IFitnessRepositry<ChargingLevel> dbCharging)
        {
            this.db = db;
            this.userManager = userManager;
            this.dbCharging = dbCharging;
        }
        [HttpPost("AddBalance")]
        public async Task<object> AddBalance(WalletViewModel wallet)
        {
            if (!string.IsNullOrEmpty(wallet.ApplicationUserId) && wallet.Balance > 0)
            {
                Wallet w = db.firstOrdefault(0, wallet.ApplicationUserId);
                if (w != null)
                {
                    w.Balance += wallet.Balance;
                    w.TotalBalance += wallet.Balance;
                    w.LastUpdate = DateTime.Now;
                    await db.update(w);
                        ChargingLevel x = dbCharging.list().Where(a => a.BalanceCount <= w.TotalBalance).LastOrDefault();
                        if (x != null)
                        {
                            ApplicationUser _user = await userManager.FindByIdAsync(wallet.ApplicationUserId);
                            _user.ChargingLevelId = x.Id;
                            await userManager.UpdateAsync(_user);
                        }
                    return (new { message = "Success", AlertMsg = "Balance Added successfully" });
                }
                return (new { message = "Error", AlertMsg = "There is problem in adding balance" });
            }
            else
            {
                return (new { message = "Error", AlertMsg = "Data not complete" });
            }

        }
        [HttpPost("UserWallet")]
        public WalletViewModel userWallet(WalletViewModel w)
        {
            if (!string.IsNullOrEmpty(w.ApplicationUserId))
            {

                Wallet x = db.firstOrdefault(0, w.ApplicationUserId);
                if (x != null)
                {
                    WalletViewModel wallet = new WalletViewModel()
                    {
                        ApplicationUserId = w.ApplicationUserId,
                        Balance = x.Balance,
                        Id = x.Id,
                        LastUpdate = x.LastUpdate,
                        TotalBalance = x.TotalBalance,
                        DiamonadBalance = x.DiamonadBalance
                    };
                    return wallet;
                }
                else
                    return new WalletViewModel();
            }
            else
            {
                return new WalletViewModel();
            }
        }
        [HttpPost("ResetDiamondBalance")]
        public async Task<object> ResetDiamondBalance(WalletViewModel w)
        {
            if (!string.IsNullOrEmpty(w.ApplicationUserId))
            {
                Wallet x = db.firstOrdefault(0, w.ApplicationUserId);
                if (x != null)
                {

                    x.Balance += x.DiamonadBalance;
                    x.TotalBalance += x.DiamonadBalance;
                    x.LastUpdate = DateTime.Now;
                    x.DiamonadBalance = 0;
                    await db.update(x);
                    return (new { message = "Success", Balance = x.Balance, AlertMsg = "Diamond balance reset successfully" });
                }
                else
                    return (new { message = "Error", AlertMsg = "Wallet not found" });
            }
            else
            {
                return (new { message = "Error", AlertMsg = "User not found" });
            }
        }
    }
}