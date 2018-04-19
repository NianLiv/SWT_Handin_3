using System.Collections.Generic;

namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface ITrackStorage
    {
        void Clear();
        void Remove(ITrack track);
        void Add(ITrack track);
        void Update(ITrack track);
        ITrack GetTrackByTag(ITrack track);
        List<ITrack> GetAllTracks();
        bool Contains(ITrack track);
    }
}
