using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoring_Tester
{
    /// <summary>
    /// Used for getting DateTime.Now(), time is changeable for unit testing
    /// </summary>
    public static class SystemTime
    {
        /// <summary> Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for testing or debugging.
        /// </summary>
        public static Func<DateTime> Now = () => DateTime.Now;

        /// <summary> Set time to return when SystemTime.Now() is called.
        /// </summary>
        public static void SetDateTime(DateTime dateTimeNow)
        {
            Now = () => dateTimeNow;
        }

        /// <summary> Resets SystemTime.Now() to return DateTime.Now.
        /// </summary>
        public static void ResetDateTime()
        {
            Now = () => DateTime.Now;
        }
    }
    [TestFixture]
    public class FileWriter_test
    {
        private FileWriter _uut;
        private string _filename;


        [SetUp]
        public void SetUp()
        {
            _filename = "FileWriteTest.txt";
            _uut = new FileWriter(_filename);
        }

        [Test]
        public void FileExists_Test()
        {
            Track t1 = new Track()
            {
                Tag = "Tag1"
            };
            Track t2 = new Track()
            {
                Tag = "Tag2"
            };

            _uut.WriteToLog(t1, t2);
            Assert.IsTrue(File.Exists(_filename));
            File.Delete(_filename);
        }


        //
        // <summary>
        // This test may not be correct, in order to get this right you will have to manipulate SystemTime
        // Mock the system time
        // Current tries: Smocks didn't work as expected.
        // </summary>
        [Test]
        public void WriteAndReadFromFile_SyntaxCorrect()
        {
            Track t1 = new Track()
            {
                Tag = "Tag1"
            };
            Track t2 = new Track()
            {
                Tag = "Tag2"
            };

            // Write to file
            _uut.WriteToLog(t1, t2);
            
            // read line from file
            StreamReader sr = new StreamReader(_filename);
            string line = sr.ReadLine();


            StringAssert.Contains($"{DateTime.Now} - Tag1 - Tag2", line);
            sr.Close();
            File.Delete(_filename);
        }
    }
}
