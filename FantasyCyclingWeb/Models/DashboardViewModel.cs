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
        public DashboardViewModel(FantasyYearConfig config)
        {
            CurrentConfig = config; 
            //'Kämna Chameleon', 'The Bauhaus Movement', 'Plaidstockings', 'Rubicon', 'Zauzage', 'Cowboys'
            TeamUIDs = new List<int>();
            TeamData = new List<TeamPoints>();
            Points = new List<int>();
            Teams = new List<Team>();


            foreach (TeamYear ty in config.TeamUIDS)
            {
                Team t = Parser.ParseTeam(ty.TeamUID, ty.Year);
                Teams.Add(t);

                int points = FantasyCyclingParser.Parser.GetTeamPoints(Convert.ToInt32(ty.TeamUID), ty.Year);
                TeamData.Add(new TeamPoints(t.TeamName, points));
            }

            Teams = Teams.OrderByDescending(x => x.TotalPointsScored).ToList();
            TeamData = TeamData.OrderByDescending(x => x.Points).ToList();
      

            TeamUIDs.Reverse();

        
        }

        public FantasyYearConfig CurrentConfig { get; set; }

        public List<FantasyYearConfig> Configs { get; set; }
        public List<int> TeamUIDs { get; set; }
        public List<TeamPoints> TeamData { get; set; }
        public List<int> Points { get; set; }

        public List<Team> Teams { get; set; }
      

}

   
}
