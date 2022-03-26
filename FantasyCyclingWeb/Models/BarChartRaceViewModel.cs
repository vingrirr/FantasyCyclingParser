using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyCyclingWeb.Models
{
    public class BarChartRaceViewModel
    {
        public BarChartRaceViewModel()
        {
            RaceList = new List<string>();
            BarChartData = new List<BarChartRaceItem>();
        }

        public List<string> RaceList { get; set; }
        public List<BarChartRaceItem> BarChartData { get; set; }
    }

    public class BarChartRaceItem
    {
        public BarChartRaceItem()
        {
            Items = new List<KeyValuePair<string, int>>();
        }

        public string Name { get; set; }
        public List<KeyValuePair<string, int>> Items { get; set; }
        
    }

    public class BarChartDataPoint
    {
        public BarChartDataPoint()
        {

        }

        public string Key { get; set; }
        public int Value { get; set; }
    }
}