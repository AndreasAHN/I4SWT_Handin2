using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    public class AirTrafficController
    {
        private IAirspace _airspace;
        private ITrackCalculator _trackCalculator;
        private ICondition _condition;
        private IFileWriter _fileWriter;
        private IScreen _screen;



        public bool runner = true; //Kan sætte til false fra resten af programmet, for at standse koden.
        public AirTrafficController()
        {
            // TransponderReceiverClient
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var transponderReceiverClient = new TransponderReceiverClient(transponderDataReceiver);

            // Condition
            _fileWriter = new SeparationConditionLogger("AirplaneSeperations.txt");
            _condition = new Condition(_fileWriter);

            // Airspace
            _trackCalculator = new TrackCalculator();
            _airspace = new Airspace(_trackCalculator);
            transponderReceiverClient.DataReadyEvent += _airspace.HandleDataReadyEvent;
            _airspace.AirSpaceChanged += air_ThresholdReached;

            // Screen
            _screen = new Screen();


        }

        public void air_ThresholdReached(object sender, EventArgs e)//New airplains event
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
