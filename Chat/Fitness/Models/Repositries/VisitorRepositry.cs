using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.Repositries
{
    public class VisitorRepositry : IFitnessRepositry<Visitor>
    {        
            private readonly FitnessDbContext db;

            public VisitorRepositry(FitnessDbContext _db)
            {
                db = _db;
            }
            public async Task<bool> Add(Visitor entity)
            {
                await db.Visitor.AddAsync(entity);
                await db.SaveChangesAsync();
                return true;
            }
            public async Task<bool> Delete(int id, Visitor entity, List<Visitor> entities, string applicationUserId, string blockedUser)
            {
                Visitor user = firstOrdefault(0, applicationUserId, blockedUser);
                if (user != null)
                {
                    db.Visitor.Remove(user);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            public Visitor firstOrdefault(int id, string user, string blockedUser)
            {
                Visitor _user = db.Visitor.FirstOrDefault(a => a.ApplicationUserId == user && a.MainUserId == blockedUser);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;

            }
            public IEnumerable<Visitor> IList(string var = "", string name = "")
            {
                if (name == "AllFollowing")
                {
                    return db.Visitor.Include(a => a.MainUser).Where(a => a.ApplicationUserId == var);
                }
                else
                {
                    var dd = db.Visitor.Include(a => a.ApplicationUser).Where(a => a.MainUserId == var);
                    return db.Visitor.Include(a => a.ApplicationUser).Where(a => a.MainUserId == var);
                }
            }
            public List<Visitor> list(string var = "")
            {
                return db.Visitor.ToList();
            }
            public async Task<bool> update(Visitor entity)
            {
                Visitor user = firstOrdefault(0, entity.ApplicationUserId, entity.MainUserId);
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
