using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FantasyCyclingWeb.Models;
using Common;
using FantasyCyclingParser;

namespace FantasyCyclingWeb.Models
{
    public class HallOfFameViewModel
    {
        public HallOfFameViewModel(List<FantasyYearConfig> configs)
        {

            List<PDC_Season> seasons = new List<PDC_Season>();
            List<DashboardViewModel> dashboards = new List<DashboardViewModel>();
            Entries = new List<HallOfFameModel>();
            //don't show current year's podium until next year
            configs.RemoveAll(x => x.Year == DateTime.Now.Year);

            foreach (FantasyYearConfig config in configs)
            {
                PDC_Season season = Repository.PDCSeasonGet(config.Year);
                seasons.Add(season);
                DashboardModel dm = new DashboardModel(config, season);
                DashboardViewModel dvm = new DashboardViewModel(dm);

                Entries.Add(new HallOfFameModel(dvm));
            }
        }

        public List<HallOfFameModel> Entries { get; set; }

    }

    public class HallOfFameModel
    {
        public HallOfFameModel(DashboardViewModel dvm)
        {
            Year = dvm.CurrentConfig.Year;
            Results = dvm.PDCTeamData;
        }

        public int Year { get; set; }
        public List<PDCTeamPoints> Results { get; set; }

    }
}