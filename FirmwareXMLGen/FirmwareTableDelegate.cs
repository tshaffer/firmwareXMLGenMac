//using System;
//
//namespace FirmwareXMLGen
//{
//	public class FirmwareTableDelegate
//	{
//		public FirmwareTableDelegate ()
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
	public class FirmwareTableDelegate: NSTableViewDelegate
	{
		#region Constants 
		private const string CellIdentifier = "FWCell";
		#endregion

		#region Private Variables
		private FirmwareTableDataSource DataSource;
		#endregion

		#region Constructors
		public FirmwareTableDelegate (FirmwareTableDataSource datasource)
		{
			this.DataSource = datasource;
		}
		#endregion

		#region Override Methods
		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			// This pattern allows you reuse existing views when they are no-longer in use.
			// If the returned view is null, you instance up a new view
			// If a non-null view is returned, you modify it enough to reflect the new data
			NSTextField view = (NSTextField)tableView.MakeView (CellIdentifier, this);
			if (view == null) {
				view = new NSTextField ();
				view.Identifier = CellIdentifier;
				view.BackgroundColor = NSColor.Clear;
				view.Bordered = false;
				view.Selectable = false;
				view.Editable = false;
			}

			// Setup view based on the column selected
			switch (tableColumn.Title) {
			case "Family":
				view.StringValue = DataSource.Firmware [(int)row].Family;
				break;
			case "Type":
				view.StringValue = DataSource.Firmware [(int)row].Type;
				break;
			case "Force Download":
				bool stateOn = DataSource.Firmware [(int)row].ForceDownload;
				NSButton buttonView = (NSButton)tableView.MakeView (CellIdentifier, this);
				if (buttonView == null) {
					buttonView = new NSButton ();
					buttonView.Identifier = CellIdentifier;
					buttonView.Bordered = false;
					buttonView.SetButtonType (NSButtonType.Switch);
				}
				buttonView.State = NSCellStateValue.On;
				return buttonView;
//				break;
			}

			return view;
		}
		#endregion
	}
}
