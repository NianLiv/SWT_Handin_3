using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AirTrafficMonitoring.Lib;

namespace TOS.UnitTest
{
    [TestFixture]
    class PointUnitTest
    {
        [Test]
        public void DistanceTo_PointFiveFive_FromPointTenTen_IsSeven()
        {
            var _uut = new Point(5, 5);
            var result = Math.Round(_uut.DistanceTo(new Point(10, 10)));

            //Result is 7, verified from calculations.
            Assert.That(result, Is.EqualTo(7));
        }
    }
}
