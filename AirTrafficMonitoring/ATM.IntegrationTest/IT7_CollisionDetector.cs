using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT7_CollisionDetector
    {
        private CollisionDetector _detector;
        private Track _track1;
        private Track _track2;
        private List<ITrack> TrackList = new List<ITrack>();

        //Using booleans to see wether an event was raised or not.
        private bool separationWasRaised = false;
        private bool notColldingWasRaised = false;

        [SetUp]
        public void SetUp()
        {
            _detector = new CollisionDetector();

            //Instantiating two colliding tracks and adding them to a list
            _track1 = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
            TrackList.Add(_track1);
            _track2 = new Track
            {
                Tag = "BBB222",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
            TrackList.Add(_track2);

            //Attaching handlers to the events.
            _detector.Separation += (o, e) => separationWasRaised = true;
            _detector.NotColliding += (o, e) => notColldingWasRaised = true;
        }

        [Test]
        public void CheckForCollisions_CollidingTrack_RaisingSeparationEvent()
        {
            _detector.CheckForCollision(TrackList);

            //Test to see if correct event is raised.
            //Dependency to Point and CollisionPair must work correctly, because output is correct.
            Assert.That(separationWasRaised, Is.EqualTo(true));
        }

        [Test]
        public void CheckForCollisions_NotCollidingTrack_RaisingNotCollidingEvent()
        {
            //Moving track out of collisionzone, and checking for collisions
            _track2.PositionX = 30000;
            _detector.CheckForCollision(TrackList);

            //Not colliding event was raised correctly. 
            //Dependency to Point and CollisionPair must work correctly.
            Assert.That(notColldingWasRaised, Is.EqualTo(true));
        }

    }
}
