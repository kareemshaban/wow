using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class GiftRepositry:IFitnessRepositry<Gift>
    {
        private readonly FitnessDbContext db;

        public GiftRepositry(FitnessDbContext _db)
        {
            db = _db;
        }

        public async Task<bool> Add(Gift entity)
        {
            await db.Gift.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Gift entity, List<Gift> entities, string name = "", string name2 = "")
        {
            var gift = firstOrdefault(id);
            if (gift != null)
            {
                db.Gift.Remove(gift);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public Gift firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Gift cat = db.Gift.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Gift cat = db.Gift.FirstOrDefault(a => a.GiftName == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
        }

        public List<Gift> list(string var = "")
        {
            return db.Gift.ToList();
        }
        public IEnumerable<Gift> IList(string var = "EldahbDefault", string name = "")
        {
            return db.Gift.Include(a => a.Category).Where(a => a.Category.CatName != var);
        }
        public async Task<bool> update(Gift entity)
        {
            Gift cat = firstOrdefault(entity.Id);
            if (cat != null)
            {
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }
    }
}
