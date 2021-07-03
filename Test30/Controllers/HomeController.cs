using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test30.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ProductsPage()
        {
            ViewBag.Message = "Your products page.";

            return View();
        }
        public ActionResult ComingSoon()
        {
            ViewBag.Message = "Coming Soon Page";

            return View();
        }
        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ Page";

            return View();
        }
    }
}