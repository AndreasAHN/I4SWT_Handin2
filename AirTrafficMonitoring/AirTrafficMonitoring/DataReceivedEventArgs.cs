using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(List<Track> tracks) { data = tracks; }
        public List<Track> data { get; set; }
    }
}
