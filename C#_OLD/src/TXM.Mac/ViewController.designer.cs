// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TXM.Mac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableColumn NameColumn { get; set; }

		[Outlet]
		AppKit.NSTableView PlayerTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn RankColumn { get; set; }

		[Action ("ClickedButton:")]
		partial void ClickedButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PlayerTable != null) {
				PlayerTable.Dispose ();
				PlayerTable = null;
			}

			if (RankColumn != null) {
				RankColumn.Dispose ();
				RankColumn = null;
			}

			if (NameColumn != null) {
				NameColumn.Dispose ();
				NameColumn = null;
			}
		}
	}
}
