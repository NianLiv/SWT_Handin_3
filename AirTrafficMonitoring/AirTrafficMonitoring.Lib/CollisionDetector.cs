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

        public void CheckForCollision(List<ITrack> TrackList)
        {
            for (int i = 0; i < TrackList.Count; i++)
            {
                Track currentTrack = TrackList[i] as Track;
                for (int j = 0; j < TrackList.Count; j++)
                { 
                    Track otherTrack = TrackList[j] as Track;
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

                            //Update timestamp.
                            CollisionPairsList.Find(e => e.currentTrack.Tag == currentTrack.Tag && e.otherTrack.Tag == otherTrack.Tag)
                                .timeOfConflict = currentTrack.Timestamp;

                            //Invoking eventhandler
                            var handler = Separation;
                            handler?.Invoke(this, new CollisionEventArgs(CollisionPairsList));
                        }
                        //Remove CollisionPair list if the pair is not colliding anymore
                        else if (IsPairInList(currentColpair))
                        {
                            //Find the object to remove
                            var removeObj = CollisionPairsList.Find(e =>
                                e.currentTrack.Tag == currentTrack.Tag && e.otherTrack.Tag == otherTrack.Tag);
                            CollisionPairsList.Remove(removeObj);

                            //Invoking handler to update Console.
                            var handler = NotColliding;
                            handler?.Invoke(this, new CollisionEventArgs(CollisionPairsList));

                        }
                    }
                }
            }
        }

        //Utility function to check if a pair is already in the list.
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
    //Ulitlity class to wrap collinding tracks and a DateTime-object.
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
