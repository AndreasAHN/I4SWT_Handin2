using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    class Program
    {
        private static ICondition _condition;
        private static IFileWriter _fileWriter;
        private static Airspace _airspace;
        private static Screen _screen;



        public static bool runner = true; //Kan sætte til false, fra resten af programet, for at standse koden.
        static void Main(string[] args)
        {
            // TransponderReceiverClient
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var transponderReceiverClient = new TransponderReceiverClient(transponderDataReceiver);

            // Condition
            _fileWriter = new SeperationConditionLogger("AirplaneSeperations.txt");
            _condition = new Condition(_fileWriter);

            // Airspace            
            _airspace = new Airspace();
            transponderReceiverClient.DataReadyEvent += _airspace.HandleDataReadyEvent;
            _airspace.AirSpaceChanged += air_ThresholdReached;

            // Screen
            _screen = new Screen();


            while (runner)
            {
                Thread.Sleep(1000);
            }
        }

        static void air_ThresholdReached(object sender, EventArgs e)//New airplains event
        {
            _screen.printTracks(_airspace.GetTracks());
            _condition.TooClose(_airspace.GetTracks());

            if (_condition.GetSeperation())
            {
                for (int x = 0; x < _condition.GetConflictAirplain1().Count(); x++)
                {
                    _screen.printConflict(_condition.GetConflictAirplain1()[x], _condition.GetConflictAirplain2()[x]);
                }
            }
        }
    }
}
