using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace TOS.UnitTest
{
    [TestFixture()]
    class ConsoleViewUnitTest
    {
        private IRender _uut;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            // The faked IOutpus must return a screen size to ConsoleViews ctor.
            _output = Substitute.For<IOutput>();
            _output.When(x => x.GetLargetsScreenSize(out int w, out int h)).Do(x =>
            {
                x[0] = 80;
                x[1] = 40;
            });

            _uut = new ConsoleView(_output);
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesGetLargetsScreenSizeCall()
        {
            _output.Received(1).GetLargetsScreenSize(out var w, out var h);
        }

    }
}
