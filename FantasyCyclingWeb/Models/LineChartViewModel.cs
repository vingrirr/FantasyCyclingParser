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

        public void AddTeam(PDCTeam team, List<LineDataPoint> points)
        {
            string clr = _ColorList[ChartData.Count()];
            string key = team.PDCTeamName;

            LineChartModel m = new LineChartModel(key, clr);
            m.values = points;
            ChartData.Add(m);
        }

        public List<LineChartModel> ChartData { get; set; }
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
    }

    public class LineChartModel
    {
        public LineChartModel()
        {
            values = new List<LineDataPoint>(); 
        }
        public LineChartModel(string ky, string clr)
            :this()
        {
            color = clr;
            key = ky;
            
        }
        public string color { get; set; }
        public string key { get; set; }
        public List<int[][]> values { get; set; }
    }
}