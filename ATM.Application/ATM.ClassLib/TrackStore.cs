using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ClassLib
{
    class TrackStore
    {
        public List<TrackObject> trackObjects;

        public TrackStore() => trackObjects = new List<TrackObject>();

        public void saveTrack(TrackObject track) => trackObjects.Add(track);

    }
}
