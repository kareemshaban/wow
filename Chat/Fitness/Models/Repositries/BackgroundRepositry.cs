using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class BackgroundRepositry : IFitnessRepositry<Background>
    {
        private readonly FitnessDbContext db;

        public BackgroundRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Background entity)
        {
           await db.Background.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Background entity,List<Background> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Background.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Background firstOrdefault(int id,string name="", string name2 = "")
        {
                Background cat = db.Background.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
        }
        public IEnumerable<Background> IList(string var = "", string name = "")
        {
                return db.Background;
        }
        public List<Background> list(string var = "")
        {
            return db.Background.ToList();
        }
        public async Task<bool> update(Background entity)
        {
            Background cat = firstOrdefault(entity.Id);
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
