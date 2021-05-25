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
                TeamData.Add(new TeamPoints(t.PDCTeamName, points));
            }

            Teams = Teams.OrderByDescending(x => x.TotalPointsScored).ToList();
            TeamData = TeamData.OrderByDescending(x => x.Points).ToList();
            //p = FantasyCyclingParser.Parser.GetTeamPoints(2483, config.Year);
            //TeamData.Add(new TeamPoints("Kämna Chameleon", p));

            //////////////////********** Read from the Config and do as a loop now! 

            //TeamUIDs.Add(2483); // Kämna Chameleon
            //TeamUIDs.Add(2534); // The Bauhaus Movement
            //TeamUIDs.Add(1881); //plaidstockings
            //TeamUIDs.Add(1191); // Rubicon
            //TeamUIDs.Add(37); //Zauzage
            //TeamUIDs.Add(1882); //Cowboys

            //TeamUIDs.Add(2211); // Krtecek_35
            //TeamUIDs.Add(2216); //Steven Kruijswijk’s Snowplows
            //TeamUIDs.Add(2843); // Team Taco Van
            //TeamUIDs.Add(2059); //CloserEncounter          
            //TeamUIDs.Add(2176); //Goldbar35

            TeamUIDs.Reverse();

            int p = 0;
            //////////////////********** Do as a loop now!!

            
            //p = FantasyCyclingParser.Parser.GetTeamPoints(2483, config.Year);
            //TeamData.Add(new TeamPoints("Kämna Chameleon", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2534, year);
            //TeamData.Add(new TeamPoints("The Bauhaus Movement",p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(1881, year);
            //TeamData.Add(new TeamPoints("Plaidstockings", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(1191, year);
            //TeamData.Add(new TeamPoints("Rubicon", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(37, year);
            //TeamData.Add(new TeamPoints("Zauzage", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2176, year);
            //TeamData.Add(new TeamPoints("Cowboys", p));

            ////Krtecek_35 = Parser.ParseTeam("2211");
            
            //p = FantasyCyclingParser.Parser.GetTeamPoints(2211, year); 
            //TeamData.Add(new TeamPoints("Krtecek_35", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2216, year);
            //TeamData.Add(new TeamPoints("Kruijswijks Snowplows", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2843, year);
            //TeamData.Add(new TeamPoints("Team Taco Van", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2059, year);
            //TeamData.Add(new TeamPoints("CloserEncounter", p));

            //p = FantasyCyclingParser.Parser.GetTeamPoints(2176, year);
            //TeamData.Add(new TeamPoints("Goldbar35", p));

            //TeamData = TeamData.OrderByDescending(x => x.Points).ToList();

            //foreach (int id in TeamUIDs)
            //{
            //    Points.Add(FantasyCyclingParser.Parser.GetTeamPoints(id));
            //}


            //////////////////********** Do as a loop now!!
            //KamnaChameleon = Parser.ParseTeam("2483", config.Year);
            //TheBauhausMovement = Parser.ParseTeam("2534", year);
            //Plaidstockings = Parser.ParseTeam("1881", year);
            //Rubicon = Parser.ParseTeam("1191", year);
            //Zauzage = Parser.ParseTeam("37", year);
            //Cowboys = Parser.ParseTeam("2176", year);
            //Krtecek_35 = Parser.ParseTeam("2211", year);
            //double fuglsangPts = Krtecek_35.Riders.Find(x => x.Name == "Jakob Fuglsang").CurrentYearPoints;
            //Krtecek_35.TotalPointsScored = Krtecek_35.TotalPointsScored; 

            //need to add 1885

            //KruijswijksSnowplows = Parser.ParseTeam("2216", CurrentConfig.Year);
            //TeamTacoVan = Parser.ParseTeam("2843", CurrentConfig.Year);
            //CloserEncounter = Parser.ParseTeam("2059", CurrentConfig.Year);
            //Goldbar35 = Parser.ParseTeam("2176", CurrentConfig.Year);

        }

        public FantasyYearConfig CurrentConfig { get; set; }

        public List<FantasyYearConfig> Configs { get; set; }
        public List<int> TeamUIDs { get; set; }
        public List<TeamPoints> TeamData { get; set; }
        public List<int> Points { get; set; }

        public List<Team> Teams { get; set; }
      

}

    public class SnapshotViewModel
    {
        public SnapshotViewModel()
        {
            int count = 1; 

            List<SeasonSnapshot> items = Repository.SnapshotGetAll();

            CloserEncounter = new List<SnapshotDataPoint>();
            Cowboys = new List<SnapshotDataPoint>();
            Goldbar35 = new List<SnapshotDataPoint>();
            KamnaGanna = new List<SnapshotDataPoint>();
            Krtecek35 = new List<SnapshotDataPoint>();
            Snowplows = new List<SnapshotDataPoint>();
            Plaidstockings = new List<SnapshotDataPoint>();
            Rubicon = new List<SnapshotDataPoint>();
            TacoVan = new List<SnapshotDataPoint>();
            TheBauhaus = new List<SnapshotDataPoint>();
            Zauzage = new List<SnapshotDataPoint>();
            //just use s.Teams[1] that way we know what team is waht

            MaxYValue = Convert.ToInt32(items.Last().Teams.Max(x => x.TotalPointsScored)) + 250;

            foreach (SeasonSnapshot s in items)
            {
                CloserEncounter.Add(new SnapshotDataPoint(count, s.Teams[0].TotalPointsScored));
                Cowboys.Add(new SnapshotDataPoint(count, s.Teams[1].TotalPointsScored));
                Goldbar35.Add(new SnapshotDataPoint(count, s.Teams[2].TotalPointsScored));
                KamnaGanna.Add(new SnapshotDataPoint(count, s.Teams[3].TotalPointsScored));
                Krtecek35.Add(new SnapshotDataPoint(count, s.Teams[4].TotalPointsScored));
                Snowplows.Add(new SnapshotDataPoint(count, s.Teams[5].TotalPointsScored));
                Plaidstockings.Add(new SnapshotDataPoint(count, s.Teams[6].TotalPointsScored));
                Rubicon.Add(new SnapshotDataPoint(count, s.Teams[7].TotalPointsScored));
                TacoVan.Add(new SnapshotDataPoint(count, s.Teams[8].TotalPointsScored));
                TheBauhaus.Add(new SnapshotDataPoint(count, s.Teams[9].TotalPointsScored));
                Zauzage.Add(new SnapshotDataPoint(count, s.Teams[10].TotalPointsScored));

                count++;

            }
        }

        public List<SnapshotDataPoint> CloserEncounter { get; set; }
        public List<SnapshotDataPoint> Cowboys { get; set; }
        public List<SnapshotDataPoint> Goldbar35 { get; set; }
        public List<SnapshotDataPoint> KamnaGanna { get; set; }
        public List<SnapshotDataPoint> Krtecek35 { get; set; }
        public List<SnapshotDataPoint> Snowplows { get; set; }
        public List<SnapshotDataPoint> Plaidstockings { get; set; }
        public List<SnapshotDataPoint> Rubicon { get; set; }
        public List<SnapshotDataPoint> TacoVan { get; set; }
        public List<SnapshotDataPoint> TheBauhaus { get; set; }
        public List<SnapshotDataPoint> Zauzage { get; set; }

        public int MaxYValue { get; set; }





    }
}
