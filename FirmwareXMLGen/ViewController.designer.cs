// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FirmwareXMLGen
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableColumn BAVersionColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn DontDownloadColumn { get; set; }

		[Outlet]
		AppKit.NSTextField existingFile { get; set; }

		[Outlet]
		AppKit.NSTableColumn FamilyColumn { get; set; }

		[Outlet]
		AppKit.NSTableView FirmwareTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn ForceDownloadColumn { get; set; }

		[Outlet]
		AppKit.NSTextField outputFile { get; set; }

		[Outlet]
		AppKit.NSTextFieldCell outputFolder { get; set; }

		[Outlet]
		AppKit.NSTableColumn TypeColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn VersionColumn { get; set; }

		[Action ("browseForExistingFile:")]
		partial void browseForExistingFile (Foundation.NSObject sender);

		[Action ("browseForOutputFolder:")]
		partial void browseForOutputFolder (Foundation.NSObject sender);

		[Action ("createFiles:")]
		partial void createFiles (Foundation.NSObject sender);

		[Action ("deleteFW:")]
		partial void deleteFW (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (existingFile != null) {
				existingFile.Dispose ();
				existingFile = null;
			}

			if (DontDownloadColumn != null) {
				DontDownloadColumn.Dispose ();
				DontDownloadColumn = null;
			}

			if (FamilyColumn != null) {
				FamilyColumn.Dispose ();
				FamilyColumn = null;
			}

			if (FirmwareTable != null) {
				FirmwareTable.Dispose ();
				FirmwareTable = null;
			}

			if (ForceDownloadColumn != null) {
				ForceDownloadColumn.Dispose ();
				ForceDownloadColumn = null;
			}

			if (outputFile != null) {
				outputFile.Dispose ();
				outputFile = null;
			}

			if (outputFolder != null) {
				outputFolder.Dispose ();
				outputFolder = null;
			}

			if (TypeColumn != null) {
				TypeColumn.Dispose ();
				TypeColumn = null;
			}

			if (VersionColumn != null) {
				VersionColumn.Dispose ();
				VersionColumn = null;
			}

			if (BAVersionColumn != null) {
				BAVersionColumn.Dispose ();
				BAVersionColumn = null;
			}
		}
	}
}
