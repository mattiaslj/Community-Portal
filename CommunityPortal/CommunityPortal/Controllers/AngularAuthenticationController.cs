using System.Web.Mvc;

namespace CommunityPortal.Controllers
{

    public class AngularAuthenticationController : Controller
    {
        [Authorize]
        public JsonResult GetCurrentUser()
        {
            var currentUser = User.Identity;

            if (currentUser != null)
            {
                return Json(currentUser.Name, JsonRequestBehavior.AllowGet);
            }

            return Json("No current user...");
        }

        public JsonResult GetUserRole()
        {
            if (User.IsInRole("Admin"))
            {
                return Json("Admin", JsonRequestBehavior.AllowGet);
            }

            if (User.IsInRole("Moderator"))
            {
                return Json("Moderator", JsonRequestBehavior.AllowGet);
            }

            if (User.IsInRole("User"))
            {
                return Json("User", JsonRequestBehavior.AllowGet);
            }

            return Json("No Role", JsonRequestBehavior.AllowGet);
        }
    }
}