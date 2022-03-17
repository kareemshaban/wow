using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChatRoomCategoryRepositry:IFitnessRepositry<ChatRoomCategory>
    {
        private readonly FitnessDbContext db;

        public ChatRoomCategoryRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(ChatRoomCategory entity)
        {
            await db.ChatRoomCategory.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChatRoomCategory entity,List<ChatRoomCategory> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
                db.ChatRoomCategory.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }
            else if (entity != null)
            {
                db.ChatRoomCategory.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                ChatRoomCategory room = firstOrdefault(id);
                if (room != null)
                {
                    db.ChatRoomCategory.Remove(room);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public ChatRoomCategory firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                ChatRoomCategory room = db.ChatRoomCategory.FirstOrDefault(a => a.Id == id);
                if (room != null)
                {
                    return room;
                }
                else
                    return null;
            }
            else
            {
                ChatRoomCategory room = db.ChatRoomCategory.FirstOrDefault(a => a.Name == name);
                if (room != null)
                {
                    return room;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChatRoomCategory> IList(string var = "", string name = "")
        {
            return db.ChatRoomCategory;
        }
        public List<ChatRoomCategory> list(string var = "")
        {         
            return db.ChatRoomCategory.ToList();
        }
        public async Task<bool> update(ChatRoomCategory entity)
        {
            ChatRoomCategory p = firstOrdefault(entity.Id);
            if (p != null)
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
