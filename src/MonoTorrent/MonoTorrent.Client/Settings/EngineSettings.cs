//
// EngineSettings.cs
//
// Authors:
//   Alan McGovern alan.mcgovern@gmail.com
//
// Copyright (C) 2006 Alan McGovern
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//



using MonoTorrent.Client.Encryption;
using System.Reflection;
using System;
using System.Net;
using MonoTorrent.Common;

namespace MonoTorrent.Client
{
    /// <summary>
    /// Represents the Settings which need to be passed to the engine
    /// </summary>
    [Serializable]
    public class EngineSettings : ICloneable
    {
        #region Private Fields

        private int maxOpenStreams = 15;

        #endregion Private Fields

        #region Properties

        /// <summary>
        /// The minimum encryption level to use. "None" corresponds to no encryption.
        /// </summary>
        public EncryptionTypes AllowedEncryption { get; set; }

        /// <summary>
        /// True if you want to enable have surpression
        /// </summary>
        public bool HaveSupressionEnabled { get; set; }

        /// <summary>
        /// The maximum number of connections that can be opened
        /// </summary>
        public int GlobalMaxConnections { get; set; }

        /// <summary>
        /// The maximum number of simultaenous 1/2 open connections
        /// </summary>
        public int GlobalMaxHalfOpenConnections { get; set; }

        /// <summary>
        /// The maximum combined download speed
        /// </summary>
        public int GlobalMaxDownloadSpeed { get; set; }

        /// <summary>
        /// The maximum combined upload speed
        /// </summary>
        public int GlobalMaxUploadSpeed { get; set; }

        /// <summary>
        /// The port to listen to incoming connections on
        /// </summary>
        [Obsolete("Use the constructor overload for ClientEngine which takes a port argument." +
            "Alternatively just use the ChangeEndpoint method at a later stage")]
        public int ListenPort { get; set; }

        /// <summary>
        /// The maximum number of simultaenous open filestreams
        /// </summary>
        public int MaxOpenFiles
        {
            get { return maxOpenStreams; }
            set { maxOpenStreams = value; }
        }

        /// <summary>
        /// The maximum read rate from the harddisk (for all active torrentmanagers)
        /// </summary>
        public int MaxReadRate { get; set; }

        /// <summary>
        /// The maximum write rate to the harddisk (for all active torrentmanagers)
        /// </summary>
        public int MaxWriteRate { get; set; }

        /// <summary>
        /// The IPEndpoint reported to the tracker
        /// </summary>
        public IPEndPoint ReportedAddress { get; set; }

        /// <summary>
        /// If encrypted and unencrypted connections are enabled, specifies if encryption should be chosen first
        /// </summary>
        public bool PreferEncryption { get; set; }

        /// <summary>
        /// The path that torrents will be downloaded to by default
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// Client PeerID Version String (-MO1000-)
        /// </summary>
        public string ClientVersion { get; set; }

        #endregion Properties


        #region Defaults

        private const bool DefaultEnableHaveSupression = false;
        private const string DefaultSavePath = "";
        private const int DefaultMaxConnections = 150;
        private const int DefaultMaxDownloadSpeed = 0;
        private const int DefaultMaxUploadSpeed = 0;
        private const int DefaultMaxHalfOpenConnections = 5;
        private const EncryptionTypes DefaultAllowedEncryption = EncryptionTypes.All;
        private const int DefaultListenPort = 52138;
        private static string DefaultClientVersion = VersionInfo.CreateClientVersion();

        #endregion


        #region Constructors

        public EngineSettings()
            : this(DefaultSavePath, DefaultListenPort, DefaultMaxConnections, DefaultMaxHalfOpenConnections,
                  DefaultMaxDownloadSpeed, DefaultMaxUploadSpeed, DefaultAllowedEncryption)
        {
        }

        public EngineSettings(string defaultSavePath, int listenPort)
            : this(defaultSavePath, listenPort, DefaultMaxConnections, DefaultMaxHalfOpenConnections,
                   DefaultMaxDownloadSpeed, DefaultMaxUploadSpeed, DefaultAllowedEncryption)
        {
        }

        public EngineSettings(string defaultSavePath, int listenPort, int globalMaxConnections)
            : this(defaultSavePath, listenPort, globalMaxConnections, DefaultMaxHalfOpenConnections,
                   DefaultMaxDownloadSpeed, DefaultMaxUploadSpeed, DefaultAllowedEncryption)
        {
        }

        public EngineSettings(string defaultSavePath, int listenPort, int globalMaxConnections, int globalHalfOpenConnections)
            : this(defaultSavePath, listenPort, globalMaxConnections, globalHalfOpenConnections,
                   DefaultMaxDownloadSpeed, DefaultMaxUploadSpeed, DefaultAllowedEncryption)
        {
        }

        public EngineSettings(string defaultSavePath, int listenPort, int globalMaxConnections, int globalHalfOpenConnections,
            int globalMaxDownloadSpeed, int globalMaxUploadSpeed, EncryptionTypes allowedEncryption)
        {
            GlobalMaxConnections = globalMaxConnections;
            GlobalMaxDownloadSpeed = globalMaxDownloadSpeed;
            GlobalMaxUploadSpeed = globalMaxUploadSpeed;
            GlobalMaxHalfOpenConnections = globalHalfOpenConnections;
            ListenPort = listenPort;
            AllowedEncryption = allowedEncryption;
            SavePath = defaultSavePath;
            ClientVersion = DefaultClientVersion;
        }
 
        #endregion


        #region Methods

        object ICloneable.Clone()
        {
            return Clone();
        }

        public EngineSettings Clone()
        {
            return (EngineSettings)MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var settings = obj as EngineSettings;

            return (settings == null) ? false :
                    GlobalMaxConnections == settings.GlobalMaxConnections &&
                    GlobalMaxDownloadSpeed == settings.GlobalMaxDownloadSpeed &&
                    GlobalMaxHalfOpenConnections == settings.GlobalMaxHalfOpenConnections &&
                    GlobalMaxUploadSpeed == settings.GlobalMaxUploadSpeed &&
                    ListenPort == settings.ListenPort &&
                    AllowedEncryption == settings.AllowedEncryption &&
                    SavePath == settings.SavePath;
        }

        public override int GetHashCode()
        {
            return GlobalMaxConnections +
                   GlobalMaxDownloadSpeed +
                   GlobalMaxHalfOpenConnections +
                   GlobalMaxUploadSpeed +
                   ListenPort.GetHashCode() +
                   AllowedEncryption.GetHashCode() +
                   SavePath.GetHashCode();
            
        }

        #endregion Methods
    }
}