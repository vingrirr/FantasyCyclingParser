using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyCyclingParser;

namespace FantasyCyclingWeb.Models
{


    public class DashboardViewModel
    {
        public DashboardViewModel(FantasyYearConfig config, List<PDCTeamPoints> teamData, List<int> pts, List<PDCTeam> teams)
        {
            CurrentConfig = config;
            //'Kämna Chameleon', 'The Bauhaus Movement', 'Plaidstockings', 'Rubicon', 'Zauzage', 'Cowboys'

            PDCTeamData = teamData;
            Points = pts;
            PDCTeams = teams;
     

            PDCTeams = PDCTeams.OrderByDescending(x => x.TotalPointsScored).ToList();
            PDCTeamData = PDCTeamData.OrderByDescending(x => x.Points).ToList();
           
        }

        public DashboardViewModel(DashboardModel model)
        {
            CurrentConfig = model.CurrentConfig;
            //'Kämna Chameleon', 'The Bauhaus Movement', 'Plaidstockings', 'Rubicon', 'Zauzage', 'Cowboys'

            PDCTeamData = model.PDCTeamData;
            PDCTeams = model.PDCTeams;


            PDCTeams = PDCTeams.OrderByDescending(x => x.TotalPointsScored).ToList();
            PDCTeamData = PDCTeamData.OrderByDescending(x => x.Points).ToList();

        }

        public FantasyYearConfig CurrentConfig { get; set; }

        public List<FantasyYearConfig> Configs { get; set; }
        public List<int> TeamUIDs { get; set; }
        public List<PDCTeamPoints> PDCTeamData { get; set; }
        public List<int> Points { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }
      

    }


    public class DashboardModel
    {
        public DashboardModel(FantasyYearConfig config, PDC_Season season)
        {


            PDCTeams = new List<PDCTeam>();
            PDCTeamData = new List<PDCTeamPoints>();
            CurrentConfig = config;            
            List<PDC_Result> results = season.RaceResults;

            foreach (PDCTeamYear ty in config.TeamUIDS)
            {

                PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
                if (team != null)
                {
                    team.Is35Team = ty.Is35Team;
                    PDCTeams.Add(team);
                }
            }
            List<int> points = new List<int>();

            foreach (PDCTeam t in PDCTeams)
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

        }

        public FantasyYearConfig CurrentConfig { get; set; }
        
        public List<PDCTeamPoints> PDCTeamData { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }


    }


}
