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
        public static Airspace airSpace = new Airspace();
        public static Condition condition = new Condition();
        public static Screen screen = new Screen();

        public static bool runner = true; //Kan sætte til false, fra resten af programet, for at standse koden.

        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var transponderReceiverClient = new TransponderReceiverClient(transponderDataReceiver);
            airSpace.AirSpaceChanged += air_ThresholdReached;

            transponderReceiverClient.DataReadyEvent += airSpace.HandleDataReadyEvent;



            while (runner)
            {
                Thread.Sleep(1000);
            }
        }

        static void air_ThresholdReached(object sender, EventArgs e)//New airplains event
        {
            screen.printTracks(airSpace.GetTracks());
            condition.TooClose(airSpace.GetTracks());

            if (condition.GetSeperation())
            {
                for (int x = 0; x < condition.conflictTrack1.Count(); x++)
                {
                    screen.printConflict(condition.GetConflictAirplain1()[x], condition.GetConflictAirplain2()[x]);
                }
            }
        }
    }
}
