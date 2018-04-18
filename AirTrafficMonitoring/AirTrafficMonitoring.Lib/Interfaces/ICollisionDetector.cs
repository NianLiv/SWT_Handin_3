using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public class CollisionEventArgs : EventArgs
    {
        public List<CollisionPairs> CollisionPairs { get; }
        public CollisionEventArgs(List<CollisionPairs> colpairs)
        {
            CollisionPairs = colpairs;
        }
    }

    public interface ICollisionDetector
    {
        event EventHandler<CollisionEventArgs> Separation;
        event EventHandler<CollisionEventArgs> NotColliding;
        void CheckForCollision(List<Track> TrackList);
        List<CollisionPairs> CollisionPairsList { get; }
    }
}
