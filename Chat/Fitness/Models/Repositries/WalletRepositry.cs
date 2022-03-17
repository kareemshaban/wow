using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class WalletRepositry : IFitnessRepositry<Wallet>
    {
        private readonly FitnessDbContext db;

        public WalletRepositry(FitnessDbContext _db)
        {
            db = _db;
        }

        public async Task<bool> Add(Wallet entity)
        {
            await db.Wallet.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Wallet entity,List<Wallet> entities, string name = "", string name2 = "")
        {
            if (!string.IsNullOrEmpty(name))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
            {
                List<Wallet> wallet_instances = db.Wallet.Where(a => a.ApplicationUserId.Equals(name)).ToList();
                db.Wallet.RemoveRange(wallet_instances);
                db.SaveChanges();
                return true;
            }
            var wallet = firstOrdefault(id);
            if (wallet != null)
            {
                db.Wallet.Remove(wallet);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Wallet firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Wallet wallet = db.Wallet.FirstOrDefault(a => a.Id == id);
                if (wallet != null)
                {
                    return wallet;
                }
                else
                    return null;
            }
            else
            {
                Wallet wallet = db.Wallet.FirstOrDefault(a => a.ApplicationUserId == name);
                if (wallet != null)
                {
                    return wallet;
                }
                else
                    return null;
            }
        }

        public List<Wallet> list(string var = "")
        {
            return db.Wallet.ToList();
        }
        public IEnumerable<Wallet> IList(string var = "", string name = "")
        {
            return db.Wallet.Include(a => a.ApplicationUserId);
        }
        public async Task<bool> update(Wallet entity)
        {
            db.Update(entity);
            await db.SaveChangesAsync();
            return true;

        }
    }
}
