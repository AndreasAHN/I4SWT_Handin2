﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class AirspaceChangedEventArgs : EventArgs
    {
        public List<Track> Tracks;
    }
}
