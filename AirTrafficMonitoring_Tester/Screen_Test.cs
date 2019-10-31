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
            consolReader.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            consolReader.Start();

            Screen screen = new Screen();
            screen.printTracks(_testData);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            List<string> expectedResult = new List<string>();
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < _testData.Count(); i++)
            {
                expectedResult.Add(i + ":\tTag: " + _testData[i].Tag + "\tCoordinates: (" + _testData[i].X + ", " + _testData[i].Y + ", " + _testData[i].Z + ")\tSpeed: " + _testData[i].Velocity + "\tBearing: " + _testData[i].CompassCourse + "\tTime: " + _testData[i].Timestamp);
            }
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("\n Orginale oven over:");

            Console.WriteLine("\n\n Forventet output:\n");
            Console.WriteLine(expectedResult);

            Console.WriteLine("\n\n Opnåede output:\n");
            Console.WriteLine(result);

            Assert.IsTrue(expectedResult.Contains(result));
        }

        [Test]
        public void Test_PrintTracksOutput()
        {
            List<Track> _testData = new List<Track>();
            _testData.Add(new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            Screen screen = new Screen();
            screen.printTracks(_testData);

            List<string> expectedResult = new List<string>();
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < _testData.Count(); i++)
            {
                expectedResult.Add(i + ":\tTag: " + _testData[i].Tag + "\tCoordinates: (" + _testData[i].X + ", " + _testData[i].Y + ", " + _testData[i].Z + ")\tSpeed: " + _testData[i].Velocity + "\tBearing: " + _testData[i].CompassCourse + "\tTime: " + _testData[i].Timestamp);
            }
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");

            List<string> output = screen.PrintTracksOutput.ToList();
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.Equals(expectedResult[i], output[i]);
            }
        }

        [Test]
        public void Test_PrintConflict()
        {
            Track _testData1 = (new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            Track _testData2 = (new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });

            var consolReader = new Process();
            consolReader.StartInfo.UseShellExecute = false;
            consolReader.StartInfo.RedirectStandardOutput = true;
            consolReader.StartInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            consolReader.Start();

            Screen screen = new Screen();
            screen.printConflict(_testData1, _testData2);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            string expectedResult = "\n" + "!WARNING-SEPERATION! " + _testData1.Tag + " and " + _testData2.Tag + ", at: " + _testData1.Timestamp;

            Console.WriteLine("\n Orginale oven over:");

            Console.WriteLine("\n\n Forventet output:\n");
            Console.WriteLine(expectedResult);

            Console.WriteLine("\n\n Opnåede output:\n");
            Console.WriteLine(result);

            Assert.IsTrue(expectedResult.Contains(result));
        }


        [Test]
        public void Test_PrintConflictOutput()
        {
            Track _testData1 = (new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            Track _testData2 = (new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });

            Screen screen = new Screen();
            screen.printConflict(_testData1, _testData2);

            string expectedResult = "\n" + "!WARNING-SEPERATION! " + _testData1.Tag + " and " + _testData2.Tag + ", at: " + _testData1.Timestamp;
            List<string> buf = screen.PrintConflictOutput.ToList();
            string output = buf[0];
            Assert.Equals(expectedResult, output);
        }
    }
}
