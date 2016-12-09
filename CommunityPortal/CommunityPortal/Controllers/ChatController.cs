using CommunityPortal.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        [HttpPost]
        public JsonResult GetMessages(string currentUser, string username)
        {
            using (var _db = new ApplicationDbContext())
            {
                var chats = _db.Messages;
                if (chats != null)
                {
                    var chat = chats.Where(c => c.Username == currentUser && c.ReceiverUsername == username).ToList();
                    chat.AddRange(chats.Where(c => c.Username == username && c.ReceiverUsername == currentUser).ToList());
                    if (chat.Count > 0)
                    {
                        chat.Sort((x, y) => DateTime.Compare(x.Time, y.Time));
                        return Json(chat.Distinct());
                    }
                }
            }
            return Json("Something went wrong");
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddMessage(string currentUser, string receiver, string message)
        {
            using (var _db = new ApplicationDbContext())
            {
                if (User.Identity.Name == currentUser)
                {
                    var messageReceiver = _db.Users.FirstOrDefault(u => u.UserName == receiver);

                    if (messageReceiver != null && message != null && message.Count() > 0)
                    {
                        var chatMessage = new ChatMessage();
                        chatMessage.Message = message;
                        chatMessage.Username = currentUser;
                        chatMessage.Time = DateTime.Now;
                        chatMessage.ReceiverUsername = receiver;
                        _db.Messages.Add(chatMessage);
                        _db.SaveChanges();
                        return Json("Success");
                    }
                }
            }
            return Json("Something went wrong");
        }
    }
}