using System;
using System.Collections.Generic;
using System.Text;

namespace MonoTorrent.Client.Tracker
{
    public class ScrapeParameters
    {
        public InfoHash InfoHash { get; set; }
        public string ClientVersion { get; set; }

        public ScrapeParameters() { }
    }
}
