using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ClassLib
{
    public class TrackStore
    {
        public List<TrackObject> trackObjects { get; }

        public TrackStore() => trackObjects = new List<TrackObject>();

        public void saveTrack(TrackObject track) => trackObjects.Add(track);

    }
}
