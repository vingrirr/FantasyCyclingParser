using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyCyclingWeb.Models
{
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