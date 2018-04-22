using System;
using System.Collections.Generic;
using System.IO;
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

        private string _logPath = @"CollisionATMTest.txt";

        [SetUp]
        public void SetUp()
        {
            // The faked IOutpus must return a screen size to ConsoleViews ctor.
            _output = Substitute.For<IOutput>();
            _output.When(x => x.GetLargetsScreenSize(out int w, out int h)).Do(x =>
            {
                x[0] = 80;
                x[1] = 40;
            });

            _collisionDetector = new CollisionDetector();
            _trackStorage = new TrackStorage();
            _airSpace = new AirSpace();
            _log = new FileLogger(_logPath);
            _consoleView = new ConsoleView(_output);
            _atm = new AirTrafficController(_collisionDetector, _trackStorage, _airSpace, _consoleView, _log);

            // Create Tos with fake TransponderReceiver, attach ATM to subscribe on Tos.
            // _tos.Notify(_tos) will call _atm.Update(). Notify will start the system.
            _tos = new Tos(Substitute.For<ITransponderReceiver>());
            _tos.Attach(_atm);

            // Init some standard List with valid tracks, used to give to Tos before Notify() call.
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
        public void Notify_ListCount5Only4InValidAirSpace_TrackStorageCountIs4()
        {
            _tracks1[0].PositionY = 1000;

            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            Assert.That(_trackStorage.GetAllTracks().Count, Is.EqualTo(4));
        }

        [Test]
        public void Notify_CalledTwiceWithDiffListsButSameTrackTags_TagsInStorageIsUpdated()
        {
            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // All tracks in _tracks2 has at least a new PositionY value.
            _tos.RecievedTracks = _tracks2;
            _tos.Notify(_tos);

            // Assert that the stored tracks has opdated their PositionY to _track2's values.
            for (int i = 0; i < _trackStorage.GetAllTracks().Count; i++)
                Assert.That(_trackStorage.GetTrackByTag(_tracks1[i].Tag).PositionY, Is.EqualTo(_tracks2[i].PositionY));  
        }

        [Test]
        public void Notify_CalledTwiceOneTrackLeavesAirSpaceOnSecondNotify_StorageDosentContainTrack()
        {
            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // One track leaves AirSpace.
            var leaveTrack = _tracks1[0];
            leaveTrack.PositionY = 1000;

            // Update stored tracks with the new "leaveTrack".
            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // Assert that the "leaveTrack" has been deleted from storage.
            Assert.That(_trackStorage.Contains(leaveTrack), Is.False);
        }

        [Test]
        public void Notify_ListCount5InValidAirSpace_IOutputReceivesTrackToString5Times()
        {
            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // Then ATM has called ConsoleView.RenderTrackData() correct.
            foreach (var track in _trackStorage.GetAllTracks())
                _output.Received(1).OutputLine(track.ToString());
        }

        [Test]
        public void Notify_ListCount5InValidAirSpace_IOutputReceivesCharX5Times()
        {
            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // Then ATM has called ConsoleView.RenderMap() correct.
            _output.Received(5).OutputLine("x");
        }

        [Test]
        public void Notify_ListWith2CollidingTracks_IOutputReceivesCollisionPairString()
        {
            var collisionTrack1 = _tracks1[0];
            var collisionTrack2 = _tracks1[1];

            // Two track is now colliding.
            collisionTrack1.PositionY = 12000;
            collisionTrack1.PositionX = 12000;
            collisionTrack2.PositionY = 12000;
            collisionTrack2.PositionX = 12000;

            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // Then ATM must have received a Separation event and called ConsoleView.PrintCollisionTracks() correct.
            var collisionPair = new CollisionPairs((Track)collisionTrack1, (Track)collisionTrack2, DateTime.Now);
            _output.Received().OutputLine(collisionPair.ToString());
        }

        [Test]
        public void Notify_CalledTwiceFirstWithCollidingTracksSecondWithNoCollions_IOutPutRecievedEmptyString()
        {
            var collisionTrack1 = _tracks1[0];
            var collisionTrack2 = _tracks1[1];

            // Two track is now colliding.
            collisionTrack1.PositionY = 12000;
            collisionTrack1.PositionX = 12000;
            collisionTrack2.PositionY = 12000;
            collisionTrack2.PositionX = 12000;

            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // No Tracks in _tracks2 is colliding.
            _tos.RecievedTracks = _tracks2;
            _tos.Notify(_tos);

            // Then ATM must have received a NotColliding event and
            // called ConsoleView.Clear() with startX = 0 and endX = w - w /3 correct.
            var clearStart = 0;
            var clearEnd = _consoleView.Width - _consoleView.Width / 3;
            _output.Received().OutputLine(new string(' ', clearEnd - clearStart));
        }

        [Test]
        public void Notify_ListWith2CollidingTracks_()
        {
            var collisionTrack1 = _tracks1[0];
            var collisionTrack2 = _tracks1[1];

            // Two track is now colliding.
            collisionTrack1.PositionY = 12000;
            collisionTrack1.PositionX = 12000;
            collisionTrack2.PositionY = 12000;
            collisionTrack2.PositionX = 12000;

            _tos.RecievedTracks = _tracks1;
            _tos.Notify(_tos);

            // Then ATM must have received a Separation event ant called FileLogger.LogCollisionToFile().
            // If streamReader reads the collisionpair string in file, then ATM uses FileLogger correct.
            var collisionPair = new CollisionPairs((Track)collisionTrack1, (Track)collisionTrack2, DateTime.Now);
            using (StreamReader reader = new StreamReader(_logPath, true))
                Assert.That(reader.ReadToEnd(), Contains.Substring(collisionPair.ToString()));
        }

    }
}
