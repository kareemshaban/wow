using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
     
    public class ChatRoomCategoryController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<ChatRoomCategory> db;
        private readonly IFitnessRepositry<ChatRoomMember> dbMembers;
        private readonly IFitnessRepositry<ChatRoom> dbRoom;
        private readonly IHostingEnvironment hostingEnvironment;

        public ChatRoomCategoryController(IFitnessRepositry<ChatRoomCategory> _db, IFitnessRepositry<ChatRoomMember> dbMembers,IFitnessRepositry<ChatRoom> dbRoom, IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.dbMembers = dbMembers;
            this.dbRoom = dbRoom;
            this.hostingEnvironment = hostingEnvironment;
        }      
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View(db.list());
        }
        //[Authorize]
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View();
        }
        // POST: ChatRoom/Create
        [HttpPost]
        public ActionResult Create(ChatRoomCategory cat,IFormFile file)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                if (!string.IsNullOrEmpty(cat.Name) )
                {
                    string fileName = string.Empty;
                    if (file != null)
                    {
                        fileName = DateTime.Now.Second + DateTime.Now.Millisecond +file.FileName;
                        string path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "categoryImgs");
                        path = System.IO.Path.Combine(path, fileName);
                        file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    cat.ImgUrl = fileName;
                    cat.CreateDate = DateTime.Now;
                    db.Add(cat);
                    return Redirect("/ChatRoomCategory/Index");
                }
                else
                    return View(cat);
            }
            catch
            {
                return View(cat);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id > 0)
            {
                ChatRoomCategory cat = db.firstOrdefault(id);
                return View(cat);
            }
            else
                return Redirect("/ChatRoomCategory/Index");
        }

        // POST: ChatRoom/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChatRoomCategory cat, IFormFile imgUploader)
        {
            try
            {
                if (cat.Id > 0)
                {
                    ChatRoomCategory update = db.firstOrdefault(cat.Id);
                    if (update != null)
                    {
                        string fileName = string.Empty;
                        if (imgUploader != null)
                        {
                            fileName = DateTime.Now.Second + DateTime.Now.Millisecond + imgUploader.FileName;
                            string path = hostingEnvironment.WebRootPath;
                            path = System.IO.Path.Combine(path, "categoryImgs");
                            path = System.IO.Path.Combine(path, fileName);
                            imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                        }
                        update.ImgUrl = !string.IsNullOrEmpty(fileName)?fileName: update.ImgUrl;
                        update.Name = cat.Name;                      
                        await db.update(update);
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(cat);
            }
            catch (Exception)
            {
                return View(cat);
            }
        }
        // POST: ChatRoom/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (await db.Delete(id, null, new List<ChatRoomCategory>()))
                {
                    return Json(new { msg = "Done" });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }

        #region 
        public ActionResult getAll()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            getAllClass model = new getAllClass()
            {
                rooms= dbRoom.IList(),
                members= dbMembers.list()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id  > 0)
            {
                ChatRoom c = dbRoom.list().FirstOrDefault(a => a.Id == id);
                if (c != null)
                {
                    return View(c);
                }
            }
            return Redirect("/ChatRoomCategory/getAll");
        }

        [HttpPost]
        public async Task<object> Update(string RoomName, IFormFile imgUploader,int id)
        {
            if (id > 0)
            {
                string fileName = string.Empty;
                if (imgUploader != null)
                {
                     fileName = DateTime.Now.Second + imgUploader.FileName;
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "RoomImgs");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                ChatRoom r = dbRoom.list().FirstOrDefault(a => a.Id == id);
                if (r != null)
                {
                    if (!string.IsNullOrEmpty(RoomName))
                    {
                        r.Name = RoomName;
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        r.ImgUrl = fileName;
                    }
                    if (await dbRoom.update(r))
                    {
                        return Redirect("/ChatRoomCategory/getAll");
                    }
                    else
                        return Redirect("/ChatRoomCategory/Update/" + id);
                }
            }
                    return Redirect("/ChatRoomCategory/getAll");
        }

        [HttpPost]
        public async Task<JsonResult> DeleteRoom(int id)
        {
            try
            {
                if (await dbRoom.Delete(id, null, new List<ChatRoom>()))
                {
                    return Json(new { msg = "Done" });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }
        #endregion
    }

    public class getAllClass
    {
        public IEnumerable<ChatRoom> rooms { get; set; }
        public List<ChatRoomMember> members { get; set; }
    }
}