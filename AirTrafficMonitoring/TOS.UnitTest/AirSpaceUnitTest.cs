using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;

namespace TOS.UnitTest
{
    [TestFixture]
    class AirSpaceUnitTest
    {
        //Testing with Bounday values
        [TestCase(10000, 10000, 500, true)]
        [TestCase(10000, 90000, 500, true)]
        [TestCase(90000, 10000, 500, true)]
        [TestCase(90000, 90000, 500, true)]
        [TestCase(10000, 10000, 20000, true)]
        [TestCase(10000, 90000, 20000, true)]
        [TestCase(90000, 10000, 20000, true)]
        [TestCase(90000, 90000, 500, true)]
        [TestCase(9999, 15000, 500, false)]
        [TestCase(15000, 9999, 500, false)]
        [TestCase(15000, 15000, 499, false)]
        [TestCase(90001, 10000, 500, false)]
        [TestCase(10000, 90001, 500, false)]
        [TestCase(60000, 60000, 20001, false)]
        public void IsInvalidAirSpace_ReturnTrue(int x, int y, int altitude, bool result)
        {
            var _uut = new AirSpace();
            var _track = Substitute.For<ITrack>();
            _track.PositionX = x;
            _track.PositionY = y;
            _track.Altitude = altitude;

            Assert.That(_uut.IsInValidAirSpace(_track), Is.EqualTo(result));
        }
    }
}
