using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using System;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT5_CollisionPairsTrack
    {
        private Track _track1;
        private Track _track2;
        private CollisionPairs _collisionPairs;

        [SetUp]
        public void SetUp()
        {
            _track1 = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 12000,
                Altitude = 700
            };
            _track2 = new Track
            {
                Tag = "BBB222",
                PositionX = 13000,
                PositionY = 11000,
                Altitude = 1000
            };
            _collisionPairs = new CollisionPairs(_track1, _track2, DateTime.Now);
        }

        [Test]
        public void ToString_ReturnContainsBothTags_MeansTrackPropertyIsCalledCorrect()
        {
            Assert.That(_collisionPairs.ToString(), Contains.Substring(_track1.Tag).And.Contains(_track2.Tag));
        }


    }
}
