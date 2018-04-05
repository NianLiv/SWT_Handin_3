using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM.ClassLib
{
    public class Receiver
    {
        private List<string> _receivedTags;

        public Receiver()
        {
            var transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            transponderReceiver.TransponderDataReady += TransponderReceiverOnTransponderDataReady;
            _receivedTags = new List<string>();
        }

        private void TransponderReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                string[] split = data.Split(';');
                if (!_receivedTags.Contains(split[0]))
                {
                    _receivedTags.Add(split[0]);
                    new TrackObject(split);
                }
            }
        }
    }
}
