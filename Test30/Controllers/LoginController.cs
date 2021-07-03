using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test30.Models;

namespace Test30.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Test30.Models.User userModel)
        {
            using (Test30Entities db = new Test30Entities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {

                    return View("Index", userModel);
                }
                else
                {
                    Session["usedID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

        }
        public ActionResult LogOut()
        {
            int userID = (int)Session["usedID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }


    }
}