using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace TOS.UnitTest
{
    [TestFixture]
    class AirTrafficControllerUnitTest
    {
        private AirTrafficController _uut;
        private ICollisionDetector _collisionDetector;
        private IAirSpace _airSpace;
        private ILog _log;
        private IRender _render;
        private ITrackStorage _trackStorage;
        private Tos _tos;

        private ITrack validTrack1;
        private ITrack validTrack2;

        [SetUp]
        public void SetUp()
        {
            _collisionDetector = Substitute.For<ICollisionDetector>();
            _airSpace = Substitute.For<IAirSpace>();
            _log = Substitute.For<ILog>();
            _render = Substitute.For<IRender>();
            _trackStorage = Substitute.For<ITrackStorage>();
            _tos = Substitute.For<Tos>(Substitute.For<ITransponderReceiver>());

            validTrack1 = new Track {Tag = "AAA111", PositionX = 12000, PositionY = 80000, Altitude = 700};
            validTrack2 = new Track {Tag = "BBB222", PositionX = 80000, PositionY = 12000, Altitude = 1700};

            _uut = new AirTrafficController(_collisionDetector, _trackStorage, _airSpace, _render, _log);
        }

        [TestCase(3)]
        [TestCase(190)]
        [TestCase(8900)]
        public void SeparationEventHandler_EventRaisedOnce_PrintCollisionTracksCalledOnce(int numOfEvents)
        {
            var collisionList = new List<CollisionPairs> {
                new CollisionPairs(new Track(), new Track(), new DateTime())
            };

            for (int i = 0; i < numOfEvents; i++)
                _collisionDetector.Separation += Raise.EventWith(new object(), new CollisionEventArgs(collisionList));

            _render.Received(numOfEvents).PrintCollisionTracks(collisionList);
        }

        [TestCase(3)]
        [TestCase(190)]
        [TestCase(8900)]
        public void SeparationEventHandler_EventRaisedNumberOfTimes_LogCollisionToFileCalledNumberOfTimes(int numOfEvents)
        {
            var collisionList = new List<CollisionPairs> {
                new CollisionPairs(new Track(), new Track(), new DateTime())
            };

            for (int i = 0; i < numOfEvents; i++)
                _collisionDetector.Separation += Raise.EventWith(new object(), new CollisionEventArgs(collisionList));

            _log.Received(numOfEvents).LogCollisionToFile(collisionList);
        }

        [Test]
        public void Update_ArgIs2DifferentValidTracks_AddIsCalledTwice()
        {
            var list = new List<ITrack> {
                validTrack1,
                validTrack2
            };
            _tos.RecievedTracks = list;

            _airSpace.IsInValidAirSpace(validTrack1).Returns(true);
            _airSpace.IsInValidAirSpace(validTrack2).Returns(true);

            _uut.Update(_tos);
            _trackStorage.ReceivedWithAnyArgs(2).Add(new Track()); 
        }

        [Test]
        public void Update_ArgIs3IdenticalValidTracks_UpdateIsCalledTwice()
        {
            int containsArgValidTrack1Cnt = 0;

            var list = new List<ITrack> {
                validTrack1,
                validTrack1,
                validTrack1
            };
            _tos.RecievedTracks = list;

            _airSpace.IsInValidAirSpace(validTrack1).Returns(true);

            _trackStorage.Contains(validTrack1).Returns(x => {
                if (containsArgValidTrack1Cnt > 1) return true;
                else return false;
            });

            _trackStorage.When(x => x.Contains(validTrack1)).Do(x => containsArgValidTrack1Cnt++);
            
            _uut.Update(_tos);

            _trackStorage.ReceivedWithAnyArgs(2).Update(new Track());
        }

        [Test]
        public void Update_validTrackLeavesAirspaceOnSecondUpdate_RemoveIsCalledOnce()
        {
            bool trackIsInAirSpace = true;

            var list = new List<ITrack> {
                validTrack1,
            };
            _tos.RecievedTracks = list;

            _trackStorage.Contains(validTrack1).Returns(true);
            _airSpace.IsInValidAirSpace(validTrack1).Returns(x =>
            {
                if (trackIsInAirSpace) return true;
                else return false; ;
            });

            _airSpace.When(x => x.IsInValidAirSpace(validTrack1)).Do(x => trackIsInAirSpace = false);

            _uut.Update(_tos);
            _uut.Update(_tos);

            _trackStorage.ReceivedWithAnyArgs().Remove(new Track());
        }

        
    }
}
