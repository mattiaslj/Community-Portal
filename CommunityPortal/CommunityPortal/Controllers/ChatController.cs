using CommunityPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        [HttpPost]
        public JsonResult GetMessages(string currentUser, string username)
        {
            //using(var _db = new ApplicationDbContext())
            //{
            //    var chats = _db.Chats;
            //    if(chats != null)
            //    {
            //        var chat = chats.Where(c => c.FirstUser == currentUser && c.SecondUser.UserName == username);
            //        if(chat != null)
            //        {
            //            return Json(chat);
            //        }
            //        var chat2 = chats.Where(c => c.FirstUser == username && c.SecondUser.UserName == currentUser);

            //        if (chat2 != null)
            //        {
            //            return Json(chat2);
            //        }
            //    }
            //}
            return Json("Something went wrong", JsonRequestBehavior.AllowGet);
        }
    }
}