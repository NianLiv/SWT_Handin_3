using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace AirTrafficMonitoring.Lib
{
    // Track Objectification Software
    public class Tos
    {
        private readonly Dictionary<string, Track> _recievedTracks = new Dictionary<string, Track>();
        public Dictionary<string, Track> RecievedTracks => _recievedTracks;

        public Tos(ITransponderReceiver iTransponderReceiver)
        {
            iTransponderReceiver.TransponderDataReady += (sender, args) =>
            {
                var rawList = args.TransponderData;
                foreach (var line in rawList)
                {
                    var newTrack = CreateTrackObject(line);

                    if(!RecievedTracks.ContainsKey(newTrack.Tag))
                        RecievedTracks.Add(newTrack.Tag, newTrack);
                    else
                    {
                        RecievedTracks[newTrack.Tag] = newTrack;
                    }
                }
                //PrintTracks();
            };
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
                Console.WriteLine(track.Value.Tag);
                Console.WriteLine(track.Value.PositionX);
                Console.WriteLine(track.Value.PositionY);
                Console.WriteLine(track.Value.Altitude);
                Console.WriteLine(track.Value.Timestamp);
                Console.WriteLine();
            }
        }
    }
}
