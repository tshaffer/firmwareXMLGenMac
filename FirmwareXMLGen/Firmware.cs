using System;

namespace FirmwareXMLGen
{
	public class Firmware
	{
		#region Computed Propoperties
		public string Family { get; set;} = "";
		public string Type { get; set;} = "";
		public string Version { get; set; } = "";
		public string BAVersion { get; set; } = "";
		public bool ForceDownload { get; set; } = false;
		public bool DontDownload { get; set; } = false;
		#endregion

		#region Constructors
		public Firmware ()
		{
		}

		public Firmware (string family, string type, string version, string baVersion, bool forceDownload, bool dontDownload)
		{
			this.Family = family;
			this.Type = type;
			this.Version = version;
			this.BAVersion = baVersion;
			this.ForceDownload = forceDownload;
			this.DontDownload = dontDownload;
		}
		#endregion
	}
}

