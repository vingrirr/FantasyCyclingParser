﻿using FantasyCyclingParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyCyclingWeb.Models
{
    public class NewRaceSeasonViewModel
    {
        public NewRaceSeasonViewModel(PDC_Season season, FantasyYearConfig config)
        {

            _PDCTeamData = new List<PDCTeamPoints>();
            _teams = new List<PDCTeam>();
            _results = new List<PDC_Result>();
            FantasyResults = new List<FantasyResult>();
            LineChartVM = new LineChartViewModel(); 
            BuildSeason(season, config);

          
            RaceCount = 0;
            int runningSum = 0; 
            foreach (PDCTeam currTeam in _teams)
            {
                runningSum = 0;
                List<LineDataPoint> currTeamLineData = new List<LineDataPoint>();
                foreach (PDC_Result r in _results)
                {
                    FantasyResult f = new FantasyResult();
                               
                    f.Race = r;
                    f.TempID = RaceCount;

                    PDCTeamPoints t = r.ComparePDCTeamToRace(currTeam);
                    
                    t.Name = currTeam.PDCTeamName;
                    runningSum += t.Points;
                    t.RunningTotalPoints = runningSum;

                    f.Points.Add(t);
                    currTeamLineData.Add(new LineDataPoint(RaceCount, runningSum));                                        
                    FantasyResults.Add(f);
                    RaceCount++;
                }
                
                LineChartVM.AddTeam(currTeam, currTeamLineData);
                
            }

            // MaxPointsRace = FantasyResults.OrderByDescending(x => x.Points.Max(y => y.Points)).First();
            //MaxPointsPDCTeam = MaxPointsRace.Points.OrderByDescending(x => x.Points).First();


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
