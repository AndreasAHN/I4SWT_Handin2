using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Screen : IScreen
    {
        public List<string> PrintTracksOutput { get; private set; }
        public List<string> PrintConflictOutput { get; private set; }

        public Screen()
        {
            PrintTracksOutput = new List<string>();
            PrintConflictOutput = new List<string>();
        }

        public void printTracks(List<Track> tracks)
        {
            PrintTracksOutput.Clear();
            Console.Clear();
            string output1 = "-------------------------------------------------------------------------------------------------------------------------";
            PrintTracksOutput.Add(output1);
            Console.WriteLine(output1);

            if (tracks.Count != 0)
            {
                for (int i = 0; i < tracks.Count; i++)
                {
                    string output2 = i + ":\tTag: " + tracks[i].Tag + "\tCoordinates: (" + tracks[i].X + ", " + tracks[i].Y + ", " + tracks[i].Z + ")\tSpeed: " + tracks[i].Velocity + "\tBearing: " + tracks[i].CompassCourse + "\tTime: " + tracks[i].Timestamp;
                    PrintTracksOutput.Add(output2);
                    Console.WriteLine(output2);
                }
            }

            PrintTracksOutput.Add(output1);
            Console.WriteLine(output1);
        }

        public void printConflict(Track conflictTrack1, Track conflictTrack2)
        {
            string output = "\n" + "!WARNING-SEPERATION! " + conflictTrack1.Tag + " and " + conflictTrack2.Tag + ", at: " + conflictTrack1.Timestamp;
            PrintConflictOutput.Add(output);
            Console.WriteLine(output);
        }
    }
}
