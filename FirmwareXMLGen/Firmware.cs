using System;

namespace FirmwareXMLGen
{
	public class Firmware
	{
		#region Computed Propoperties
		public string Family { get; set;} = "";
		public string Type { get; set;} = "";
		public bool ForceDownload { get; set; } = false;
		#endregion

		#region Constructors
		public Firmware ()
		{
		}

		public Firmware (string family, string type, bool forceDownload)
		{
			this.Family = family;
			this.Type = type;
			this.ForceDownload = forceDownload;
		}
		#endregion
	}
}

