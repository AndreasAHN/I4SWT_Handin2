using System;
using System.IO;
using System.Text;

namespace AirTrafficMonitoring
{
    public class FileWriter
    {
        private readonly string _filename;

        public FileWriter(string filename)
        {
            _filename = filename;
        }

        public void WriteToLog(Track track1, Track track2)
        {
            string toWrite = $"{DateTime.Now} - {track1.Tag} - {track2.Tag}\n";
            FileStream fs = new FileStream(_filename, FileMode.Append);
            fs.Write(Encoding.ASCII.GetBytes(toWrite), 0, toWrite.Length);
            fs.Close();
        }
    }
}
