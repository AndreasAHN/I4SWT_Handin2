using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface IFileWriter
    {
        void WriteToFile(Track track1, Track track2);
    }
}
