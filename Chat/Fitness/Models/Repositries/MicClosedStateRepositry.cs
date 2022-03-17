using Chat.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class MicClosedStateRepositry : IFitnessRepositry<MicClosedState>
    {
        private readonly FitnessDbContext db;

        public MicClosedStateRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(MicClosedState entity)
        {
            await db.MicClosedState.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, MicClosedState entity,List<MicClosedState> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
               db.MicClosedState.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }
            else if (entity != null)
            {
                db.MicClosedState.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                MicClosedState room = firstOrdefault(id);
                if (room != null)
                {
                   db.MicClosedState.Remove(room);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public MicClosedState firstOrdefault(int id, string name = "", string name2 = "")
        {
            try
            {
                int chatRoomId = 0;
                if (int.TryParse(name, out chatRoomId) && id > 0)
                {
                    MicClosedState room = db.MicClosedState.FirstOrDefault(a => a.MicId == id && a.ChatRoomId == chatRoomId);
                    if (room != null)
                    {
                        return room;
                    }
                }
            }
            catch (Exception sx)
            {

                throw;
            }
           
                return null;
        }
        public IEnumerable<MicClosedState> IList(string var = "", string name = "")
        {
            return null;
        }
        public List<MicClosedState> list(string var = "")
        {
            if (string.IsNullOrEmpty(var))
            {
                return db.MicClosedState.ToList();
            }
            return db.MicClosedState.Where(a=>a.ChatRoomId == int.Parse(var)).ToList();
        }
        public async Task<bool> update(MicClosedState entity)
        {
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;
        }
    }
}
