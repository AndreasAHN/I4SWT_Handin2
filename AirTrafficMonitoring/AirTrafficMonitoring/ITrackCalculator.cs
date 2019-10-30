using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface ITrackCalculator
    {
        List<Track> TrackCalculate(List<Track> track);
        int CalculateVelocity(Track newTrack, Track oldTrack);
        int CalculateCompassCourse(Track newTrack, Track oldTrack);

    }
}
