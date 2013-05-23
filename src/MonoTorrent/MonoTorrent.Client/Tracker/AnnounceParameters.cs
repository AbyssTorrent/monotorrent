using System;
using System.Collections.Generic;
using System.Text;
using MonoTorrent.Common;

namespace MonoTorrent.Client.Tracker
{
    public class AnnounceParameters
    {
        public long BytesDownloaded { get; set; }
        public long BytesUploaded { get; set; }
        public long BytesLeft { get; set; }

        public TorrentEvent ClientEvent { get; set; }
        public InfoHash InfoHash { get; set; }

        public string Ipaddress { get; set; }
        public int Port { get; set; }
        public string PeerId { get; set; }
        public string ClientVersion { get; set; }

        public bool RequireEncryption { get; set; }
        public bool SupportsEncryption { get; set; }


        public AnnounceParameters() { }
    }
}