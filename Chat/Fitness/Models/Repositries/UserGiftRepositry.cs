using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class UserGiftRepositry : IFitnessRepositry<UserGift>
    {
        private readonly FitnessDbContext db;
        private readonly IFitnessRepositry<SiteSetting> settingDb;
        private readonly IFitnessRepositry<Wallet> walletDb;
        private readonly IFitnessRepositry<Gift> giftDb;
        public UserGiftRepositry(FitnessDbContext _db, IFitnessRepositry<SiteSetting> settingDb, IFitnessRepositry<Wallet> walletDb, IFitnessRepositry<Gift> giftDb)
        {
            db = _db;
            this.settingDb = settingDb;
            this.walletDb = walletDb;
            this.giftDb = giftDb;
        }
        public async Task<bool> Add(UserGift entity)
        {
            Gift g = giftDb.firstOrdefault(entity.GiftId);
            Wallet w = walletDb.firstOrdefault(0, entity.ApplicationUserId);
            Wallet lastVersio = new Wallet()
            {
                ApplicationUserId = w.ApplicationUserId,
                Balance = w.Balance,
                DiamonadBalance = w.DiamonadBalance,
                Id = w.Id,
                LastUpdate = w.LastUpdate,
                TotalBalance = w.TotalBalance
            };
            if (w != null && g != null)
            {
                if (w.Balance >= g.Price)
                {
                    //Take price form user wallet
                    w.Balance -= g.Price;
                    w.DiamonadBalance += (settingDb.firstOrdefault(0).ConvertGift2Diamond / 100) * g.Price;
                    if (await walletDb.update(w))
                    {
                        try
                        {
                            //add gift to user
                            entity.Used = false;
                            entity.NewUserId = entity.NewUserId;// المرسل له الهديه 
                            entity.ApplicationUserId = entity.ApplicationUserId;// الراسل
                            await db.UserGift.AddAsync(entity);
                            db.SaveChanges();
                            return true;
                        }
                        catch (System.Exception e)
                        {

                            try
                            {
                                w.TotalBalance = lastVersio.TotalBalance;
                                w.Balance = lastVersio.Balance;
                                w.DiamonadBalance = lastVersio.DiamonadBalance;
                                await walletDb.update(w);
                                return false;
                            }
                            catch (System.Exception dc)
                            {

                                throw;
                            }
                        }

                    }
                    else
                        return false;
                }
            }
            return false;
        }
        public async Task<bool> Delete(int id, UserGift entity, List<UserGift> entities, string name = "", string name2 = "")
        {
            if (entity != null)
            {
                db.UserGift.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            UserGift x = firstOrdefault(id);
            if (x != null)
            {
                db.UserGift.Remove(x);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public UserGift firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                UserGift p = db.UserGift.FirstOrDefault(a => a.Id == id);
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
            }
            else
            {
                UserGift p = db.UserGift.FirstOrDefault(a => a.ApplicationUserId == name && a.NewUserId == name2 && a.Used == false);
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
            }
        }
        public IEnumerable<UserGift> IList(string var = "", string name = "")
        {
            if (var != "")
            {
                return db.UserGift.Include(a => a.Gift).
               Where(a => (a.NewUserId == var && a.Used == false));
            }
            else
            {
                return db.UserGift.Include(a => a.Gift).
               Where(a => (a.Used == false));
            }

        }
        public List<UserGift> list(string var = "")
        {
            return db.UserGift.ToList();
        }
        public async Task<bool> update(UserGift entity)
        {
            UserGift p = firstOrdefault(entity.Id, entity.ApplicationUserId, entity.GiftId.ToString());
            if (p != null)
            {
                entity.Used = false;
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
