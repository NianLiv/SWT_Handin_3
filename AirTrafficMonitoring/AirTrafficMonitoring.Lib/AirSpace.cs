using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    class AirSpace : IAirSpace
    {
        public bool IsInValidAirSpace(Track track)
        {
            int _EastBound = 90000;
            int _WestBound = 10000;
            int _NorthBound = 90000;
            int _SouthBound = 10000;
            int _LowerBound = 500;
            int _UpperBound = 20000;

            if (track.Altitude < _LowerBound && track.Altitude > _UpperBound && (track.PositionX < _WestBound || track.PositionX > _EastBound) && (track.PositionY < _SouthBound || track.PositionY > _NorthBound))
                return false;

            return true;
        }
    }
}
