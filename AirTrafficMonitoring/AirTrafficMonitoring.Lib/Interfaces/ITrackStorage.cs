using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface ITrackStorage
    {
        void Clear();
        void Remove(Track track);
        void Add(Track track);
        void Update(Track track);
        Track GetTrackByTag(Track track);
        List<Track> GetAllTracks();
        bool Contains(Track track);
    }
}
