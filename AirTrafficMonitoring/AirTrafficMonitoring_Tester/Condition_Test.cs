using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;
using System.IO;

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[1], conflictAirplain1[0]);
            Assert.AreEqual(_testData[2], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

        [Test]
        public void Test_MaxBoundary_xy_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 5000, Y = 5000, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }


        [Test]
        public void Test_MidBoundary_xy_SeperationFound()
        {
            _testData.Add(new Track { Tag = "GPJ740", X = 2500, Y = 2500, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 3750, Y = 3750, Z = 2500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });

            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);

            Assert.IsTrue(condition.TooClose(_testData));


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }

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


            List<Track> conflictAirplain1 = condition.GetConflictAirplain1();
            List<Track> conflictAirplain2 = condition.GetConflictAirplain2();

            Assert.AreEqual(_testData[0], conflictAirplain1[0]);
            Assert.AreEqual(_testData[1], conflictAirplain2[0]);

            File.Delete("AirplaneSeperations.txt");
        }


        public void Test_SeperationNoFound()
        {
            IFileWriter _fileWriter;
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            Condition condition = new Condition(_fileWriter);
        }
    }
}
