using CommunityPortal.Models;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class EventController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();

        public JsonResult GetEvents()
        {
            if (_db.Events != null)
            {
                var events = _db.Events.ToList();
                return Json(events, JsonRequestBehavior.AllowGet);
            }
            return Json("No events in database", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEvent(int Id)
        {
            var evnt = _db.Events.FirstOrDefault(e => e.Id == Id);

            if (evnt != null)
            {
                return Json(evnt);
            }
            return Json("");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult AddEvent(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _db.Events.Add(newEvent);
                _db.SaveChanges();
            }
            return Json("");
        }
    }
}
