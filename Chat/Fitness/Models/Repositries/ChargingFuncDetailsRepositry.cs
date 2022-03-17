using Chat.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChargingFuncDetailsRepositry : IFitnessRepositry<ChargingFuncDetails>
    {
        private readonly FitnessDbContext db;

        public ChargingFuncDetailsRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(ChargingFuncDetails entity) 
        {
            await db.ChargingFuncDetail.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChargingFuncDetails entity,List<ChargingFuncDetails> entities, string name = "", string name2 = "")
        {
            var ChargingFuncDetails = firstOrdefault(id);
            if (ChargingFuncDetails != null)
            {
                db.ChargingFuncDetail.Remove(ChargingFuncDetails);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public ChargingFuncDetails firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if (id > 0)
                {
                    ChargingFuncDetails ChargingFuncDetails = db.ChargingFuncDetail.FirstOrDefault(a => a.Id == id);
                    if (ChargingFuncDetails != null)
                    {
                        return ChargingFuncDetails;
                    }
                    else
                        return null;
                }
                else
                {
                    ChargingFuncDetails ChargingFuncDetails = db.ChargingFuncDetail.FirstOrDefault();
                    if (ChargingFuncDetails != null)
                    {
                        return ChargingFuncDetails;
                    }
                    else
                        return null;
                }
            }
            else
            {
                ChargingFuncDetails ChargingFuncDetails = db.ChargingFuncDetail.FirstOrDefault(a => a.Guid == name);
                if (ChargingFuncDetails != null)
                {
                    return ChargingFuncDetails;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChargingFuncDetails> IList(string var = "", string name = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.ChargingFuncDetail.Include(a => a.ApplicatioUser);
            }
            else
            {
                return db.ChargingFuncDetail.Include(a => a.User);
            }
        }
        public List<ChargingFuncDetails> list(string var="")
        {
            return db.ChargingFuncDetail.ToList();
        }
        public async Task<bool> update(ChargingFuncDetails entity)
        {
            ChargingFuncDetails ChargingFuncDetails = firstOrdefault(entity.Id);
            if (ChargingFuncDetails != null)
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
