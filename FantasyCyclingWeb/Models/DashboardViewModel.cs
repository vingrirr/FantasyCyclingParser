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
            PDCTeamData = teamData;
            Points = pts;
            PDCTeams = teams;
     

            PDCTeams = PDCTeams.OrderByDescending(x => x.PDCTeamName).ToList();
            PDCTeamData = PDCTeamData.OrderByDescending(x => x.Points).ToList();
           
        }

        public DashboardViewModel(DashboardModel model)
        {
            CurrentConfig = model.CurrentConfig;            
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
                    team.CalculatePoints(results); 
                    PDCTeams.Add(team);
                }
            }
            
            foreach (PDCTeam t in PDCTeams)
            {

                PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, t.TotalPointsScored);
                PDCTeamData.Add(ptp);                
            }

        }

        public DashboardModel(FantasyYearConfig config, PDC_Season season, bool isDraft)
        {
            PDCTeams = new List<PDCTeam>();
            PDCTeamData = new List<PDCTeamPoints>();
            CurrentConfig = config;
            List<PDC_Result> results = season.RaceResults;

            foreach (PDCTeam team in season.DraftTeams)
            {
                
                if (team != null && team.Riders.Count() > 0)
                {
                    team.Is35Team = false;
                    team.CalculatePoints(results);
                    PDCTeams.Add(team);
                }
            }

            foreach (PDCTeam t in PDCTeams)
            {

                PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, t.TotalPointsScored);
                PDCTeamData.Add(ptp);
            }

        }

        public FantasyYearConfig CurrentConfig { get; set; }
        
        public List<PDCTeamPoints> PDCTeamData { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }


    }


}
