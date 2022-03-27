using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FantasyCyclingParser;

namespace FantasyCyclingWeb.Models
{
    public class LineChartViewModel
    {
        public LineChartViewModel()
        {
            ChartData = new List<LineChartModel>();
        }

        public void AddTeam(PDCTeam team, int[,] points)
        {
            string clr = "";
            if (!_data.ContainsKey(team.PDCTeamName))
            {
                string key = team.PDCTeamName;
                
                if (!_teamColors.ContainsKey(team.PDCTeamName))
                {                    
                    clr = _ColorList[ChartData.Count()];
                    _teamColors.Add(team.PDCTeamName, clr);
                }
                else
                {
                    clr = _teamColors[team.PDCTeamName];
                }
                List<LineChartModel> list = new List<LineChartModel>(); 
                LineChartModel m = new LineChartModel(key, clr);
                
                m.values.Add(points);

                list.Add(m);
                _data.Add(key, list);
                ChartData.Add(m);
            }
            else
            {
                LineChartModel m = ChartData.FirstOrDefault(x => x.key == team.PDCTeamName);                
                m.values.Add(points);                
            }
                                                                                             
        }
        public int GetRunningSumPoints(PDCTeam team)
        {
            if (!_data.ContainsKey(team.PDCTeamName))
                return 0;
            else
            {
                LineChartModel m = ChartData.FirstOrDefault(x => x.key == team.PDCTeamName);

                int[,] sum = m.values.Last();

                return sum[0, 1];
            }
        }
        
        public  List<LineChartModel> ChartData { get; set; }
        private List<string> _ColorList = new List<string>
        {
            "rgb(255, 127, 14)",
            "rgb(44, 160, 44)",
            "rgb(174, 199, 232)",
            "rgb(255, 187, 120)",
            "rgb(31, 119, 180)",
            "rgb(152, 223, 138)",

            "rgb(255, 127, 14)",
            "rgb(44, 160, 44)",
            "rgb(174, 199, 232)",
            "rgb(255, 187, 120)",
            "rgb(31, 119, 180)",
            "rgb(152, 223, 138)",

            "rgb(255, 127, 14)",
            "rgb(44, 160, 44)",
            "rgb(174, 199, 232)",
            "rgb(255, 187, 120)",
            "rgb(31, 119, 180)",
            "rgb(152, 223, 138)"



        };
        private Dictionary<string, string> _teamColors = new Dictionary<string, string>();
        private Dictionary<string, List<LineChartModel>> _data = new Dictionary<string, List<LineChartModel>>();
    }

    public class LineChartModel
    {
        public LineChartModel()
        {
            values = new List<int[,]>(); 
        }
        public LineChartModel(string ky, string clr)
            :this()
        {
            color = clr;
            key = ky;
            
        }
        public string color { get; set; }
        public string key { get; set; }
        public List<int[,]> values { get; set; }
    }
}