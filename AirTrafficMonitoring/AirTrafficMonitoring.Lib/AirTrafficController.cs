using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class AirTrafficController : IObserver<Tos>
    {
        private ICollisionDetector _collisonDetector;
        private ITrackStorage _trackStorage;
        private IAirSpace _airSpace;
        private IRender _render;
        private ILog _log;

        public AirTrafficController(ICollisionDetector cd, ITrackStorage ts, IAirSpace airs, IRender r, ILog l)
        {
            _collisonDetector = cd;
            _trackStorage = ts;
            _airSpace = airs;
            _render = r;
            _log = l;

            _collisonDetector.Separation += (s, e) =>
            {
                _render.PrintCollisionTracks(e.CollisionPairs);
                _log.LogCollisionToFile(e.CollisionPairs);
            };
            
            _collisonDetector.NotColliding += (s, e) =>
            {
                _render.PrintCollisionTracks(e.CollisionPairs, true);
            };
        }

        public void Update(Tos obj)
        {
            var recievedTracks = obj.RecievedTracks;

            foreach (var track in recievedTracks)
            {
                if (_airSpace.IsInValidAirSpace(track))
                {
                    if (_trackStorage.Contains(track))
                        _trackStorage.Update(track);
                    else
                        _trackStorage.Add(track);
                }
                else if (_trackStorage.Contains(track) && !_airSpace.IsInValidAirSpace(track))
                    _trackStorage.Remove(track);
            }

            _collisonDetector.CheckForCollision(_trackStorage.GetAllTracks());
            _render.PrintTrackData(_trackStorage.GetAllTracks());
        }
    }
}
