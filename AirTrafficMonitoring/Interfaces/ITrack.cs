using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    interface ITrack
    {
        string Tag { get; set; }

        int X { get; set; }

        int Y { get; set; }

        int Z { get; set; }

        int Velocity { get; set; }

        int CompassCourse { get; set; }

        DateTime Timestamp { get; set; }
    }
}
