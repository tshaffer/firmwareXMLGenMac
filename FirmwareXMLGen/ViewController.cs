using System;

using AppKit;
using Foundation;
using System.Collections.ObjectModel;


namespace FirmwareXMLGen
{
	public partial class ViewController : NSViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view.
		}

		public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		// the following function is never called
		partial void deleteFW (Foundation.NSObject sender) {
		}

		partial void browseForExistingFile (Foundation.NSObject sender) {
			
			var dlg = NSOpenPanel.OpenPanel;
			dlg.CanChooseFiles = true;
			dlg.CanChooseDirectories = false;

			if (dlg.RunModal () == 1) {
				var url = dlg.Urls[0];
				if (url != null) {
					
					existingFile.StringValue = url.Path;
					string existingFilePath = url.Path;

					FirmwareXmlGenShared.FirmwareXmlGen.ClearFirmwareFiles();

					FirmwareXmlGenShared.FirmwareXmlGen.OpenExistingXML(existingFilePath);
					ObservableCollection<FirmwareXmlGenShared.FirmwareXmlGen.FirmwareFile> existingFWFiles = FirmwareXmlGenShared.FirmwareXmlGen.GetFirmwareFiles();

					// Create the Firmware Table Data Source and populate it
					var DataSource = new FirmwareTableDataSource ();

					foreach (FirmwareXmlGenShared.FirmwareXmlGen.FirmwareFile firmwareFile in existingFWFiles) {
						Firmware firmware = new Firmware(firmwareFile.Family, firmwareFile.Type, firmwareFile.Version, firmwareFile.BAVersion, firmwareFile.ForceDownload, firmwareFile.DontDownload);
						DataSource.Firmware.Add(firmware);
					}

					// Populate the Firmware Table
					FirmwareTable.DataSource = DataSource;
					FirmwareTable.Delegate = new FirmwareTableDelegate (DataSource);
				}
			}
		}

		partial void browseForOutputFolder (Foundation.NSObject sender) {

			var dlg = NSOpenPanel.OpenPanel;
			dlg.CanChooseFiles = false;
			dlg.CanChooseDirectories = true;

			if (dlg.RunModal () == 1) {
				var url = dlg.Urls[0];
				if (url != null) {
					outputFolder.StringValue = url.Path;
				}
			}
		}

		partial void createFiles (Foundation.NSObject sender) {


			string filePath = "/Users/tedshaffer/Documents/All Media/Videos/0arc.mp4";
			string foo = ProbeWrapper.GetProbeData(filePath);

			// update the firmwareFiles data structure based on what's in the table

			NSTableView fwTable = FirmwareTable;
			FirmwareXMLGen.FirmwareTableDataSource dataSource = (FirmwareXMLGen.FirmwareTableDataSource)fwTable.DataSource;

			int numRows = (int)fwTable.RowCount;
			for (int i = 0; i < numRows; i++) {

				// family
				string family = (fwTable.GetView(1, i, false) as NSTextField).StringValue;

				// type
				string type = (fwTable.GetView(2, i, false) as NSTextField).StringValue;

				// version
				string version = (fwTable.GetView(3, i, false) as NSTextField).StringValue;

				// ba version
				string baVersion = (fwTable.GetView(4, i, false) as NSTextField).StringValue;

				// force download
				bool forceDownload;
				NSButton forceDownloadButton = (fwTable.GetView(5, i, false) as NSButton);
				NSCellStateValue forceDownloadState = forceDownloadButton.State;
				if (forceDownloadState == NSCellStateValue.On) {
					forceDownload = true;
				}
				else {
					forceDownload = false;
				}

				// don't download
				bool dontDownload;
				NSButton dontDownloadButton = (fwTable.GetView(6, i, false) as NSButton);
				NSCellStateValue dontDownloadState = dontDownloadButton.State;
				if (dontDownloadState == NSCellStateValue.On) {
					dontDownload = true;
				}
				else {
					dontDownload = false;
				}

				Firmware firmware = dataSource.Firmware[i];
				firmware.Family = family;
				firmware.Type = type;
				firmware.Version = version;
				firmware.BAVersion = baVersion;
				firmware.ForceDownload = forceDownload;
				firmware.DontDownload = dontDownload;
			}

			FirmwareXmlGenShared.FirmwareXmlGen.Create(existingFile.StringValue, outputFolder.StringValue, outputFile.StringValue);
		}


		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			// Create the Firmware Table Data Source and populate it
			// TODO - just test code
			var DataSource = new FirmwareTableDataSource ();
			DataSource.Firmware.Add (new Firmware( "Tiger", "Production", "6.0.51", "", false, true ));
			DataSource.Firmware.Add (new Firmware( "Lynx", "Beta", "6.0.69", "", false, false ));
			DataSource.Firmware.Add (new Firmware( "Cheetah", "Minimum Compatible", "5.1.33", "4.1", true, true ));

			// Populate the Firmware Table
			FirmwareTable.DataSource = DataSource;
			FirmwareTable.Delegate = new FirmwareTableDelegate (DataSource);
		}
	}
}
