using Library.Helpers;
using Library.InterfacesLogic;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shirirtoi.Controllers
{
    public class UserProfileController : Controller
    {
        private IUserProfileLogic UserProfile;

        public UserProfileController(IUserProfileLogic upl)
        {
            UserProfile = upl;
        }

        // GET: UserProfile View All UserProfiles
        [AuthorizeHelper(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(UserProfile.GetAllUserProfile());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserProfileVM userProfile)
        {
            if (ModelState.IsValid && userProfile.Username != userProfile.Password)
            {
                UserProfile.Register(userProfile);
                return RedirectToAction("Login", "Home", new { userProfile = userProfile });
            }
            else
            {
                return View();
            }
        }
        
        [AuthorizeHelper(Roles = "User, PowerUser, Administrator")]
        public ActionResult ViewUserProfile(int userProfileID)
        {
            return View(UserProfile.GetUserProfileByID(userProfileID));
        }

        [HttpGet]
        [AuthorizeHelper(Roles = "User, PowerUser, Administrator")]
        public ActionResult UpdateUserProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUserProfile(UserProfileVM userProfile)
        {
            if (ModelState.IsValid && userProfile.Username != userProfile.Password)
            {
                UserProfile.UpdateUserProfile(userProfile);
                return RedirectToAction("ViewUserProfile", "UserProfile", new { userProfileID = userProfile.UserProfileID });
            }
            else
            {
                return View();
            }
        }
        
        [AuthorizeHelper(Roles = "Administrator")]
        public ActionResult Demote(int userProfileID)
        {
            UserProfile.Demote(userProfileID);
            return RedirectToAction("Index", "UserProfile");
        }
        
        [AuthorizeHelper(Roles = "Administrator")]
        public ActionResult Promote(int userProfileID)
        {
            UserProfile.Promote(userProfileID);
            return RedirectToAction("Index", "UserProfile");
        }
    }
}