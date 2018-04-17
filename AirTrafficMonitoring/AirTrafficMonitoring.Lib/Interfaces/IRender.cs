using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface IRender
    {
        void PrintTrackData(List<Track> tracks);
    }
}
