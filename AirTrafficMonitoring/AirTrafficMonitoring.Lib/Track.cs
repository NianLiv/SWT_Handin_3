using System;

namespace AirTrafficMonitoring.Lib
{
    public class Track
    {
        public string Tag { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Altitude { get; set; }
        public DateTime Timestamp { get; set; }

        public void Update(Track track)
        {
            if (this == track || Tag != track.Tag) return;

            if (PositionX != track.PositionX)
                PositionX = track.PositionX;

            if (PositionY != track.PositionY)
                PositionY = track.PositionY;

            if (Altitude != track.Altitude)
                Altitude = track.Altitude;

            if (Timestamp != track.Timestamp)
                Timestamp = track.Timestamp;
        }

        public override string ToString() => $"{Tag}: ({PositionX}, {PositionY}) ALT: {Altitude}";
    }
}
