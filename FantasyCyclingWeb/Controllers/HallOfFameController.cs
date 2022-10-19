using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyCyclingWeb.Controllers
{
    public class HallOfFameController : Controller
    {
        // GET: HallOfFame
        public ActionResult Index()
        {
            return View();
        }
    }
}