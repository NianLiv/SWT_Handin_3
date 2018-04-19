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
    class TrackCalculatorUnitTest
    {
        [Test]
        public void CalculateVelocity_IsEqualTo07()
        {
            var Velocity = Math.Round(TrackCalculator.CalculateVelocity(5, 5, 10, 10, DateTime.Now - TimeSpan.FromSeconds(10), DateTime.Now), 1);
            Assert.That(Velocity, Is.EqualTo(0.7));
        }

        [Test]
        public void CalculateCourse_IsEqualTo()
        {
            var Course = TrackCalculator.CalculateCourse(5, 5, 10, 10);
            Assert.That(Course, Is.EqualTo(45));
        }
    }
}
