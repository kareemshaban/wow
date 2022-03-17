using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class UserImageLikeRepositry : IFitnessRepositry<UserImageLike>
    {
        private readonly FitnessDbContext db;

        public UserImageLikeRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(UserImageLike entity)
        {
           await db.UserImageLike.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, UserImageLike entity,List<UserImageLike> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.UserImageLike.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public UserImageLike firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                UserImageLike cat = db.UserImageLike.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                UserImageLike cat = db.UserImageLike.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<UserImageLike> IList(string var = "", string name = "")
        {
            if (string.IsNullOrEmpty(var ) && string.IsNullOrEmpty(name))
            {
                return db.UserImageLike.Include(a => a.ApplicationUser);
            }
            else 
            {
                return db.UserImageLike.Where(a=>a.ApplicationUserId == var ).Include(a => a.Liker);
            }

        }
        public List<UserImageLike> list(string var = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.UserImageLike.Where(a => a.ApplicationUserId == var).ToList();
            }
            return db.UserImageLike.ToList();
        }
        public async Task<bool> update(UserImageLike entity)
        {
            UserImageLike cat = firstOrdefault(entity.Id);
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
