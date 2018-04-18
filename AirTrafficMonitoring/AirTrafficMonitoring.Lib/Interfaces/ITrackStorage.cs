using System.Collections.Generic;

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
