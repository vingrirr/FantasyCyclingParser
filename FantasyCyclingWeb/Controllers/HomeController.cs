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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //List<Rider> all = Parser.ParseAllRiders(false);

            FantasyYearConfig c = Repository.FantasyYearConfigGetDefault();
            
            DashboardViewModel vm = new DashboardViewModel(c);                
            return View(vm);
        }

        public ActionResult RaceSeason()
        {



            List<PDC_Result> results = Parser.ParsePDCResults(DateTime.Now.Year);

            //List<PDC_Result> results = Repository.RaceResultsAll();
            results.Reverse();

            RaceSeasonViewModel vm = new RaceSeasonViewModel(DateTime.Now.Year, results);
            return View(vm);

        }

        public ActionResult HallOfFame()
        {

            //List<PDC_Result> results = Parser.ParsePDCResults(2018);

            List<PDC_Result> results = Repository.RaceResultsAll();
            results.Reverse();

            RaceSeasonViewModel vm = new RaceSeasonViewModel(2018, results);
            return View(vm);

        }

        public ActionResult ParseSeason()
        {

            Repository.ParseSeasonToDB(DateTime.Now.Year);      
            return View("");

        }

        public ActionResult AddConfig()
        {
            FantasyYearConfig vm = new FantasyYearConfig();
            return View(vm);

        }


        [HttpPost]
        public ActionResult AddConfig(FantasyYearConfig vm)
        {

            Repository.FantasyYearConfigInsert(vm);
            return View(vm);
        }

        [HttpPost]
        public JsonResult AddTeamToConfig(FantasyYearConfig vm)
        {

            Uri myUri = new Uri(vm.URLToAddTeam);
            string year = HttpUtility.ParseQueryString(myUri.Query).Get("y");
            string teamUID = HttpUtility.ParseQueryString(myUri.Query).Get("uid");

            Team t = Parser.ParseTeam(teamUID, Convert.ToInt32(year));

            TeamYear ty = new TeamYear(teamUID, Convert.ToInt32(year), t.TeamName);


            vm.TeamUIDS.Add(ty);

            var obj = new
            {
                //Message = "Success!  Your event has been created.",
                TeamListHTML = RenderHelper.PartialView(this, "_TeamYearList", vm.TeamUIDS),
                TeamListData = vm.TeamUIDS
            };

            return Json(obj);
   
        }


        public JsonResult GetData()
        {
            
            FantasyYearConfig c = new FantasyYearConfig();
            DashboardViewModel vm = new DashboardViewModel(c); 


            return Json(vm, JsonRequestBehavior.AllowGet);
        }


    }
}