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
            //List<List<LineDataPoint>> PDCTeamLineDataPoints = new List<List<LineDataPoint>>(); 
            //LineData = new List<List<LineDataPoint>>();

            ////List<FantasyResult> fr = new List<FantasyResult>();
            //FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            //List<PDCTeam> PDCTeams = new List<PDCTeam>();
            //foreach (PDCTeamYear ty in config.PDCTeamUIDS)
            //{
            //    PDCTeam t = Parser.ParsePDCTeam(ty.PDCTeamUID, ty.Year);
            //    PDCTeams.Add(t);

            //    //PDCTeamLineDataPoints.Add(new  )
            //}

            ////int sumPoints = 0;

            //foreach (PDC_Result r in results)
            //{

            //    FantasyResult f = new FantasyResult();
            //    f.Race = r;
            //    foreach (PDCTeam tm in PDCTeams)
            //    {
            //        PDCTeamPoints t = r.ComparePDCTeamToRace(tm);
            //        t.Name = tm.PDCPDCTeamName;
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
            PDCTeam KamnaChameleon = Parser.ParsePDCTeam("2483", year);
            PDCTeam TheBauhausMovement = Parser.ParsePDCTeam("2534", year);
            PDCTeam Plaidstockings = Parser.ParsePDCTeam("1881", year);
            PDCTeam Rubicon = Parser.ParsePDCTeam("1191", year);
            PDCTeam Zauzage = Parser.ParsePDCTeam("37", year);
            PDCTeam Cowboys = Parser.ParsePDCTeam("2176", year);

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

                PDCTeamPoints t = r.ComparePDCTeamToRace(KamnaChameleon);
                t.Name = "AlternativeFlats";
                kamnaSum += t.Points;
                t.RunningTotalPoints = kamnaSum; 
                
                f.Points.Add(t);
                //KamnaChameleonPoints.Add(raceCount.ToString(), kamnaSum.ToString());
                KamnaChameleonPoints.Add(new LineDataPoint(RaceCount, kamnaSum));


                t = r.ComparePDCTeamToRace(TheBauhausMovement);
                t.Name = "BauhauseMovement";
                bauhausSum += t.Points;
                t.RunningTotalPoints = bauhausSum;
                f.Points.Add(t);
                BauhausMovementPoints.Add(new LineDataPoint(RaceCount, bauhausSum));


                t = r.ComparePDCTeamToRace(Plaidstockings);
                t.Name = "Plaidstockings";
                plaidstockingsSum += t.Points;
                t.RunningTotalPoints = plaidstockingsSum;
                f.Points.Add(t);
                PlaidstockingsPoints.Add(new LineDataPoint(RaceCount, plaidstockingsSum));

                t = r.ComparePDCTeamToRace(Rubicon);
                t.Name = "Rubicon";
                rubiconSum += t.Points;
                t.RunningTotalPoints = rubiconSum;
                f.Points.Add(t);
                RubiconPoints.Add(new LineDataPoint(RaceCount, rubiconSum));

                t = r.ComparePDCTeamToRace(Zauzage);
                t.Name = "Zauzage";
                zauzageSum += t.Points;
                t.RunningTotalPoints = zauzageSum;                
                f.Points.Add(t);
                ZauzagePoints.Add(new LineDataPoint(RaceCount, zauzageSum));

                t = r.ComparePDCTeamToRace(Cowboys);
                t.Name = "Cowboys";
                cowboySum += t.Points;
                t.RunningTotalPoints = cowboySum;
                f.Points.Add(t);
                CowboysPoints.Add(new LineDataPoint(RaceCount, cowboySum));

                FantasyResults.Add(f);
                RaceCount++;
            }

            MaxPointsRace = FantasyResults.OrderByDescending(x => x.Points.Max(y => y.Points)).First();
            MaxPointsPDCTeam = MaxPointsRace.Points.OrderByDescending(x => x.Points).First();
          

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
        public PDCTeamPoints MaxPointsPDCTeam { get; set; }
    }

}
