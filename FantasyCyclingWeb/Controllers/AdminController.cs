using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyCyclingParser;
using FantasyCyclingWeb.Models;
using Common;


namespace FantasyCyclingWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ConfigAdmin()
        {
            List<FantasyYearConfig> configs = Repository.FantasyYearConfigGetAll();
            return View();
        }
    }
}