using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChatRoomRepositry:IFitnessRepositry<ChatRoom>
    {
        private readonly FitnessDbContext db;

        public ChatRoomRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(ChatRoom entity)
        {
            await db.ChatRoom.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChatRoom entity,List<ChatRoom> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
                db.ChatRoom.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }
            else if (entity != null)
            {
                db.ChatRoom.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(name))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
                {
                    List<ChatRoom> chat_rooms_of_user = db.ChatRoom.Where(a => a.ApplicationUserId.Equals(name)).ToList();
                    db.ChatRoom.RemoveRange(chat_rooms_of_user);
                    db.SaveChanges();
                    return true;
                }

                ChatRoom room = firstOrdefault(id);
                if (room != null)
                {
                    db.ChatRoom.Remove(room);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public ChatRoom firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name) )
            {
                ChatRoom room = db.ChatRoom.FirstOrDefault(a => a.Id == id);
                if (room != null)
                {
                    return room;
                }
                else
                    return null;
            }
            else if(string.IsNullOrEmpty(name2))
            {
                ChatRoom room = db.ChatRoom.FirstOrDefault(a => a.Name == name);
                if (room != null)
                {
                    return room;
                }
                else
                    return null;
            }
            else
            {
                ChatRoom room = db.ChatRoom.FirstOrDefault(a => a.ApplicationUserId == name2);
                if (room != null)
                {
                    return room;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChatRoom> IList(string var = "", string name = "")
        {
            return db.ChatRoom.Include(a => a.ApplicationUser);
        }
        public List<ChatRoom> list(string var = "")
        {         
            return db.ChatRoom.ToList();
        }
        public async Task<bool> update(ChatRoom entity)
        {
            ChatRoom p = firstOrdefault(entity.Id);
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
