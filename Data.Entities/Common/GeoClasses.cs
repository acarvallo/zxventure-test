using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.Geo
{
    public class Point
    {
        public Point()
        {

        }
        public Point(double longt, double lat)
        {
            coordinates = new double[] { longt, lat };
        }

        public string type { get; set; }
        public double[] coordinates { get; set; }
    }
    public class Multipolygon
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }
}
