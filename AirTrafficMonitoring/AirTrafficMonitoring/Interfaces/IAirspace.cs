using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    interface IAirspace
    {
        void HandleDataReadyEvent(object sender, DataReceivedEventArgs e);

        List<Track> GetTracks();

        void clearTracks();




    }
}
