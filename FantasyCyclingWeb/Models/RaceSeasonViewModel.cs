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
        public RaceSeasonViewModel(PDC_Season season, FantasyYearConfig config)
        {

            _PDCTeamData = new List<PDCTeamPoints>();
            _teams = new List<PDCTeam>();
            _results = new List<PDC_Result>();
            FantasyResults = new List<FantasyResult>();
            LineChartVM = new LineChartViewModel();
            BuildSeason(season, config);


            RaceCount = 0;
            int runningSum = 0;

            List<int[,]> pointValues = new List<int[,]>();
            
   
            foreach (PDC_Result r in _results)
            {
                
                pointValues = new List<int[,]>();
                List<LineDataPoint> currTeamLineData = new List<LineDataPoint>();
               
                FantasyResult f = new FantasyResult();

                foreach (PDCTeam currTeam in _teams)
                {
                    pointValues = new List<int[,]>();
                    runningSum = LineChartVM.GetRunningSumPoints(currTeam);

                    if (config.TeamUIDS.FirstOrDefault(u => u.TeamUID == currTeam.PDC_ID).Is35Team)
                        continue;
                    f.Race = r;
                    f.TempID = RaceCount;

                    PDCTeamPoints t = r.ComparePDCTeamToRace(currTeam);

                    t.Name = currTeam.PDCTeamName;
                    runningSum += t.Points;
                    t.RunningTotalPoints = runningSum;

                    f.Points.Add(t);

                    int[,] graphAxisValues = new int[,] { { RaceCount, runningSum } };

                    //pointValues.Add(graphAxisValues);
                    //currTeamLineData.Add(new LineDataPoint(count, runningSum));                                        
                    


                    LineChartVM.AddTeam(currTeam, graphAxisValues);
                }
                FantasyResults.Add(f);
                RaceCount++;


            }
            int x = 0;
            MaxPointsRace = FantasyResults.OrderByDescending(m => m.Points.Max(y => y.Points)).First();
            MaxPointsPDCTeam = MaxPointsRace.Points.OrderByDescending(n => n.Points).First();


        }


        public void BuildSeason(PDC_Season season, FantasyYearConfig config)
        {
            _results = season.RaceResults;
            _results.Reverse();

            foreach (PDCTeamYear ty in config.TeamUIDS)
            {

                PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
                _teams.Add(team);
            }
            List<int> points = new List<int>();

            foreach (PDCTeam t in _teams)
            {

                foreach (Rider r in t.Riders)
                {

                    int raceResults = season.RaceResults.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == r.PDC_RiderID)).Sum(g => g.Points);
                    points.Add(raceResults);
                }
                PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, points.Sum());
                _PDCTeamData.Add(ptp);
                points.Clear();

            }
        }
        public int RaceCount { get; set; }
        public List<FantasyResult> FantasyResults { get; set; }

        public LineChartViewModel LineChartVM { get; set; }

        public FantasyResult MaxPointsRace { get; set; }
        public PDCTeamPoints MaxPointsPDCTeam { get; set; }


        private List<PDCTeam> _teams { get; set; }
        List<PDC_Result> _results { get; set; }

        List<PDCTeamPoints> _PDCTeamData { get; set; }
    }


}
