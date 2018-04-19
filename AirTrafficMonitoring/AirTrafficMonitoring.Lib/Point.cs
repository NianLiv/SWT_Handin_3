using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib
{
    public class Point
    {
        private int X;
        private int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point otherPoint)
        {
            return Math.Sqrt(Math.Pow((otherPoint.X - X), 2) + Math.Pow((otherPoint.Y - Y), 2));
        }
    }
    
}
