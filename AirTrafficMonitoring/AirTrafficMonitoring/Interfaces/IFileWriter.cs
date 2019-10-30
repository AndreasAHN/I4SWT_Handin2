using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    interface IFileWriter
    {
        void WriteToLog(Track track1, Track track2);
    }
}
