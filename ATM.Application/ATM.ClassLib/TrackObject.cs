using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ClassLib
{
    public class TrackObject
    {
        public TrackObject(string[] arr)
        {
            Tag = arr[0];
            XCoord = Int32.Parse(arr[1]);
            YCoord = Int32.Parse(arr[2]);
            Altitude = Int32.Parse(arr[3]);
            TimeStamp = long.Parse(arr[4]);
        }

        public string Tag { get; }
        public int XCoord { get; }
        public int YCoord { get; }
        public int Altitude { get; }
        public long  TimeStamp { get; }
    }
}
