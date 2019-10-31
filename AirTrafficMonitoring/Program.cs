﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    public class Program
    {
        private static ICondition _condition;
        private static IFileWriter _fileWriter;
        private static IAirspace _airspace;
        private static ITrackCalculator _trackCalculator;
        private static IScreen _screen;



        public static bool runner = true; //Kan sætte til false, fra resten af programet, for at standse koden.
        static void Main(string[] args)
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
            _airspace.AirSpaceChanged += HandleAirspaceChangedEvent;

            // Screen
            _screen = new Screen();


            while (runner)
            {
                Thread.Sleep(1000);
            }
        }

        public static void HandleAirspaceChangedEvent(object sender, EventArgs e)
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
