using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib
{
    public class Track
    {
        public string Tag { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Altitude { get; set; }
        public DateTime Timestamp { get; set; }

        internal void Update(Track track)
        {
            
        }
    }
}
