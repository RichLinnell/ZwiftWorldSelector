// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ZwiftWorldSelector
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField FileMissing { get; set; }

		[Outlet]
		AppKit.NSButton Save { get; set; }

		[Outlet]
		AppKit.NSPopUpButton SelectedWorld { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Save != null) {
				Save.Dispose ();
				Save = null;
			}

			if (SelectedWorld != null) {
				SelectedWorld.Dispose ();
				SelectedWorld = null;
			}

			if (FileMissing != null) {
				FileMissing.Dispose ();
				FileMissing = null;
			}
		}
	}
}
