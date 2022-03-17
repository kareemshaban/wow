using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class RolesRepositry : IFitnessRepositry<IdentityRole>
    {
        private readonly FitnessDbContext db;

        public RolesRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public IdentityRole firstOrdefault(int id, string UserId = "", string name2 = "")
        {
            if (!string.IsNullOrEmpty(UserId) && id==0)
            {
                IdentityRole _user = db.Roles.FirstOrDefault(a => a.Name == UserId);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;
            }
            else
            {
                IdentityRole _user = db.Roles.FirstOrDefault(a => a.Id == name2);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;
            }
               
        }
        public IEnumerable<IdentityRole> IList(string var = "", string name = "")
        {
            return db.Roles;
        }
        public List<IdentityRole> list(string var = "")
        {
            return db.Roles.ToList();
        }
        public async Task<bool> update(IdentityRole entity)
        {
             db.Roles.Update(entity);
                return   true;
        }
        public async Task<bool> Delete(int id,IdentityRole x,List<IdentityRole> xx ,string name = "", string name2 = "")
        {
            return   true;
        }
        public async Task<bool> Add(IdentityRole entity)
        {
            return true;
        }
    }
}
