using Library.InterfacesLogic;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shirirtoi.Controllers
{
    public class HomeController : Controller
    {
        private IUserProfileLogic UserProfile;

        public HomeController(IUserProfileLogic upl)
        {
            UserProfile = upl;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserProfileVM userProfile)
        {
            var user = UserProfile.Login(userProfile);
            if (user != null)
            {
                Session["UserName"] = user.Username;
                Session["ID"] = user.UserProfileID;
                Session["AccessLevel"] = user.RoleID;
                CreateCookie(user);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        private void CreateCookie(UserProfileVM userProfile)
        {
            FormsAuthentication.SetAuthCookie(userProfile.Username, true);
            var authTicket = new FormsAuthenticationTicket(1, userProfile.Username, DateTime.Now, DateTime.Now.AddMinutes(60), false, userProfile.Role.RoleName);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }
    }
}