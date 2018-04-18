using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class CollisionDetector : ICollisionDetector
    {
        public event EventHandler<CollisionEventArgs> Separation;
        public event EventHandler<CollisionEventArgs> NotColliding;
        public List<CollisionPairs> CollisionPairsList { get; } = new List<CollisionPairs>();

        public void CheckForCollision(List<Track> TrackList)
        {
            for (int i = 0; i < TrackList.Count; i++)
            {
                var currentTrack = TrackList[i];
                for (int j = 0; j < TrackList.Count; j++)
                { 
                    var otherTrack = TrackList[j];
                    if (currentTrack.Tag != otherTrack.Tag)
                    {
                        //Track coordinates in space
                        var currentPoint = new Point(currentTrack.PositionX, currentTrack.PositionY);
                        var otherPoint = new Point(otherTrack.PositionX, otherTrack.PositionY);

                        //Collision Pair to check
                        var currentColpair = new CollisionPairs(currentTrack, otherTrack, currentTrack.Timestamp);

                        //Checking for event
                        if ((currentTrack.Altitude - otherTrack.Altitude) < 300 
                            && (currentTrack.Altitude -otherTrack.Altitude) > -300
                            && currentPoint.DistanceTo(otherPoint) < 5000)
                        {
                            if(!IsPairInList(currentColpair)) CollisionPairsList.Add(currentColpair);

                            if(CollisionPairsList.Count >= 2)
                                CollisionPairsList.Find(e => e.currentTrack.Tag == currentTrack.Tag && e.otherTrack.Tag == otherTrack.Tag)
                                    .timeOfConflict = currentTrack.Timestamp;

                            Debug.WriteLine("Tilføjet til listen: " + CollisionPairsList.Count);

                            var handler = Separation;
                            handler?.Invoke(this, new CollisionEventArgs(CollisionPairsList));
                        }
                        else if (IsPairInList(currentColpair))
                        {
                            var removeObj = CollisionPairsList.Find(e =>
                                e.currentTrack.Tag == currentTrack.Tag && e.otherTrack.Tag == otherTrack.Tag);
                            CollisionPairsList.Remove(removeObj);
                            var handler = NotColliding;
                            handler?.Invoke(this, new CollisionEventArgs(CollisionPairsList));

                            Debug.WriteLine("Fjerner fra listen: " + CollisionPairsList.Count);
                        }
                    }
                }
            }
        }

        private bool IsPairInList(CollisionPairs pairToCheck)
        {
            foreach (var pair in CollisionPairsList)
            {
                if (pair.currentTrack.Tag == pairToCheck.currentTrack.Tag &&
                    pair.otherTrack.Tag == pairToCheck.otherTrack.Tag)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class CollisionPairs
    {
        public Track currentTrack;
        public Track otherTrack;
        public DateTime timeOfConflict;

        public CollisionPairs(Track ct, Track ot, DateTime dt)
        {
            currentTrack = ct;
            otherTrack = ot;
            timeOfConflict = dt;
        }

        public override string ToString()
        {
            return "Tag: " + currentTrack.Tag + " kolliderer med tag: " + otherTrack.Tag + ". Tidpunkt: " +
                   timeOfConflict;
        }
    }
}
