//
// VersionInfo.cs
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



using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonoTorrent.Common
{
    public static class VersionInfo
    {
        /// <summary>
        /// Protocol string for version 1.0 of Bittorrent Protocol
        /// </summary>
        public static readonly string ProtocolStringV100 = "BitTorrent protocol";

        public static readonly string DhtClientVersion = "MO06";

        internal static  Version Version;

        public static string CreateClientVersion()
        {
            return CreateClientVersion("MO", Assembly.GetExecutingAssembly());
        }

		public static string CreateClientVersion(string clientCode, ICustomAttributeProvider assembly)
		{
            if(clientCode.Length != 2)
                throw new ArgumentException("ClientCode needs to be a 2 character string", "clientCode");

		    var versionAttrs = assembly.GetCustomAttributes(typeof (AssemblyInformationalVersionAttribute), false);
            if(versionAttrs.Length == 0)
                throw new ArgumentException("Unable to find AssemblyInformationalVersionAttribute", "assembly");

		    var versionAttr = (AssemblyInformationalVersionAttribute) versionAttrs[0];

			Version = new Version(versionAttr.InformationalVersion);

            // Build Version String
		    var version = string.Format("{0}{1}{2}{3}",
                Math.Max(Version.Major, 0),
		        Math.Max(Version.Minor, 0),
		        Math.Max(Version.Build, 0),
		        Math.Max(Version.Revision, 0));

            version = version.Length > 4 ?
                version.Substring (0, 4) :
                version.PadRight (4, '0');


		    return string.Format("-{0}{1}-", clientCode, version);
		}
    }
}
