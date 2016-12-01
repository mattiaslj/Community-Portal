using CommunityPortal.Models;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class ThreadController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public JsonResult GetThreads()
        {
            using (var _db = new ApplicationDbContext())
            {
                if (_db.ForumThreads != null)
                {
                    // NO GOOD SOLUTIION --- SHOULD USE _DB INSTEAD OF CONTEXT
                    var forumThreads = context.ForumThreads.Include("User").ToList();
                    return Json(forumThreads, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("No threads available", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThread(string id)
        {
            using (var _db = new ApplicationDbContext())
            {
                int result;
                int.TryParse(id, out result);
                var forumThread = _db.ForumThreads.FirstOrDefault(t => t.Id == result);

                if (forumThread != null)
                {
                    return Json(forumThread, JsonRequestBehavior.AllowGet);
                }

                return Json("Couldnt find thread", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public JsonResult AddThread(ForumThread forumThread, string username)
        {
            using (var _db = new ApplicationDbContext())
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    forumThread.User = user;
                }

                if (ModelState.IsValid && forumThread.User != null)
                {
                    _db.ForumThreads.Add(forumThread);
                    _db.SaveChanges();
                    return Json(forumThread.Id);
                }
                return Json("Couldnt add thread");
            }
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public JsonResult DeleteThread(string Id)
        {
            int result;
            int.TryParse(Id, out result);

            using (var _db = new ApplicationDbContext())
            {
                var thread = _db.ForumThreads.FirstOrDefault(t => t.Id == result);

                if (thread != null)
                {
                    _db.ForumThreads.Remove(thread);
                    _db.SaveChanges();
                    return Json("Successfully removed thread");
                }

                return Json("Something went wrong");
            }
        }
    }
}