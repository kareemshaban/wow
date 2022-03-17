using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class LikeRepositry:IFitnessRepositry<Like>
    {
        private readonly FitnessDbContext db;

        public LikeRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Like entity)
        {
            await db.Like.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Like entity, List<Like> entities, string name = "", string name2 = "")
        {
         
            if (id  > 0)
            {
                Like _like = firstOrdefault(id);
                db.Like.Remove(_like);
                await db.SaveChangesAsync();
                return true;
            }
            else if (entity != null)
            {
                db.Like.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Like firstOrdefault(int id, string name = "", string name2 = "")
        {
            int postId;
            if (string.IsNullOrEmpty(name))
            {
                Like cat = db.Like.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else if(int.TryParse(name2,out postId))
            {
                Like cat = db.Like.FirstOrDefault(a => a.PostId == postId && a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Like cat = db.Like.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }

        }
        public IEnumerable<Like> IList(string var = "", string name = "")
        {
            return db.Like.Include(a => a.Post);
        }
        public List<Like> list(string var = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.Like.Where(a=>a.ApplicationUserId == var).ToList();
            }
            return db.Like.ToList();
        }
        public async Task<bool> update(Like entity)
        {
            Like cat = firstOrdefault(entity.Id);
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
