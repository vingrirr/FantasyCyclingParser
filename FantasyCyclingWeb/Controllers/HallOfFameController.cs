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
    public class HallOfFameController : Controller
    {
        // GET: HallOfFame
        public ActionResult Index()
        {
            
            List<FantasyYearConfig> configs = Repository.FantasyYearConfigGetAll();            
            configs = configs.OrderByDescending(x => x.Year).ToList();

            HallOfFameViewModel vm = new HallOfFameViewModel(configs);

            return View(vm);
        }
    }
}