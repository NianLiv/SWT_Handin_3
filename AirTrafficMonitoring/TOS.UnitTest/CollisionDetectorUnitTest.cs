using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib.Interfaces;
using AirTrafficMonitoring.Lib;
using NSubstitute;
using NUnit.Framework;

namespace TOS.UnitTest
{
    [TestFixture]
    class CollisionDetectorUnitTest
    {
        private ICollisionDetector _uut;
        private int SeparationEventCounter = 0;
        private int NotCollidingEventCounter = 0;

        [SetUp]
        public void SetUp()
        {
            _uut = new CollisionDetector();

            //Attaching event handlers
            _uut.Separation += (e, args) => SeparationEventCounter++;
            _uut.NotColliding += (e, args) => NotCollidingEventCounter++;
        }

        [Test]
        public void CheckForCollision_SeparationEventRaised_SeparationCounterIsTwo()
        {
            //Reset event counters
            SeparationEventCounter = 0;
            NotCollidingEventCounter = 0;
            //Add colliding tracks to list
            var tracks = new List<ITrack>
            {
                new Track{PositionX = 5000, PositionY = 5000, Tag = "AAA", Timestamp = DateTime.Now, Altitude = 1000},
                new Track{PositionX = 5000, PositionY = 5000, Tag = "BBB", Timestamp = DateTime.Now, Altitude = 1000},
            };

            _uut.CheckForCollision(tracks);

            //Is equal to two because both tracks cause the event.
            Assert.That(SeparationEventCounter, Is.EqualTo(2));
        }

        [Test]
        public void CheckForCollision_SeparationEventNotRaised_SeparationEventCounterIsZero()
        {
            //Reset Event counters
            SeparationEventCounter = 0;

            //Add non colliding tracks to a list
            var tracks = new List<ITrack>
            {
                new Track{PositionX = 50000, PositionY = 15000, Tag = "AAA", Timestamp = DateTime.Now, Altitude = 9000},
                new Track{PositionX = 5000, PositionY = 5000, Tag = "BBB", Timestamp = DateTime.Now, Altitude = 1000},
            };

            _uut.CheckForCollision(tracks);

            //Is equal to two because both tracks cause the event.
            Assert.That(SeparationEventCounter, Is.EqualTo(0));
        }

        [Test]
        public void CheckForCollision_NotCollidingEventRaised_NotCollidingEventCounterIsTwo()
        {
            NotCollidingEventCounter = 0;

            //Add colliding tracks
            var tracks = new List<ITrack>
            {
                new Track{PositionX = 5000, PositionY = 5000, Tag = "AAA", Timestamp = DateTime.Now, Altitude = 1000},
                new Track{PositionX = 5000, PositionY = 5000, Tag = "BBB", Timestamp = DateTime.Now, Altitude = 1000},
            };
            _uut.CheckForCollision(tracks);

            //Move a track out of collision zone
            tracks[0].Altitude = 9000;

            _uut.CheckForCollision(tracks);

            //Is equal to two because both tracks cause the event.
            Assert.That(NotCollidingEventCounter, Is.EqualTo(2));
        }
    }
}
