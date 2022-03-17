using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChatRoomMsgRepositry:IFitnessRepositry<ChatRoomMsg>
    {
        private readonly FitnessDbContext db;

        public ChatRoomMsgRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(ChatRoomMsg entity)
        {
            await db.ChatRoomMsg.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChatRoomMsg entity,List<ChatRoomMsg> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
                db.ChatRoomMsg.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }else if (entity != null)
            {
                db.ChatRoomMsg.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(name))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
                {
                    List<ChatRoomMsg> room_member_msg = db.ChatRoomMsg.Where(a => a.SenderId.Equals(name)).ToList();
                    db.ChatRoomMsg.RemoveRange(room_member_msg);
                    db.SaveChanges();
                    return true;
                }

                ChatRoomMsg msg =await db.ChatRoomMsg.FirstOrDefaultAsync(a => a.Id == id);
                if (msg!= null)
                {
                    db.ChatRoomMsg.Remove(msg);
                    await db.SaveChangesAsync();
                    return true;
                }else
                    return false;

            }             
        }
        public ChatRoomMsg firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                ChatRoomMsg msg = db.ChatRoomMsg.FirstOrDefault(a => a.Id == id);
                if (msg != null)
                {
                    return msg;
                }
                else
                    return null;
            }
            else
            {
                ChatRoomMsg msg = db.ChatRoomMsg.Include(x=>x.Sender).FirstOrDefault(a => a.Msg.Contains(name));
                if (msg != null)
                {
                    return msg;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChatRoomMsg> IList(string var = "", string name = "")
        {
            if (!string.IsNullOrEmpty(var) && !string.IsNullOrEmpty(name))
            {
                return db.ChatRoomMsg.Include(a => a.Sender).Where(a => a.SenderId == var && a.ChatRoomId == int.Parse(name));
            }
            return db.ChatRoomMsg.Include(a => a.Sender);
        }
        public List<ChatRoomMsg> list(string var = "")
        {
            int roomId = 0;
            if (int.TryParse(var,out roomId))
            {
                return db.ChatRoomMsg.Where(a => a.ChatRoomId == roomId).ToList();
            }
            return db.ChatRoomMsg.ToList();
        }
        public async Task<bool> update(ChatRoomMsg entity)
        {
            ChatRoomMsg p = firstOrdefault(entity.Id);
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
