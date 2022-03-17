using Fitness.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class FollowUserRepositry:IFitnessRepositry<FollowUser>
    {
        private readonly FitnessDbContext db;

        public FollowUserRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(FollowUser entity)
        {
            await db.FollowUser.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, FollowUser entity,List<FollowUser> entities, string applicationUserId, string blockedUser)
        {
            FollowUser user = firstOrdefault(0, applicationUserId, blockedUser);
            if (user != null)
            {
                db.FollowUser.Remove(user);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public FollowUser firstOrdefault(int id, string user, string blockedUser)
        {
            if (string.IsNullOrEmpty(blockedUser))
            {
                // خاص بالجزء بتاع حذف المستخدم من التطبيق  وبالتالى  لازم نحذف المستخدم من اى حاجه موجود فيها
                foreach (var item in db.FollowUser.Where(a => a.ApplicationUserId == user || a.FollowedUserId == user).ToList())
                {
                    db.Remove(item);
                    db.SaveChanges();
                }
                return null;
            }
            else
            {
                FollowUser _user = db.FollowUser.FirstOrDefault(a => a.ApplicationUserId == user && a.FollowedUserId == blockedUser);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;
            }
        }
        public IEnumerable<FollowUser> IList(string var = "", string name = "")
        {
            if (name == "AllFollowing")
            {
                return db.FollowUser.Include(a => a.FollowedUser).Where(a=>a.ApplicationUserId==var);
            }else if (var == "All")
            {
                return db.FollowUser.Include(a => a.FollowedUser).Include(a => a.ApplicationUser);
            }
            else
            {
                var dd= db.FollowUser.Include(a => a.ApplicationUser).Where(a => a.FollowedUserId == var);
                return db.FollowUser.Include(a => a.ApplicationUser).Where(a => a.FollowedUserId == var);
            }
        }
        public List<FollowUser> list(string var = "")
        {
            return db.FollowUser.ToList();
        }
        public async Task<bool> update(FollowUser entity)
        {
            FollowUser user = firstOrdefault(0, entity.ApplicationUserId, entity.FollowedUserId);
            if (user != null)
            {
                db.Update(user);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }
    }
}
