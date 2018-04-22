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
        private List<ITrack> TrackList = new List<ITrack>();
        private bool wasCalled = false;

        [SetUp]
        public void SetUp()
        {
            _detector = new CollisionDetector();
            Track _track1 = new Track
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

            Track _track2 = new Track
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

            _detector.Separation += (o, e) => wasCalled = true;
            _detector.NotColliding += (o, e) => wasCalled = true;
        }

        [Test]
        public void CheckForCollisions_CollidingTrack_CallingSeparationEvent()
        {
            _detector.CheckForCollision(TrackList);
            Assert.That(wasCalled, Is.EqualTo(true));

        }
    }
}
