using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    class CollisionDetector : ICollisionDetector
    {
        public event EventHandler<CollisionEventArgs> Separation;

        public CollisionDetector()
        {

        }

        public void CheckForCollision(List<Track> TrackList)
        {
            for (int i = 0; i < TrackList.Count; i++)
            {
                var currentTrack = TrackList[i];
                for (int j = 0; j < TrackList.Count; j++)
                {
                    var otherTrack = TrackList[j];
                    if (currentTrack != otherTrack)
                    {
                        if(((currentTrack.Altitude - otherTrack.Altitude) < 300) && (currentTrack.PositionX))
                    }
                }
            }
        }

        
        


    }
}
