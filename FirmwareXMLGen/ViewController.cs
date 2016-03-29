﻿using System;

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

//					DataSource.Firmware.Clear();

					// Create the Firmware Table Data Source and populate it
					var DataSource = new FirmwareTableDataSource ();

					foreach (FirmwareXmlGenShared.FirmwareXmlGen.FirmwareFile firmwareFile in existingFWFiles) {
						Firmware firmware = new Firmware(firmwareFile.Family, firmwareFile.Type, firmwareFile.Version, firmwareFile.BAVersion, false, false);
						DataSource.Firmware.Add(firmware);
					}
//					DataSource.Firmware.Add (new Firmware( "Monaco", "Beta", "5.1.69", "4.4", false, true ));

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
			FirmwareXmlGenShared.FirmwareXmlGen.Create(existingFile.StringValue, outputFolder.StringValue, outputFile.StringValue);
		}

//		private FirmwareTableDataSource DataSource = null;

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			// Create the Firmware Table Data Source and populate it
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
