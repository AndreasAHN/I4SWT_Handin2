using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Airspace : IAirspace
    {
        private List<Track> tracks;
        private ITrackCalculator _trackCalculator;

        public Airspace(ITrackCalculator trackCalculator)
        {
            tracks = new List<Track>();
            _trackCalculator = trackCalculator;
        }

        public event EventHandler AirSpaceChanged;
        protected virtual void AirTrafficController(EventArgs e)
        {
            EventHandler handler = AirSpaceChanged;
            handler?.Invoke(this, e);
        }



        public void HandleDataReadyEvent(object sender, DataReceivedEventArgs e)
        {
            clearTracks();

            List<Track> bufTracks = e.data;

            if (bufTracks.Count != 0)
            {
                for (int i = 0; i < bufTracks.Count(); i++)
                {
                    if (((500 < bufTracks[i].Z) || (bufTracks[i].Z < 20000)) && (bufTracks[i].X < 80000) && (bufTracks[i].Y < 80000))
                    {
                        tracks.Add(bufTracks[i]);
                    }
                }

                tracks = _trackCalculator.TrackCalculate(tracks).ToList();

                AirTrafficController(EventArgs.Empty);
            }
        }


     
        public List<Track> GetTracks()
        {
            return tracks;
        }


        public void clearTracks()
        {
            tracks.Clear();
        }
    }
}
