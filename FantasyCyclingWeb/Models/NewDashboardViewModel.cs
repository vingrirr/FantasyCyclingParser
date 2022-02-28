using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyCyclingParser;

namespace FantasyCyclingWeb.Models
{


    public class NewDashboardViewModel
    {
        public NewDashboardViewModel(FantasyYearConfig config, List<PDCTeamPoints> teamData, List<int> pts, List<PDCTeam> teams)
        {
            CurrentConfig = config;
            //'Kämna Chameleon', 'The Bauhaus Movement', 'Plaidstockings', 'Rubicon', 'Zauzage', 'Cowboys'

            PDCTeamData = teamData;
            Points = pts;
            PDCTeams = teams;
     

            PDCTeams = PDCTeams.OrderByDescending(x => x.TotalPointsScored).ToList();
            PDCTeamData = PDCTeamData.OrderByDescending(x => x.Points).ToList();


           // TeamUIDs.Reverse();


        }

        public FantasyYearConfig CurrentConfig { get; set; }

        public List<FantasyYearConfig> Configs { get; set; }
        public List<int> TeamUIDs { get; set; }
        public List<PDCTeamPoints> PDCTeamData { get; set; }
        public List<int> Points { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }
      

}

   
}
