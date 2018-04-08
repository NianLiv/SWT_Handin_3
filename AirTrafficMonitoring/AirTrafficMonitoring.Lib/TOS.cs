using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace AirTrafficMonitoring.Lib
{
    // Track Objectification Software
    public class Tos
    {
        private readonly List<Track> _recievedTracks = new List<Track>();
        public List<Track> RecievedTracks => _recievedTracks;

        public Tos(ITransponderReceiver iTransponderReceiver)
        {
            //iTransponderReceiver.TransponderDataReady += (sender, args) =>
            //{
            //    var rawList = args.TransponderData;
            //    foreach (var line in rawList)
            //    {
            //        var newTrack = CreateTrackObject(line);
            //        _recievedTracks.Add(newTrack);
            //    }
            //    PrintTracks();
            //};

            iTransponderReceiver.TransponderDataReady += ITransponderReceiverOnTransponderDataReady;
        }

        private void ITransponderReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            var rawList = rawTransponderDataEventArgs.TransponderData;
            foreach (var line in rawList)
            {
                var newTrack = CreateTrackObject(line);
                _recievedTracks.Add(newTrack);
            }
            PrintTracks();
        }

        private Track CreateTrackObject(string line)
        {
            var rawData = line.Split(';');
            var timeStamp = rawData[4];
            return new Track
            {
                Tag = rawData[0],
                PositionX = int.Parse(rawData[1]),
                PositionY = int.Parse(rawData[2]),
                Altitude = int.Parse(rawData[3]),
                Timestamp = new DateTime(
                    int.Parse(timeStamp.Substring(0, 4)),
                    int.Parse(timeStamp.Substring(4, 2)),
                    int.Parse(timeStamp.Substring(6, 2)),
                    int.Parse(timeStamp.Substring(8, 2)),
                    int.Parse(timeStamp.Substring(10, 2)),
                    int.Parse(timeStamp.Substring(12, 2)),
                    int.Parse(timeStamp.Substring(14, 3))
                )
            };
        }

        private void PrintTracks()
        {
            Console.Clear();
            foreach (var track in _recievedTracks)
            {
                Console.WriteLine(track.Tag);
                Console.WriteLine(track.PositionX);
                Console.WriteLine(track.PositionY);
                Console.WriteLine(track.Altitude);
                Console.WriteLine(track.Timestamp);
                Console.WriteLine();
            }
            _recievedTracks.Clear();
        }

    }
}
