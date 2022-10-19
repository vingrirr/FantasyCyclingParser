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
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {

            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            
            PDC_Season season = Repository.PDCSeasonGet(config.Year);

            RaceSeasonViewModel rs = new RaceSeasonViewModel(season, config);

            List<string> raceList = new List<string>();

            BarChartRaceViewModel vm = new BarChartRaceViewModel(); 
            foreach (FantasyResult fr in rs.FantasyResults)
            {
                vm.RaceList.Add(fr.Race.EventName);
                BarChartRaceItem item = new BarChartRaceItem();
                item.Name = fr.Race.EventName;

                foreach (PDCTeamPoints tp in fr.Points)
                {
                    //BarChartDataPoint d = new BarChartDataPoint();
                    KeyValuePair<string, int> kvp = new System.Collections.Generic.KeyValuePair<string, int>(tp.Name, tp.RunningTotalPoints);
                    
                    //d.Key = tp.Name;
                    //d.Value = tp.RunningTotalPoints;
                    item.Items.Add(kvp);
                }

                vm.BarChartData.Add(item);
                
            }

            return View(vm);
        }
        public ActionResult Year(int id)
        {

            
            FantasyYearConfig config = Repository.FantasyYearConfigGetByYear(id);

            PDC_Season season = Repository.PDCSeasonGet(config.Year);


            RaceSeasonViewModel rs = new RaceSeasonViewModel(season, config);

            List<string> raceList = new List<string>();

            BarChartRaceViewModel vm = new BarChartRaceViewModel();
            foreach (FantasyResult fr in rs.FantasyResults)
            {
                vm.RaceList.Add(fr.Race.EventName);
                BarChartRaceItem item = new BarChartRaceItem();
                item.Name = fr.Race.EventName;

                foreach (PDCTeamPoints tp in fr.Points)
                {
                    //BarChartDataPoint d = new BarChartDataPoint();
                    
                    KeyValuePair<string, int> kvp = new System.Collections.Generic.KeyValuePair<string, int>(tp.Name, tp.RunningTotalPoints);

                    //d.Key = tp.Name;
                    //d.Value = tp.RunningTotalPoints;
                    item.Items.Add(kvp);
                }

                vm.BarChartData.Add(item);

            }

            return View("Index", vm);
        }
    }
}