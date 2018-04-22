using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT9_ATM
    {
        private CollisionDetector _collisionDetector;
        private TrackStorage _trackStorage;
        private AirSpace _airSpace;
        private ConsoleView _consoleView;
        private FileLogger _log;
        private IOutput _output;
        private AirTrafficController _atm;
        private Tos _tos;

        private List<ITrack> _tracks1;
        private List<ITrack> _tracks2;

        [SetUp]
        public void SetUp()
        {
            _collisionDetector = new CollisionDetector();
            _trackStorage = new TrackStorage();
            _airSpace = new AirSpace();
            _log = new FileLogger(@"CollisionATMTest.txt");
            _output = Substitute.For<IOutput>();
            _output.When(x => x.GetLargetsScreenSize(out int w, out int h)).Do(x =>
            {
                x[0] = 80;
                x[1] = 40;
            });
            _consoleView = new ConsoleView(_output);
            _atm = new AirTrafficController(_collisionDetector, _trackStorage, _airSpace, _consoleView, _log);

            _tos = new Tos(Substitute.For<ITransponderReceiver>());
            _tos.Attach(_atm);

            _tracks1 = new List<ITrack>
            {
                new Track
                {
                    Tag = "AAA111",
                    PositionX = 15000,
                    PositionY = 30000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "BBB222",
                    PositionX = 15000,
                    PositionY = 30000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "CCC333",
                    PositionX = 15000,
                    PositionY = 30000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "DDD444",
                    PositionX = 15000,
                    PositionY = 30000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "EEE555",
                    PositionX = 15000,
                    PositionY = 30000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
            };
            _tracks2 = new List<ITrack>
            {
                new Track
                {
                    Tag = "AAA111",
                    PositionX = 15000,
                    PositionY = 80000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "BBB222",
                    PositionX = 25000,
                    PositionY = 36000,
                    Altitude = 850,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "CCC333",
                    PositionX = 10000,
                    PositionY = 27000,
                    Altitude = 1900,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "DDD444",
                    PositionX = 45000,
                    PositionY = 66000,
                    Altitude = 833,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
                new Track
                {
                    Tag = "EEE555",
                    PositionX = 15000,
                    PositionY = 28000,
                    Altitude = 2500,
                    Course = 0,
                    Timestamp = DateTime.Now,
                    Velocity = 0
                },
            };
        }

        [Test]
        public void Update_ListCount5Only4InValidAirSpace_TrackStorageCountIs4()
        {
            _tracks1[0].PositionY = 1000;

            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            Assert.That(_trackStorage.GetAllTracks().Count, Is.EqualTo(4));
        }





    }
}
