using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class UserProductRepositry : IFitnessRepositry<UserProduct>
    {
        private readonly FitnessDbContext db;
        private readonly IFitnessRepositry<Wallet> walletDb;
        private readonly IFitnessRepositry<Product> productDb;

        public UserProductRepositry(FitnessDbContext _db, IFitnessRepositry<Wallet> walletDb, IFitnessRepositry<Product> productDb)
        {
            db = _db;
            this.walletDb = walletDb;
            this.productDb = productDb;
        }
        public async Task<bool> Add(UserProduct entity)
        {
            Product p = productDb.firstOrdefault(entity.ProductId);
            Wallet w = walletDb.firstOrdefault(0, entity.UserDis);
            if (w != null && p != null)
            {
                if (w.Balance >= p.Price)
                {
                    //Take price form user wallet
                    w.Balance -= p.Price;
                    if (await walletDb.update(w))
                    {
                        try
                        {
                            entity.deleteDate = DateTime.Now;
                            entity.date = DateTime.Now;
                            entity.IsDeleted = false;
                            await db.UserProduct.AddAsync(entity);
                            await db.SaveChangesAsync();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            w.Balance += p.Price;
                            await walletDb.update(w);
                            return false;
                        }
                    }
                    else
                        return false;
                }             
            }
                return false;
        }
        public async Task<bool> Delete(int id, UserProduct entity,List<UserProduct> entities, string name = "", string name2 = "")
        {
            UserProduct x = firstOrdefault(0,name2, id.ToString());
            if (x != null)
            {
                if (name=="FromUserDataOrSearch")
                {
                    x.deleteDate = DateTime.Now;
                    x.IsDeleted = true;
                    db.Entry(x).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                db.UserProduct.Remove(x);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public UserProduct firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                UserProduct p = db.UserProduct.FirstOrDefault(a => a.ProductId == id && a.IsDeleted == false);
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
            }
            else
            {
                UserProduct p = db.UserProduct.FirstOrDefault(a => a.ApplicationUserId == name&& a.ProductId==int.Parse(name2) && a.IsDeleted== false);
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
            }
        }
        public IEnumerable<UserProduct> IList(string var = "", string name = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                var xx = db.UserProduct.Include(a => a.Product).Where(a=>a.ApplicationUserId==var).ToList();
                return db.UserProduct.Include(a => a.Product).Where(a => a.IsDeleted == false &&( a.NewUserId == var || a.ApplicationUserId== var));
            }else
                return db.UserProduct.Include(a => a.Product);
        }
        public List<UserProduct> list(string var = "")
        {
            return db.UserProduct.Where(a=>a.IsDeleted==false).ToList();
        }
        public async Task<bool> update(UserProduct entity)
        {
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;

        }
    }
}
