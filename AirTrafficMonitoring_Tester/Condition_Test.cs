using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    public class Condition_Test
    {
        public IFileWriter _fileWriter;
        public Condition condition;

        [SetUp]
        public void Setup()
        {
            _fileWriter = new SeparationConditionLogger("AirplaneSeperations.txt");
            _fileWriter = new SeparationConditionLogger("AirplaneSeperations.txt");
            condition = new Condition(_fileWriter);
        }


        public static IEnumerable<TestCaseData> TestCasesData_SeperationFound
        {
            get
            {
                yield return new TestCaseData //Vertical = 5000 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track {Tag = "GPJ740", X = 1, Y = 2500, Z = 2500, Timestamp = DateTime.Now},
                        new Track {Tag = "QRM275", X = 5001, Y = 2500, Z = 2500, Timestamp = DateTime.Now},
                        new Track {Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now}
                    }
                ).SetName("Test_MaxBoundary_x_SeperationFound");

                yield return new TestCaseData //Vertical = 5000 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 1, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 5001, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_y_SeperationFound");

                yield return new TestCaseData //Vertical = 4999,24 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 6035, Y = 6035, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_xy_SeperationFound");

                yield return new TestCaseData //Vertical = 0 and Horizontal = 300
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2501, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2801, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_z_SeperationFound");

                yield return new TestCaseData //Vertical = 2500 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 5000, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MidBoundary_x_SeperationFound");

                yield return new TestCaseData //Vertical = 2500 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 5000, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MidBoundary_y_SeperationFound");

                yield return new TestCaseData //Vertical = 2500,33 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 4268, Y = 4268, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MidBoundary_xy_SeperationFound");

                yield return new TestCaseData //Vertical = 0 and Horizontal = 150
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2650, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MidBoundary_z_SeperationFound");

                yield return new TestCaseData //Vertical = 0 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MinBoundary_xyz_SeperationFound");
            }
        }



        [Test, TestCaseSource("TestCasesData_SeperationFound")]
        public void Test_SeperationFound(List<Track> _testData)
        {
            Assert.IsTrue(condition.TooClose(_testData));

            File.Delete("AirplaneSeperations.txt");
        }

        [Test, TestCaseSource("TestCasesData_SeperationFound")]
        public void Test_SeperationFound_GetFunction(List<Track> _testData)
        {
            condition.TooClose(_testData);
            Assert.IsTrue(condition.GetSeperation());

            File.Delete("AirplaneSeperations.txt");
        }

        [Test, TestCaseSource("TestCasesData_SeperationFound")]
        public void Test_SeperationFound_TheRightPlainsFound(List<Track> _testData)
        {
            condition.TooClose(_testData);

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }



        public static IEnumerable<TestCaseData> TestCasesData_SeperationNotFound
        {
            get
            {
                yield return new TestCaseData //Vertical = 5001 and Horizontal = 0
                    (
                        new List<Track>()
                        {
                            new Track { Tag = "GPJ740", X = 1, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                            new Track { Tag = "QRM275", X = 5002, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                            new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                        }
                    ).SetName("Test_MaxBoundary_x_SeperationNotFound");

                yield return new TestCaseData //Vertical = 5001 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 1, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 5002, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_y_SeperationNotFound");

                yield return new TestCaseData //Vertical = 5000,66 and Horizontal = 0
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 6036, Y = 6036, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_xy_SeperationNotFound");

                yield return new TestCaseData //Vertical = 0 and Horizontal = 301
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2501, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2802, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_MaxBoundary_z_SeperationNotFound");

                yield return new TestCaseData //Vertical = 14142,14 and Horizontal = 2500
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 10000, Y = 10000, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 20000, Y = 20000, Z = 5000, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 30000, Y = 30000, Z = 7500, Timestamp = DateTime.Now }
                    }
                ).SetName("Test_SeperationNotFound");
            }
        }



        [Test, TestCaseSource("TestCasesData_SeperationNotFound")]
        public void Test_SeperationNotFound(List<Track> _testData)
        {
            Assert.IsFalse(condition.TooClose(_testData));

            File.Delete("AirplaneSeperations.txt");
        }

        [Test, TestCaseSource("TestCasesData_SeperationNotFound")]
        public void Test_SeperationNotFound_GetFunktion(List<Track> _testData)
        {
            condition.TooClose(_testData);
            Assert.IsFalse(condition.GetSeperation());

            File.Delete("AirplaneSeperations.txt");
        }

        [Test, TestCaseSource("TestCasesData_SeperationNotFound")]
        public void Test_SeperationNotFound_NoConflictAirplains(List<Track> _testData)
        {
            condition.TooClose(_testData);

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(conflictAirplain1.Count(), 0);
            Assert.AreEqual(conflictAirplain2.Count(), 0);

            File.Delete("AirplaneSeperations.txt");
        }




        public static IEnumerable<TestCaseData> TestCasesData_SeperationFoundMultipleTimes
        {
            get
            {
                yield return new TestCaseData //Vertical = 14142,14 and Horizontal = 2500
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now }
            }
                ).SetName("Test_SeperationFoundMultipleTimes");
            }
        }

        [TestCaseSource("TestCasesData_SeperationFoundMultipleTimes")]
        public void Test_SeperationFoundMultipleTimes_After10TimesStillOnlyOne(List<Track> _testData) //Time on sepration must not change
        {
            List<Track> _testDataBuf = _testData.ToList();

            Thread.Sleep(500);

            condition.TooClose(_testDataBuf);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < _testDataBuf.Count(); j++)
                {
                    _testDataBuf[j].X = (_testDataBuf[j].X + 1);
                    _testDataBuf[j].Y = (_testDataBuf[j].Y + 1);
                    _testDataBuf[j].Z = (_testDataBuf[j].Z + 1);
                    _testDataBuf[j].Timestamp = DateTime.Now;
                }

                Thread.Sleep(100);
            }

            Assert.AreEqual(1, condition.GetConflictAirplain1().Count());
            Assert.AreEqual(1, condition.GetConflictAirplain2().Count());

            File.Delete("AirplaneSeperations.txt");
        }


        [TestCaseSource("TestCasesData_SeperationFoundMultipleTimes")]
        public void Test_SeperationFoundMultipleTimes_TimesGreaterThanInput_WhenTheSeprationFound(List<Track> _testData) //Time on sepration must not change
        {
            List<Track> _testDataBuf = _testData.ToList();

            DateTime start1 = _testDataBuf[0].Timestamp;
            DateTime start2 = _testDataBuf[1].Timestamp;

            Thread.Sleep(500);

            condition.TooClose(_testDataBuf);
            Assert.GreaterOrEqual(condition.GetConflictAirplain1()[0].Timestamp, start1);
            Assert.GreaterOrEqual(condition.GetConflictAirplain2()[0].Timestamp, start2);

            File.Delete("AirplaneSeperations.txt");
        }


        [TestCaseSource("TestCasesData_SeperationFoundMultipleTimes")]
        public void Test_SeperationFoundMultipleTimes_FoundSeperation10Times(List<Track> _testData) //Time on sepration must not change
        {
            List<Track> _testDataBuf = _testData.ToList();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < _testDataBuf.Count(); j++)
                {
                    _testDataBuf[j].X = (_testDataBuf[j].X + 1);
                    _testDataBuf[j].Y = (_testDataBuf[j].Y + 1);
                    _testDataBuf[j].Z = (_testDataBuf[j].Z + 1);
                    _testDataBuf[j].Timestamp = DateTime.Now;
                }

                Assert.IsTrue(condition.TooClose(_testDataBuf));

                Thread.Sleep(100);
            }

            File.Delete("AirplaneSeperations.txt");
        }


        [TestCaseSource("TestCasesData_SeperationFoundMultipleTimes")]
        public void Test_SeperationFoundMultipleTimes_FoundSeperation10Times_RightAirplains(List<Track> _testData) //Time on sepration must not change
        {
            List<Track> _testDataBuf = _testData.ToList();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < _testDataBuf.Count(); j++)
                {
                    _testDataBuf[j].X = (_testDataBuf[j].X + 1);
                    _testDataBuf[j].Y = (_testDataBuf[j].Y + 1);
                    _testDataBuf[j].Z = (_testDataBuf[j].Z + 1);
                    _testDataBuf[j].Timestamp = DateTime.Now;
                }

                condition.TooClose(_testDataBuf);

                Assert.AreEqual(_testDataBuf[0].Tag, condition.GetConflictAirplain1()[0].Tag);
                Assert.AreEqual(_testDataBuf[1].Tag, condition.GetConflictAirplain2()[0].Tag);

                Thread.Sleep(100);
            }

            File.Delete("AirplaneSeperations.txt");
        }



        public static IEnumerable<TestCaseData> TestCasesData_SeperationFoundMultipleAirPlains
        {
            get
            {
                yield return new TestCaseData //Vertical = 14142,14 and Horizontal = 2500
                (
                    new List<Track>()
                    {
                        new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now },
                        new Track { Tag = "QRM275", X = 2501, Y = 2501, Z = 2501, Timestamp = DateTime.Now },
                        new Track { Tag = "ONC788", X = 2502, Y = 2502, Z = 2502, Timestamp = DateTime.Now }
            }
                ).SetName("Test_SeperationFoundMultipleTimes");
            }
        }

        [Test, TestCaseSource("TestCasesData_SeperationFoundMultipleAirPlains")]
        public void Test_SeperationFound_MultipleAirplains(List<Track> _testData)
        {
            condition.TooClose(_testData);

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(3, conflictAirplain1.Count());
            Assert.AreEqual(3, conflictAirplain2.Count());

            File.Delete("AirplaneSeperations.txt");
        }
    }
}
