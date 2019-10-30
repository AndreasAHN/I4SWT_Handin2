using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface IAirspace
    {
        void HandleDataReadyEvent(object sender, DataReceivedEventArgs e);

        List<Track> GetTracks();

        void clearTracks();

        event EventHandler AirSpaceChanged;
    }
}
