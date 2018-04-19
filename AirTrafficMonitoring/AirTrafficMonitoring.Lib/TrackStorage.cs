using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class TrackStorage : ITrackStorage
    {
        private readonly Dictionary<string, ITrack> _tracks;

        public TrackStorage()
        {
            _tracks = new Dictionary<string, ITrack>();
        }

        public void Clear() => _tracks.Clear();

        public void Remove(ITrack track) => _tracks.Remove(track.Tag);

        public void Add(ITrack track) => _tracks.Add(track.Tag, track);

        public void Update(ITrack track) => _tracks[track.Tag].Update(track);

        public ITrack GetTrackByTag(ITrack track) => _tracks[track.Tag];

        public List<ITrack> GetAllTracks() => _tracks.Values.ToList();

        public bool Contains(ITrack track) => _tracks.ContainsKey(track.Tag);
    }
}
