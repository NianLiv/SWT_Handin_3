using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    class TrackStorage : ITrackStorage
    {
        private Dictionary<string, Track> _tracks;

        public TrackStorage()
        {
            _tracks = new Dictionary<string, Track>();
        }

        public void Clear() => _tracks.Clear();

        public void Remove(Track track) => _tracks.Remove(track.Tag);

        public void Add(Track track) => _tracks.Add(track.Tag, track);

        public void Update(Track track) => _tracks[track.Tag].Update(track);

        public Track GetTrackByTag(Track track) => _tracks[track.Tag];

        public List<Track> GetAllTracks() => _tracks.Values.ToList();

        public bool Contains(Track track) => _tracks.ContainsKey(track.Tag);
    }
}
