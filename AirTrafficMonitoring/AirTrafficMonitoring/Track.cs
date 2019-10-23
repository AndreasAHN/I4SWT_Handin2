using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    // <summary>
    // Track is a data class contaning all information of a track
    // Track will not have any methods due to public properties
    // </summary>
    public class Track
    {
        public string Tag { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int CompassCourse { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
