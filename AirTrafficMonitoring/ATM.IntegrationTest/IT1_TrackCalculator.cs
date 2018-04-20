using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;

namespace ATM.IntegrationTest
{
    [TestFixture]
    public class IT1_TrackCalculator
    {
        private TrackCalculator _uut;
        private Point point;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackCalculator();
            point = new Point(10, 10);
        }


    }
}
