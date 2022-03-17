using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class RolletMemberRepositry : IFitnessRepositry<RolletMember>
    {
        private readonly FitnessDbContext db;

        public RolletMemberRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(RolletMember entity)
        {
           await db.RolletMember.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, RolletMember entity,List<RolletMember> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.RolletMember.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public RolletMember firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                RolletMember cat = db.RolletMember.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                RolletMember cat = db.RolletMember.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<RolletMember> IList(string var = "", string name = "")
        {
            int rolletId = 0;
            if (int.TryParse(var,out rolletId))
            {
                return db.RolletMember.Include(a => a.ApplicationUser).Where(a=>a.RolletId== rolletId);
            }
            IEnumerable<RolletMember> xx = db.RolletMember.Include(a => a.ApplicationUser); 
            return  db.RolletMember.Include(a=>a.ApplicationUser);
        }
        public List<RolletMember> list(string var = "")
        {
            int rolletId = 0;
            if (int.TryParse(var ,out rolletId))
            {
                return db.RolletMember.Where(a => a.RolletId == rolletId).ToList();
            }
            else
            {
                if (!string.IsNullOrEmpty(var))
                {
                    return db.RolletMember.Where(a => a.ApplicationUserId == var).ToList();
                }
                else
                    return db.RolletMember.ToList();
            }
         
        }
        public async Task<bool> update(RolletMember entity)
        {
            RolletMember cat = firstOrdefault(entity.Id);
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
