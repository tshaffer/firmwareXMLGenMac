//using System;
//
//namespace FirmwareXMLGen
//{
//	public class FirmwareTableDataSource
//	{
//		public FirmwareTableDataSource ()
//		{
//		}
//	}
//}
using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace FirmwareXMLGen
{
	public class FirmwareTableDataSource : NSTableViewDataSource
	{
		#region Public Variables
		public List<Firmware> Firmware = new List<Firmware>();
		#endregion

		#region Constructors
		public FirmwareTableDataSource ()
		{
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount (NSTableView tableView)
		{
			return Firmware.Count;
		}
		#endregion
	}
}
