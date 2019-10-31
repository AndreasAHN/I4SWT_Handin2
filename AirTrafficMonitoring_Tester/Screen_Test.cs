using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;


namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    public class Screen_Test
    {

        [SetUp]
        public void Setup()
        {
            
        }


        [Test]
        public void Test_PrintTracks()
        {
            List<Track> _testData = new List<Track>();
            _testData.Add(new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            var consolReader = new Process();
            consolReader.StartInfo.UseShellExecute = false;
            consolReader.StartInfo.RedirectStandardOutput = true;
            consolReader.StartInfo.FileName = "csc.exe";
            consolReader.Start();

            Screen screen = new Screen();
            screen.printTracks(_testData);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            Console.WriteLine(result);
        }

        [Test]
        public void Test_PrintConflict()
        {
            Track _testData1 = (new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            Track _testData2 = (new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });

            var consolReader = new Process();
            consolReader.StartInfo.UseShellExecute = false;
            consolReader.StartInfo.RedirectStandardOutput = true;
            consolReader.StartInfo.FileName = "csc.exe";
            consolReader.Start();

            Screen screen = new Screen();
            screen.printConflict(_testData1, _testData2);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            Console.WriteLine(result);
        }
    }
}
