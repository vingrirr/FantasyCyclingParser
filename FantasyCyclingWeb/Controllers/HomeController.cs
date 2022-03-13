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


            List<PDCTeamPoints> PDCTeamData = new List<PDCTeamPoints>();
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);

            List<PDC_Result> results = season.RaceResults;

            List<PDCTeam> configTeams = new List<PDCTeam>();
            foreach (PDCTeamYear ty in config.TeamUIDS)
            {

                PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
                team.Is35Team = ty.Is35Team;
                configTeams.Add(team);
            }
            List<int> points = new List<int>();

            foreach (PDCTeam t in configTeams)
            {

                foreach (Rider r in t.Riders)
                {
               
                    int raceResults = season.RaceResults.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == r.PDC_RiderID)).Sum(g => g.Points);
                    points.Add(raceResults);
                }
                PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, points.Sum());
                PDCTeamData.Add(ptp);
                points.Clear();

            }

            DashboardViewModel vm = new DashboardViewModel(config, PDCTeamData, points, configTeams);


            return View(vm);
            return View(vm);
        }
        //public ActionResult NewIndex()
        //{


        //    List<PDCTeamPoints> PDCTeamData = new List<PDCTeamPoints>();
        //    FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
        //    PDC_Season season = Repository.PDCSeasonGet(config.Year);

        //    List<PDC_Result> results = season.RaceResults;

        //    List<PDCTeam> configTeams = new List<PDCTeam>();
        //    foreach (PDCTeamYear ty in config.TeamUIDS)
        //    {

        //        PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
        //        team.Is35Team = ty.Is35Team; 
        //        configTeams.Add(team);
        //    }
        //    List<int> points = new List<int>();

        //    foreach (PDCTeam t in configTeams)
        //    {

        //        foreach (Rider r in t.Riders)
        //        {
        //            //these are all the races they scored points in...but need to figure out how much they actually scored in each one...
        //            //List<PDC_Result> resultsForRider = (from res in season.RaceResults
        //            //                                    where res.RaceResults.Any(p => p.Rider_PDCID == r.PDC_RiderID)
        //            //                                    select res).ToList();

        //            int raceResults = season.RaceResults.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == r.PDC_RiderID)).Sum(g => g.Points);
        //            points.Add(raceResults);
        //        }
        //        PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, points.Sum());
        //        PDCTeamData.Add(ptp);
        //        points.Clear();

        //    }

        //    NewDashboardViewModel vm = new NewDashboardViewModel(config, PDCTeamData, points, configTeams);
            
            
        //    return View(vm);
        //}

        //public ActionResult RaceSeason()
        //{



        //    List<PDC_Result> results = Parser.ParsePDCResults(DateTime.Now.Year);

        //    //List<PDC_Result> results = Repository.RaceResultsAll();
        //    results.Reverse();

        //    RaceSeasonViewModel vm = new RaceSeasonViewModel(DateTime.Now.Year, results);
        //    return View(vm);

        //}

        public ActionResult RaceSeason()
        {


            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
                                           
            RaceSeasonViewModel vm = new RaceSeasonViewModel(season, config);
            return View(vm);

        }



        //public ActionResult HallOfFame()
        //{

        //    //List<PDC_Result> results = Parser.ParsePDCResults(2018);

        //    List<PDC_Result> results = Repository.RaceResultsAll();
        //    results.Reverse();

        //    RaceSeasonViewModel vm = new RaceSeasonViewModel(2018, results);
        //    return View(vm);

        //}

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


        //public JsonResult GetData()
        //{
            
        //    FantasyYearConfig c = new FantasyYearConfig();
        //    DashboardViewModel vm = new DashboardViewModel(c); 


        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ForceSeasonUpdate()
        {

            
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            season.UpdateResults();

            
            Repository.PDCSeasonUpdate(season);
            


            return Json("", JsonRequestBehavior.AllowGet);
        }


    }
}