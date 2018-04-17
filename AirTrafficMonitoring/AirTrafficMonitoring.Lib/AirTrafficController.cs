using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public void Update(Tos obj)
        {
            
        }
    }
}
