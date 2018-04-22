using AirTrafficMonitoring.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Lib
{
    public class FileLogger : ILog
    {
        public string FilePath { get; set; }

        public FileLogger(string path = @"CollisionLog.txt") => FilePath = path;

        public void LogCollisionToFile(List<CollisionPairs> collisionPairs)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                foreach (var pair in collisionPairs)
                {
                    writer.WriteLine(pair.ToString());
                }
            }
        }
    }
}
