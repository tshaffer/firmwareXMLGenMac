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

		void HandleActivated (object sender, EventArgs e)
		{
			Console.WriteLine("Event Handled --->>"+ DateTime.Now.ToString());
//			int row = (int)(sender as NSButton).Tag;
		}

		#region Override Methods
		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			// This pattern allows you reuse existing views when they are no-longer in use.
			// If the returned view is null, you instance up a new view
			// If a non-null view is returned, you modify it enough to reflect the new data

			NSTextField view = null;

			if (tableColumn.DataCell is NSTextFieldCell) {
				view = (NSTextField)tableView.MakeView (CellIdentifier, this);
				if (view == null) {
					view = new NSTextField ();
					view.Identifier = CellIdentifier;
					view.BackgroundColor = NSColor.Clear;
					view.Bordered = false;
					view.Selectable = false;
					view.Editable = false;
				}
			}

			// Setup view based on the column selected
			switch (tableColumn.Title) {
			case "":
				NSButton deleteButtonView = (NSButton)tableView.MakeView (CellIdentifier, this);
				if (deleteButtonView == null) {
					deleteButtonView = new NSButton ();
					deleteButtonView.Identifier = CellIdentifier;
					deleteButtonView.Bordered = false;
					deleteButtonView.SetButtonType (NSButtonType.MomentaryPushIn);
				}
				deleteButtonView.Title = "x";
				deleteButtonView.Tag = row;

				EventHandler myEventHandler;
				myEventHandler = new EventHandler (HandleActivated);
				deleteButtonView.Activated += myEventHandler;

				return deleteButtonView;
			case "Family":
				view.StringValue = DataSource.Firmware [(int)row].Family;
				break;
			case "Type":
				view.StringValue = DataSource.Firmware [(int)row].Type;
				break;
			case "Version":
				view.StringValue = DataSource.Firmware [(int)row].Version;
				break;
			case "BA Version":
				view.StringValue = DataSource.Firmware [(int)row].BAVersion;
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
				buttonView.Title = "";
				buttonView.State = stateOn ? NSCellStateValue.On : NSCellStateValue.Off;
				return buttonView;
			case "Don't Download":
				bool dontDownloadOn = DataSource.Firmware [(int)row].DontDownload;
				NSButton dontDownloadButtonView = (NSButton)tableView.MakeView (CellIdentifier, this);
				if (dontDownloadButtonView == null) {
					dontDownloadButtonView = new NSButton ();
					dontDownloadButtonView.Identifier = CellIdentifier;
					dontDownloadButtonView.Bordered = false;
					dontDownloadButtonView.SetButtonType (NSButtonType.Switch);
				}
				dontDownloadButtonView.Title = "";
				dontDownloadButtonView.State = dontDownloadOn ? NSCellStateValue.On : NSCellStateValue.Off;
				return dontDownloadButtonView;
			}

			return view;
		}
		#endregion
	}
}
