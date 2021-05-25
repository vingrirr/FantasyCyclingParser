using FantasyCyclingParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyCyclingWeb.Models
{
    public class RaceSeasonViewModel
    {
        public RaceSeasonViewModel(int year, List<PDC_Result> results)
        {
            //List<List<LineDataPoint>> teamLineDataPoints = new List<List<LineDataPoint>>(); 
            //LineData = new List<List<LineDataPoint>>();

            ////List<FantasyResult> fr = new List<FantasyResult>();
            //FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            //List<Team> Teams = new List<Team>();
            //foreach (TeamYear ty in config.TeamUIDS)
            //{
            //    Team t = Parser.ParseTeam(ty.TeamUID, ty.Year);
            //    Teams.Add(t);

            //    //teamLineDataPoints.Add(new  )
            //}

            ////int sumPoints = 0;

            //foreach (PDC_Result r in results)
            //{

            //    FantasyResult f = new FantasyResult();
            //    f.Race = r;
            //    foreach (Team tm in Teams)
            //    {
            //        TeamPoints t = r.CompareTeamToRace(tm);
            //        t.Name = tm.PDCTeamName;
            //        f.Points.Add(t);

            //        FantasyResults.Add(f);
            //        sumPoints += t.Points;

            //    }
            //}




            KamnaChameleonPoints = new List<LineDataPoint>();
            BauhausMovementPoints = new List<LineDataPoint>();
            PlaidstockingsPoints = new List<LineDataPoint>();
            RubiconPoints = new List<LineDataPoint>();
            ZauzagePoints = new List<LineDataPoint>();
            CowboysPoints = new List<LineDataPoint>();

            //Results = Parser.ParsePDCResults(year);
            FantasyResults = new List<FantasyResult>();
            Team KamnaChameleon = Parser.ParseTeam("2483", year);
            Team TheBauhausMovement = Parser.ParseTeam("2534", year);
            Team Plaidstockings = Parser.ParseTeam("1881", year);
            Team Rubicon = Parser.ParseTeam("1191", year);
            Team Zauzage = Parser.ParseTeam("37", year);
            Team Cowboys = Parser.ParseTeam("2176", year);

            int kamnaSum = 0;
            int bauhausSum = 0;
            int plaidstockingsSum = 0;
            int rubiconSum = 0;
            int zauzageSum = 0;
            int cowboySum = 0;

            RaceCount = 0; 
            foreach (PDC_Result r in results)
            {

                FantasyResult f = new FantasyResult();

                f.Race = r;
                f.TempID = RaceCount; 

                TeamPoints t = r.CompareTeamToRace(KamnaChameleon);
                t.Name = "AlternativeFlats";
                kamnaSum += t.Points;
                t.RunningTotalPoints = kamnaSum; 
                
                f.Points.Add(t);
                //KamnaChameleonPoints.Add(raceCount.ToString(), kamnaSum.ToString());
                KamnaChameleonPoints.Add(new LineDataPoint(RaceCount, kamnaSum));


                t = r.CompareTeamToRace(TheBauhausMovement);
                t.Name = "BauhauseMovement";
                bauhausSum += t.Points;
                t.RunningTotalPoints = bauhausSum;
                f.Points.Add(t);
                BauhausMovementPoints.Add(new LineDataPoint(RaceCount, bauhausSum));


                t = r.CompareTeamToRace(Plaidstockings);
                t.Name = "Plaidstockings";
                plaidstockingsSum += t.Points;
                t.RunningTotalPoints = plaidstockingsSum;
                f.Points.Add(t);
                PlaidstockingsPoints.Add(new LineDataPoint(RaceCount, plaidstockingsSum));

                t = r.CompareTeamToRace(Rubicon);
                t.Name = "Rubicon";
                rubiconSum += t.Points;
                t.RunningTotalPoints = rubiconSum;
                f.Points.Add(t);
                RubiconPoints.Add(new LineDataPoint(RaceCount, rubiconSum));

                t = r.CompareTeamToRace(Zauzage);
                t.Name = "Zauzage";
                zauzageSum += t.Points;
                t.RunningTotalPoints = zauzageSum;                
                f.Points.Add(t);
                ZauzagePoints.Add(new LineDataPoint(RaceCount, zauzageSum));

                t = r.CompareTeamToRace(Cowboys);
                t.Name = "Cowboys";
                cowboySum += t.Points;
                t.RunningTotalPoints = cowboySum;
                f.Points.Add(t);
                CowboysPoints.Add(new LineDataPoint(RaceCount, cowboySum));

                FantasyResults.Add(f);
                RaceCount++;
            }

            MaxPointsRace = FantasyResults.OrderByDescending(x => x.Points.Max(y => y.Points)).First();
            MaxPointsTeam = MaxPointsRace.Points.OrderByDescending(x => x.Points).First();
          

        }

        public int RaceCount { get; set; }
        public List<FantasyResult> FantasyResults { get; set; }


        public List<List<LineDataPoint>> LineData { get; set; }




        public List<LineDataPoint> KamnaChameleonPoints { get; set; }
        public List<LineDataPoint> BauhausMovementPoints { get; set; }
        public List<LineDataPoint> PlaidstockingsPoints { get; set; }

        public List<LineDataPoint> RubiconPoints { get; set; }

        public List<LineDataPoint> ZauzagePoints { get; set; }
        public List<LineDataPoint> CowboysPoints { get; set; }

        public FantasyResult MaxPointsRace { get; set; }
        public TeamPoints MaxPointsTeam { get; set; }
    }

    public class LineDataPoint
    {
        public LineDataPoint()
        {

        }
        public LineDataPoint(int a, int b)
        {
            X = a;
            Y = b; 
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
