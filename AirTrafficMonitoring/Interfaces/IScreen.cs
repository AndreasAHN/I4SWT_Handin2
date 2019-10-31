using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    interface IScreen
    {
        List<string> GetprintTracksOutput();
        List<string> GetprintConflictOutput();
        void printTracks(List<Track> tracks);

        void printConflict(Track conflictTrack1, Track conflictTrack2);
    }
}
