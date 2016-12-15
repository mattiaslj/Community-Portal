using CommunityPortal.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            using (var _db = new ApplicationDbContext())
            {
                var model = _db.Users.ToList(); ;
                return View(model);
            }
        }


        public ActionResult ShowUser(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }
            using (var _db = new ApplicationDbContext())
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == name);
                if (user != null)
                {
                    return PartialView("UserView", user);
                }

                ViewBag.error = "No user";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditUser(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }

            using (var _db = new ApplicationDbContext())
            {
                var model = _db.Users.FirstOrDefault(u => u.UserName == name);
                if (model == null)
                {
                    ViewBag.error = "No user";
                    return RedirectToAction("Index");
                }
                return PartialView("EditUser", model);
            }
        }

        [HttpPost]
        public ActionResult EditUser(string name, string city, string country, string image)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }
            using (var _db = new ApplicationDbContext())
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == name);

                user.City = city;
                user.Country = country;
                user.Image = image;

                _db.SaveChanges();

                return RedirectToAction("ShowUser", user);
            }
        }

        [HttpGet]
        public ActionResult LockUser(string name)
        {
            if (name == null)
            {
                ViewBag.error = "no user";
                return RedirectToAction("Index");
            }

            using (var _db = new ApplicationDbContext())
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == name);
                if (user == null)
                {
                    ViewBag.error = "no user";
                    return View("Index");
                }

                // TODO: make lockout permanent and add more lockout options
                user.LockoutEnabled = true;
                user.LockoutEndDateUtc = DateTime.UtcNow.AddDays(365 * 200);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}