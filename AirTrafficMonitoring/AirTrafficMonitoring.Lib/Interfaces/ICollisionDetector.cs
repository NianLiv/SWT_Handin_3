using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public class CollisionEventArgs : EventArgs
    {
        public Track currentTrack;
        public Track otherTrack;
        public DateTime timeOfConflict;
    }

    public interface ICollisionDetector
    {
        event EventHandler<CollisionEventArgs> Separation;
        void CheckForCollision(List<Track> TrackList);
    }
}
