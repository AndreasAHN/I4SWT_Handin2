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
        public static TrackCalculator trackCalculator = new TrackCalculator();
        public static Condition condition = new Condition();
        public static FileWriter fileWriter = new FileWriter("AirLogger.txt");
        public static Screen screen = new Screen();

        public static bool runner = true; //Kan sætte til false, fra resten af programet, for at standse koden.

        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var transponderReceiverClient = new TransponderReceiverClient(transponderDataReceiver);
            var airSpaceChange = new AirSpaceChange();
            airSpaceChange.AirSpaceChanged += air_ThresholdReached;

            while (runner)
            {
                Thread.Sleep(1000);
            }
        }

        static void air_ThresholdReached(object sender, EventArgs e)//New airplains
        {
            List<Track> tracks = new List<Track>(); //airSpace.getTracks();
            Track ownTrack = new Track(); //airSpace.getOwnTrack();
            //tracks = trackCalculator.calculate(tracks);
            screen.printTracks(tracks);
            condition.TooClose(ownTrack, tracks);
            
            if(condition.GetSeperation())
            {
                screen.printConflict(ownTrack, condition.GetConflictAirplain(), condition.GetConflictDistanceVertical(), condition.GetConflictDistanceHorizontal());
                fileWriter.WriteToLog(ownTrack, condition.GetConflictAirplain());
            }
        }
    }

    class AirSpaceChange
    {
        public event EventHandler AirSpaceChanged;
        protected virtual void AirTrafficController(EventArgs e)
        {
            EventHandler handler = AirSpaceChanged;
            handler?.Invoke(this, e);
        }
    }
}
