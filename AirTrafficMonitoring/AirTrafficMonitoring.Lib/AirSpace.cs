using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class AirSpace : IAirSpace
    {
        public int _EastBound = 90000;
        public int _WestBound = 10000;
        public int _NorthBound = 90000;
        public int _SouthBound = 10000;
        public int _LowerBound = 500;
        public int _UpperBound = 20000;

        public AirSpace() : this() { }

        public AirSpace(int E, int W, int N, int S, int L, int U)

        public bool IsInValidAirSpace(Track track)
        {
            if (track.Altitude < _LowerBound
                || track.Altitude > _UpperBound
                || track.PositionX < _WestBound
                || track.PositionX > _EastBound
                || track.PositionY > _NorthBound
                || track.PositionY < _SouthBound)
            {
                return false;
            }
            else return true;
        }
    }
}
