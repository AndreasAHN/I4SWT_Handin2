using System;
using System.IO;
using System.Text;

namespace AirTrafficMonitoring
{
    public class SeparationConditionLogger : IFileWriter
    {
        private readonly string _filename;

        public SeparationConditionLogger(string filename)
        {
            _filename = filename;
        }

        public void WriteToFile(Track track1, Track track2)
        {
            string toWrite = $"{DateTime.Now} - {track1.Tag} - {track2.Tag}\n";
            FileStream fs = new FileStream(_filename, FileMode.Append);
            fs.Write(Encoding.ASCII.GetBytes(toWrite), 0, toWrite.Length);
            fs.Close();
        }
    }
}
