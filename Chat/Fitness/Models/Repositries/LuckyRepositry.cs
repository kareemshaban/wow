using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class LuckyRepositry : IFitnessRepositry<Lucky>
    {
        private readonly FitnessDbContext db;

        public LuckyRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Lucky entity)
        {
           await db.Lucky.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Lucky entity,List<Lucky> entities, string name = "", string name2 = "")
        {
            if (entity!= null)
            {
                db.Lucky.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                var cat = firstOrdefault(id);
                if (cat != null)
                {
                    db.Lucky.Remove(cat);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
          
        }
        public Lucky firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Lucky cat = db.Lucky.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Lucky cat = db.Lucky.FirstOrDefault(a => a.ChatRoomId ==int.Parse( name));
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<Lucky> IList(string var = "", string name = "")
        {
           return  db.Lucky.Include(a=>a.ApplicationUser);
        }
        public List<Lucky> list(string var = "")
        {
            return db.Lucky.ToList();
        }
        public async Task<bool> update(Lucky entity)
        {
            Lucky cat = firstOrdefault(entity.Id);
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
