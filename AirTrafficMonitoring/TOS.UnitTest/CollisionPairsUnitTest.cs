using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TOS.UnitTest
{
    [TestFixture]
    class CollisionPairsUnitTest
    {
        private Track _track1;
        private Track _track2;

        [SetUp]
        public void SetUp()
        {

            _track1 = new Track
            {
                Altitude = 1000,
                Course = 0,
                Tag = "AAA1111",
                PositionX = 10020,
                PositionY = 10001,
                Timestamp = DateTime.Now,
                Velocity = 0
            };

            _track2 = new Track
            {
                Altitude = 1500,
                Course = 0,
                Tag = "BBB1111",
                PositionX = 10120,
                PositionY = 10101,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
        }



        [Test]
        public void ToString_returnCorrectString()
        {
            var time = new DateTime(2018,01,01);
            CollisionPairs _collisionPairs = new CollisionPairs(_track1, _track2, time);

            Assert.That(_collisionPairs.ToString(), Is.EqualTo("Tag: AAA1111 kolliderer med tag: BBB1111. Tidspunkt: 01-01-2018 00:00:00"));
        }


    }

}