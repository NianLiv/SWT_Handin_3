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

        public bool Remove(ITrack track) => _tracks.Remove(track.Tag);

        public void Add(ITrack track)
        {
            if (track.Tag?.Length > 0 && !_tracks.ContainsKey(track.Tag))
                _tracks.Add(track.Tag, track);
        }

        public void Update(ITrack track)
        {
            if(Contains(track))
                _tracks[track.Tag].Update(track);
        }

        public ITrack GetTrackByTag(string tag)
        {
            if (Contains(new Track { Tag = tag }) && tag.Length > 0)
                return _tracks[tag];
            else return null;
        }

        public List<ITrack> GetAllTracks() => _tracks.Values.ToList();


        //=> _tracks.ContainsKey(track.Tag)
        public bool Contains(ITrack track)
        {
            if (track.Tag == null)
                return false;

            if (_tracks.ContainsKey(track.Tag))
            return true;

            return false;
        }
    }
}
