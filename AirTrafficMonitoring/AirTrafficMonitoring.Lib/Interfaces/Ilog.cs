using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    interface ILog
    {
        List<Track> LoggedTracks { get; }

        //return type missing Logger(List<Track> logTracks);
    }
}
