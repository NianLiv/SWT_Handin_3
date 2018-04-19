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
        public int _EastBound;
        public int _WestBound;
        public int _NorthBound;
        public int _SouthBound;
        public int _LowerBound;
        public int _UpperBound;

        public AirSpace() : this(90000, 10000, 90000, 10000, 500, 20000)
        { }

        public AirSpace(int E, int W, int N, int S, int L, int U)
        {
            _EastBound = E;
            _WestBound = W;
            _NorthBound = N;
            _SouthBound = S;
            _LowerBound = L;
            _UpperBound = U;
        }

        public bool IsInValidAirSpace(ITrack track)
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
