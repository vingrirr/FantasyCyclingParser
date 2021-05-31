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
            PDCTeamData = new List<PDCTeamPoints>();
            Points = new List<int>();
            PDCTeams = new List<PDCTeam>();


            foreach (PDCTeamYear ty in config.TeamUIDS)
            {
                PDCTeam t = Parser.ParsePDCTeam(ty.TeamUID, ty.Year);
                PDCTeams.Add(t);

                int points = FantasyCyclingParser.Parser.GetPDCTeamPoints(Convert.ToInt32(ty.TeamUID), ty.Year);
                PDCTeamData.Add(new PDCTeamPoints(t.PDCTeamName, points));
            }

            PDCTeams = PDCTeams.OrderByDescending(x => x.TotalPointsScored).ToList();
            PDCTeamData = PDCTeamData.OrderByDescending(x => x.Points).ToList();
      

           TeamUIDs.Reverse();

        
        }

        public FantasyYearConfig CurrentConfig { get; set; }

        public List<FantasyYearConfig> Configs { get; set; }
        public List<int> TeamUIDs { get; set; }
        public List<PDCTeamPoints> PDCTeamData { get; set; }
        public List<int> Points { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }
      

}

   
}
