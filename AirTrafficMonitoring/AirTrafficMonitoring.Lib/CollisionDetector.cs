using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class CollisionDetector : ICollisionDetector
    {
        public event EventHandler<CollisionEventArgs> Separation;

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
                        //Track coordinates in space
                        var currentPoint = new Point(currentTrack.PositionX, currentTrack.PositionY);
                        var otherPoint = new Point(otherTrack.PositionX, otherTrack.PositionY);

                        //Checking for event
                        if ((currentTrack.Altitude - otherTrack.Altitude) < 300 
                            && (currentTrack.Altitude -otherTrack.Altitude) > -300
                            && currentPoint.DistanceTo(otherPoint) < 5000)
                        {
                            var handler = Separation;
                            handler?.Invoke(this, new CollisionEventArgs(currentTrack, otherTrack, currentTrack.Timestamp));
                        }                       
                    }
                }
            }
        }
    }
}
