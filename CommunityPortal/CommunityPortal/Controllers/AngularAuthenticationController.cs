using CommunityPortal.Models;
using System.Collections.Generic;
using System.Linq;
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

        [Authorize]
        public JsonResult GetUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            using (var _db = new ApplicationDbContext())
            {
                // Get all admins
                var admin = (from r in _db.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
                var admins = _db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(admin.Id)).ToList();
                // Get all Moderators
                var mod = (from r in _db.Roles where r.Name.Contains("Moderator") select r).FirstOrDefault();
                var mods = _db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(mod.Id)).ToList();
                // Get all users
                var user = (from r in _db.Roles where r.Name.Contains("User") select r).FirstOrDefault();
                var dbUsers = _db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(user.Id)).ToList();

                var forumPosts = _db.ForumPosts.ToList();
                var forumThreads = _db.ForumThreads.ToList();
                if (admins != null)
                {
                    foreach (var item in admins)
                    {
                        var vm = new UserViewModel();

                        vm.Email = item.Email;
                        vm.UserName = item.UserName;
                        vm.UserRole = "Admin";
                        users.Add(vm);
                    }
                }

                if (mods != null)
                {
                    foreach (var item in mods)
                    {
                        var vm = new UserViewModel();

                        vm.Email = item.Email;
                        vm.UserName = item.UserName;
                        vm.UserRole = "Moderator";
                        users.Add(vm);
                    }
                }

                if (dbUsers != null)
                {
                    foreach (var item in dbUsers)
                    {
                        var vm = new UserViewModel();

                        vm.Email = item.Email;
                        vm.UserName = item.UserName;
                        vm.UserRole = "User";
                        users.Add(vm);
                    }
                }
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}


//  var dbUsers = _db.Users.Include("Roles");
//if (dbUsers != null)
//{
//    List<UserViewModel> users = new List<UserViewModel>();
//    foreach (var user in dbUsers)
//    {
//        var vm = new UserViewModel();

//        vm.UserName = user.UserName;
//        vm.Email = user.Email;

//       var temp = user.Roles.FirstOrDefault(x => x.UserId == user.Id);
//       if(temp == admin)
//        {
//            vm.UserRole = "Admin";
//        }

//    }