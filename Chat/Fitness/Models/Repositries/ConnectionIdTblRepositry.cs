using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ConnectionIdTblRepositry:IFitnessRepositry<ConnectionIdTbl>
    {
        private readonly FitnessDbContext db;

        public ConnectionIdTblRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(ConnectionIdTbl entity)
        {
            await db.ConnectionIdTbl.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ConnectionIdTbl entity,List<ConnectionIdTbl> entities, string name = "", string name2 = "")
        {
            if (entity != null)
            {
                db.ConnectionIdTbl.Remove(entity);
                db.SaveChanges();
                return true;
            } else if (entities.Count > 0)
            {
                db.ConnectionIdTbl.RemoveRange(entities);
                db.SaveChanges();
                return true;
            } else
                return false;
        }
        public ConnectionIdTbl firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                ConnectionIdTbl msg = db.ConnectionIdTbl.FirstOrDefault(a => a.Id == id);
                if (msg != null)
                {
                    return msg;
                }
                else
                    return null;
            }
            else
            {
                if (!string.IsNullOrEmpty(name2))
                {
                    ConnectionIdTbl msg = db.ConnectionIdTbl.FirstOrDefault(a => a.ConnectionId == name2);
                    if (msg != null)
                    {
                        return msg;
                    }
                    else
                        return null;
                }
                else
                {
                    ConnectionIdTbl msg = db.ConnectionIdTbl.FirstOrDefault(a => a.ApplicationUserId == name);
                    if (msg != null)
                    {
                        return msg;
                    }
                    else
                        return null;
                }             
            }

        }
        public IEnumerable<ConnectionIdTbl> IList(string var = "", string name = "")
        {
            return null;
        }
        public List<ConnectionIdTbl> list(string var = "")
        {
            return db.ConnectionIdTbl.ToList();
        }
        public async Task<bool> update(ConnectionIdTbl entity)
        {
            ConnectionIdTbl msg = firstOrdefault(0,entity.ApplicationUserId);
            if (msg != null)
            {
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
                await Add(entity);
            return true;  

        }
    }
}
