using CommunityPortal.Models;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class EventController : Controller
    {

        //ApplicationDbContext _db = new ApplicationDbContext();

        public JsonResult GetEvents()
        {
            using (var _db = new ApplicationDbContext())
            {
                if (_db.Events != null)
                {
                    var events = _db.Events.ToList();
                    return Json(events, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("No events in database", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEvent(int Id)
        {
            using (var _db = new ApplicationDbContext())
            {
                var evnt = _db.Events.FirstOrDefault(e => e.Id == Id);
                if (evnt != null)
                {
                    return Json(evnt);
                }
            }
            return Json("");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public JsonResult AddEvent(Event newEvent)
        {
            using (var _db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    // used when user edit a post
                    if (newEvent.Id != 0)
                    {
                        var evnt = _db.Events.FirstOrDefault(e => e.Id == newEvent.Id);
                        if (evnt != null)
                        {
                            evnt.Title = newEvent.Title;
                            evnt.Body = newEvent.Body;
                            evnt.PostTime = newEvent.PostTime;
                            _db.SaveChanges();
                        }
                    }
                    else
                    {   // new post to be added
                        _db.Events.Add(newEvent);
                        _db.SaveChanges();
                    }
                }
            }
            return Json("");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public JsonResult DeleteEvent(string id)
        {
            using (var _db = new ApplicationDbContext())
            {
                int result;
                int.TryParse(id, out result);
                var evnt = _db.Events.FirstOrDefault(e => e.Id == result);
                if (evnt != null)
                {
                    _db.Events.Remove(evnt);
                    _db.SaveChanges();
                }
            }
            return Json("");
        }

    }
}
