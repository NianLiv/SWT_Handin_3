using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;

namespace TOS.UnitTest
{
    [TestFixture]
    class FileLoggerUnitTest
    {
        private Track _track1;
        private Track _track2;
        private CollisionPairs _collisionPairs;
        private FileLogger _fileLogger;
        private List<CollisionPairs> _collisionlist;

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
            _collisionlist = new List<CollisionPairs>();
            _collisionlist.Add(_collisionPairs);
            _fileLogger = new FileLogger(@"CollisionTestLog.txt");
        }

        [Test]
        public void LogCollitionToFile()
        {
            _fileLogger.LogCollisionToFile(_collisionlist);

            using (StreamReader reader = new StreamReader(@"CollisionTestLog.txt", true))
            {
                Assert.That(reader.ReadLine().Contains("Tag: AAA111"));
            }
        }


        [Test]
        public void LogCollisionToFile_fail()
        {
            _fileLogger.LogCollisionToFile(_collisionlist);

            using (StreamReader reader = new StreamReader(@"CollisionTestLog.txt", true))
            {
                Assert.That(reader.ReadLine().Contains("Tag: asdrgf").Equals(false));
            }

        }

    }
}
