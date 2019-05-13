using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        private IInterrface1 a;

        public HomeController(IInterrface1 a)
        {
            this.a = a;
        }

        public ActionResult Index()
        {
            ViewBag.Title = $"Home Page {this.a.Get()}";

            return View();
        }
    }
}
