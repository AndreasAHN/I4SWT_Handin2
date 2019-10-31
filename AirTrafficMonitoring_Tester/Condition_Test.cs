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
        private List<Track> _testData;

        [SetUp]
        public void Setup()
        {
            _testData = new List<Track>();
        }

        //Vertical = 5000 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_x_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 1, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 5001, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List <Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 5001 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_x_SeperationNotFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 1, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 5002, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsFalse(condition.TooClose(_testData));
            Assert.IsFalse(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(conflictAirplain1.Count(), 0);
            Assert.AreEqual(conflictAirplain2.Count(), 0);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 5000 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_y_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 1, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 5001, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 5001 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_y_SeperationNotFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 1, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 5002, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsFalse(condition.TooClose(_testData));
            Assert.IsFalse(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(conflictAirplain1.Count(), 0);
            Assert.AreEqual(conflictAirplain2.Count(), 0);

            File.Delete("AirplaneSeperations.txt");
        }


        //Vertical = 4999,24 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_xy_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 6035, Y = 6035, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 5000,66 and Horizontal = 0
        [Test]
        public void Test_MaxBoundary_xy_SeperationNotFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 6036, Y = 6036, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsFalse(condition.TooClose(_testData));
            Assert.IsFalse(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(conflictAirplain1.Count(), 0);
            Assert.AreEqual(conflictAirplain2.Count(), 0);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 0 and Horizontal = 300
        [Test]
        public void Test_MaxBoundary_z_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2501, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2801, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 0 and Horizontal = 301
        [Test]
        public void Test_MaxBoundary_z_SeperationNotFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2501, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2802, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsFalse(condition.TooClose(_testData));
            Assert.IsFalse(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(conflictAirplain1.Count(), 0);
            Assert.AreEqual(conflictAirplain2.Count(), 0);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 2500 and Horizontal = 0
        [Test]
        public void Test_MidBoundary_x_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 5000, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 2500 and Horizontal = 0
        [Test]
        public void Test_MidBoundary_y_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 5000, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 0 and Horizontal = 150
        [Test]
        public void Test_MidBoundary_z_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2650, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 2500,33 and Horizontal = 0
        [Test]
        public void Test_MidBoundary_xy_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 4268, Y = 4268, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        //Vertical = 0 and Horizontal = 0
        [Test]
        public void Test_MinBoundary_xyz_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));
            Assert.IsTrue(condition.GetSeperation());

            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }


        //Vertical = 14142,14 and Horizontal = 2500
        [Test]
        public void Test_SeperationNotFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 10000, Y = 10000, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 20000, Y = 20000, Z = 5000, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 30000, Y = 30000, Z = 7500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsFalse(condition.TooClose(_testData));
            Assert.IsFalse(condition.GetSeperation());

            File.Delete("AirplaneSeperations.txt");
        }


        //Vertical = 14142,14 and Horizontal = 2500
        [Test]
        public void Test_SeperationFoundMultipleTimes() //Time on sepration must not change
        {
            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            bool firstRun = true;
            DateTime start = new DateTime();
            
            for (int i = 0; i < 10; i++)
            {
                _testData.Clear();

                if (firstRun)
                {
                    start = DateTime.Now;

                    _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
                    _testData.Add(new Track { Tag = "QRM275", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
                    _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

                    firstRun = false;
                }
                else
                {
                    _testData.Add(new Track { Tag = "GPJ740", X = (2500 + i), Y = (2500 + i), Z = (2500 + i), Timestamp = DateTime.Now });
                    _testData.Add(new Track { Tag = "QRM275", X = (2500 + i), Y = (2500 + i), Z = (2500 + i), Timestamp = DateTime.Now });
                    _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });
                }

                Assert.IsTrue(condition.TooClose(_testData));
                Assert.IsTrue(condition.GetSeperation());

                List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
                List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

                Assert.AreEqual(_testData[0].Tag, conflictAirplain1[0].Tag);
                Assert.AreEqual(_testData[1].Tag, conflictAirplain2[0].Tag);
                Assert.AreEqual(start, conflictAirplain1[0].Timestamp);
                Assert.AreEqual(start, conflictAirplain2[0].Timestamp);

                Thread.Sleep(500);
            }



            File.Delete("AirplaneSeperations.txt");
        }
    }
}
