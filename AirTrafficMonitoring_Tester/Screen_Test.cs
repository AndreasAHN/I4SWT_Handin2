﻿using System;
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


        public static IEnumerable<TestCaseData> TestCasesData_PrintTracks
        {
            get
            {
                yield return new TestCaseData //Vertical = 14142,14 and Horizontal = 2500
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_PrintTracks");
            }
        }


        [TestCaseSource("TestCasesData_PrintTracks")]
        public void Test_PrintTracks(List<Track> _testData)
        {
            var consolReader = new Process();
            consolReader.StartInfo.UseShellExecute = false;
            consolReader.StartInfo.RedirectStandardOutput = true;
            consolReader.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            consolReader.Start();

            Screen screen = new Screen();
            screen.printTracks(_testData);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            string expectedResult ="-------------------------------------------------------------------------------------------------------------------------";
            for (int i = 0; i < _testData.Count(); i++)
            {
                expectedResult += (i + ":\tTag: " + _testData[i].Tag + "\tCoordinates: (" + _testData[i].X + ", " + _testData[i].Y + ", " + _testData[i].Z + ")\tSpeed: " + _testData[i].Velocity + "\tBearing: " + _testData[i].CompassCourse + "\tTime: " + _testData[i].Timestamp);
            }
            expectedResult += ("-------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("\n Orginale oven over:");

            Console.WriteLine("\n\n Forventet output:\n");
            Console.WriteLine(expectedResult);

            Console.WriteLine("\n\n Opnåede output:\n");
            Console.WriteLine(result);

            Assert.IsTrue(expectedResult.Contains(result));
        }



        [TestCaseSource("TestCasesData_PrintTracks")]
        public void Test_PrintTracksOutput(List<Track> _testData)
        {
            Screen screen = new Screen();
            screen.printTracks(_testData);

            List<string> expectedResult = new List<string>();
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < _testData.Count(); i++)
            {
                expectedResult.Add(i + ":\tTag: " + _testData[i].Tag + "\tCoordinates: (" + _testData[i].X + ", " + _testData[i].Y + ", " + _testData[i].Z + ")\tSpeed: " + _testData[i].Velocity + "\tBearing: " + _testData[i].CompassCourse + "\tTime: " + _testData[i].Timestamp);
            }
            expectedResult.Add("-------------------------------------------------------------------------------------------------------------------------");

            List<string> output = screen.GetprintTracksOutput().ToList();
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], output[i]);
            }
        }



        public static IEnumerable<TestCaseData> TestCasesData_PrintConflict
        {
            get
            {
                yield return new TestCaseData //Vertical = 14142,14 and Horizontal = 2500
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_PrintConflict");
            }
        }


        [TestCaseSource("TestCasesData_PrintConflict")]
        public void Test_PrintConflict(List<Track> _testData)
        {
            var consolReader = new Process();
            consolReader.StartInfo.UseShellExecute = false;
            consolReader.StartInfo.RedirectStandardOutput = true;
            consolReader.StartInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            consolReader.Start();

            Screen screen = new Screen();
            screen.printConflict(_testData[0], _testData[1]);

            string result = consolReader.StandardOutput.ReadToEnd();
            consolReader.WaitForExit();

            string expectedResult = "\n" + "!WARNING-SEPERATION! " + _testData[0].Tag + " and " + _testData[1].Tag + ", at: " + _testData[0].Timestamp;

            Console.WriteLine("\n Orginale oven over:");

            Console.WriteLine("\n\n Forventet output:\n");
            Console.WriteLine(expectedResult);

            Console.WriteLine("\n\n Opnåede output:\n");
            Console.WriteLine(result);

            Assert.IsTrue(expectedResult.Contains(result));
        }


        [TestCaseSource("TestCasesData_PrintConflict")]
        public void Test_PrintConflictOutput(List<Track> _testData)
        {
            Screen screen = new Screen();
            screen.printConflict(_testData[0], _testData[1]);

            string expectedResult = "\n" + "!WARNING-SEPERATION! " + _testData[0].Tag + " and " + _testData[1].Tag + ", at: " + _testData[0].Timestamp;
            List<string> buf = screen.GetprintConflictOutput().ToList();
            string output = buf[0];
            Assert.AreEqual(expectedResult, output);
        }
    }
}
