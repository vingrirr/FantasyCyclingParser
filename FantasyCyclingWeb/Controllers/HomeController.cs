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

            //Repository.ParseSeasonToDB(DateTime.Now.Year);      
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
        public JsonResult AddPDCTeamToConfig(FantasyYearConfig vm)
        {

            Uri myUri = new Uri(vm.URLToAddPDCTeam);
            string year = HttpUtility.ParseQueryString(myUri.Query).Get("y");
            string PDCTeamUID = HttpUtility.ParseQueryString(myUri.Query).Get("uid");

            PDCTeam t = Parser.ParsePDCTeam(PDCTeamUID, Convert.ToInt32(year));

            PDCTeamYear ty = new PDCTeamYear(PDCTeamUID, Convert.ToInt32(year), t.PDCTeamName);


            vm.TeamUIDS.Add(ty);

            var obj = new
            {
                //Message = "Success!  Your event has been created.",
                PDCTeamListHTML = RenderHelper.PartialView(this, "_PDCTeamYearList", vm.TeamUIDS),
                PDCTeamListData = vm.TeamUIDS
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