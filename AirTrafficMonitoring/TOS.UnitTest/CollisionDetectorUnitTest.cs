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
        private CollisionEventArgs _fakeArgs;
        private int SeparationEventCounter = 0;
        private int NotCollidingEventCounter = 0;

        [SetUp]
        public void SetUp()
        {
            _uut = new CollisionDetector();

            _uut.Separation += (e, args) => SeparationEventCounter++;
            _uut.NotColliding += (e, args) => NotCollidingEventCounter++;
        }

        public void SeparationEventCalled()
        {
            _uut.CheckForCollision(null);
        }
    }
}
