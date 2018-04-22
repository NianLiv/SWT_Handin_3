using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace TOS.UnitTest
{
    [TestFixture]
    class SubjectUnitTest
    {
        private Tos _uut;
        private AirTrafficMonitoring.Lib.IObserver<Tos> _observer;

        [SetUp]
        public void SetUp()
        {
            _uut = new Tos(Substitute.For<ITransponderReceiver>());
            _observer = Substitute.For<AirTrafficMonitoring.Lib.IObserver<Tos>>();
        }

        [Test]
        public void Attach_NewObserver_ObserverReceivesUpdateCall()
        {
            _uut.Attach(_observer);
            _uut.Notify(_uut);
            _observer.Received().Update(_uut);
        }

        [Test]
        public void Attach_SameObserver_ObserverReceivesUpdateCallOnce()
        {
            _uut.Attach(_observer);
            _uut.Attach(_observer);
            _uut.Notify(_uut);
            _observer.Received(1).Update(_uut);
        }

        [Test]
        public void Detach_Observer_ObserverDidNotReceivesUpdateCall()
        {
            _uut.Attach(_observer);
            _uut.Detach(_observer);
            _uut.Notify(_uut);
            _observer.DidNotReceive().Update(_uut);
        }

        [Test]
        public void Detach_NoObserver_ObserverDidNotReceivesUpdateCall()
        {
            _uut.Detach(_observer);
            _uut.Notify(_uut);
            _observer.DidNotReceive().Update(_uut);
        }

    }
}
