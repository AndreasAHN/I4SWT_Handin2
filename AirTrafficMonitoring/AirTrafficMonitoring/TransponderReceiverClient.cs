using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    public class TransponderReceiverClient : ITransponderReceiverClient
    {
        private readonly ITransponderReceiver _receiver;

        public event EventHandler<DataReceivedEventArgs> DataReadyEvent;

        public Track[] Tracks { get; private set; }

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            this._receiver = receiver;
            this._receiver.TransponderDataReady += HandleTransponderDataReady;
        }

        private void HandleTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Tracks = new Track[e.TransponderData.Count];
            char[] separator = { ';' };
            int trackIndex = 0;

            foreach (var data in e.TransponderData)
            {
                string[] tokens = data.Split(separator);
                Tracks[trackIndex] = new Track()
                {
                    Tag = tokens[0],
                    X = int.Parse(tokens[1]),
                    Y = int.Parse(tokens[2]),
                    Z = int.Parse(tokens[3]),
                    Timestamp = DateTime.ParseExact(tokens[4], "yyyyMMddHHmmssfff", null)
                };

                trackIndex++;
            }

            DataReadyEvent?.Invoke(null, new DataReceivedEventArgs(Tracks.ToList()));
        }
    }
}