using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT10_ATM
    {
        private CollisionDetector _collisionDetector;
        private TrackStorage _trackStorage;
        private AirSpace _airSpace;
        private ConsoleView _consoleView;
        private FileLogger _log;
        private IOutput _output;



        [SetUp]
        public void SetUp()
        {
            _collisionDetector = new CollisionDetector();
            _trackStorage = new TrackStorage();
            _airSpace = new AirSpace(90000, 10000, 90000, 10000, 500, 20000);
            _consoleView = new ConsoleView(_output);
            _log = new FileLogger(@"CollisionATMTest.txt");
        }


    }
}
