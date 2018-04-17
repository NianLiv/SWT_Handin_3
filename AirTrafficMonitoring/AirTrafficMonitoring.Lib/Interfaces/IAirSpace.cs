using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    interface IAirSpace
    {
        bool IsInValidAirSpace(Track track);
    }
}
