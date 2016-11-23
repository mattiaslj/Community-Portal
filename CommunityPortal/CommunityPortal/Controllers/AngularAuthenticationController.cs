using CommunityPortal.Models;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class AngularAuthenticationController : Controller
    {
        // GET: AngularAuthentication
        public JsonResult GetCurrentUser()
        {
            using (var _db = new ApplicationDbContext())
            {
                var currentUser = User.Identity;

                if (currentUser != null)
                {
                    return Json(currentUser.Name, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("No current user...");
        }
    }
}