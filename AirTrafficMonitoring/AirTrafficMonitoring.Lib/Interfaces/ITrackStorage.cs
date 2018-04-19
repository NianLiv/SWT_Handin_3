using System.Collections.Generic;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface ITrackStorage
    {
        void Clear();
        bool Remove(ITrack track);
        void Add(ITrack track);
        void Update(ITrack track);
        ITrack GetTrackByTag(string tag);
        List<ITrack> GetAllTracks();
        bool Contains(ITrack track);
    }
}
