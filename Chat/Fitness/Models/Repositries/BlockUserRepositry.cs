using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class BlockUserRepositry:IFitnessRepositry<BlockUser>
    {
        private readonly FitnessDbContext db;

        public BlockUserRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(BlockUser entity)
        {
            await db.BlockUser.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }     
        public async Task<bool> Delete(int id, BlockUser entity,List<BlockUser> entities, string applicationUserId,string blockedUser)
        {
            if (!string.IsNullOrEmpty(applicationUserId) && string.IsNullOrEmpty(blockedUser))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
            {
                List<BlockUser> removed_member_block = db.BlockUser.Where(a => a.ApplicationUserId.Equals(applicationUserId) || a.blockedUserId.Equals(applicationUserId)).ToList();
                db.BlockUser.RemoveRange(removed_member_block);
                db.SaveChanges();
                return true;
            }

            var user = firstOrdefault(0,applicationUserId,blockedUser);
            if (user != null)
            {
                db.BlockUser.Remove(user);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public BlockUser firstOrdefault(int id, string user,string blockedUser)
        {
                BlockUser _user = db.BlockUser.FirstOrDefault(a => a.ApplicationUserId == user && a.blockedUserId==blockedUser);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;

        }
        public IEnumerable<BlockUser> IList(string var = "", string name = "")
        {
            return db.BlockUser.Include(a => a.blockedUser).Where(a=>a.ApplicationUserId==var);
        }
        public List<BlockUser> list(string var = "")
        {
            return db.BlockUser.ToList();
        }
        public async Task<bool> update(BlockUser entity)
        {
            BlockUser user = firstOrdefault(0,entity.ApplicationUserId,entity.blockedUserId);
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
