﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib
{
    class TrackCalculator
    {
        public static double CalculateVelocity(int x1, int y1, int x2, int y2, DateTime lastTime, DateTime currentTime)
        {
            var PrevPoint = new Point(x1, y1);
            var NewPoint = new Point(x2, y2);
            var dist = PrevPoint.DistanceTo(NewPoint);
            var time = currentTime - lastTime;

            return time.TotalSeconds / dist;
        }

        public static int CalculateCourse(int x1, int y1, int x2, int y2)
        {
            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            double angleInDegrees = Math.Atan2(deltaY, deltaX);
            return (int) angleInDegrees;
        }
    }
}
