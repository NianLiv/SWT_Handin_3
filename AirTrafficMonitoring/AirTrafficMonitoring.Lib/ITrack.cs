using System;

namespace AirTrafficMonitoring.Lib
{
    public interface ITrack
    {
        int Altitude { get; set; }
        int Course { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        string Tag { get; set; }
        DateTime Timestamp { get; set; }
        double Velocity { get; set; }

        string ToString();
        void Update(ITrack track);
    }
}