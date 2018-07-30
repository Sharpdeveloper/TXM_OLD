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
	[Register ("WindowController")]
	partial class WindowController
	{
		[Outlet]
		AppKit.NSToolbarItem ComboBoxRounds { get; set; }

		[Action ("ButtonCut_Click:")]
		partial void ButtonCut_Click (Foundation.NSObject sender);

		[Action ("BUttonGetResults_Click:")]
		partial void BUttonGetResults_Click (Foundation.NSObject sender);

		[Action ("ButtonNextRound_Click:")]
		partial void ButtonNextRound_Click (Foundation.NSObject sender);

		[Action ("ChangePairings_Click:")]
		partial void ChangePairings_Click (Foundation.NSObject sender);

		[Action ("ComboBoxRounds_SelectionChanged:")]
		partial void ComboBoxRounds_SelectionChanged (Foundation.NSObject sender);

		[Action ("DisqualifyPlayer_Click:")]
		partial void DisqualifyPlayer_Click (Foundation.NSObject sender);

		[Action ("EditPlayer_Click:")]
		partial void EditPlayer_Click (Foundation.NSObject sender);

		[Action ("NewPlayer_Click:")]
		partial void NewPlayer_Click (Foundation.NSObject sender);

		[Action ("NewTimer_Click:")]
		partial void NewTimer_Click (Foundation.NSObject sender);

		[Action ("RemovePlayer_Click:")]
		partial void RemovePlayer_Click (Foundation.NSObject sender);

		[Action ("ResetLastResults_Click:")]
		partial void ResetLastResults_Click (Foundation.NSObject sender);

		[Action ("Save_Click:")]
		partial void Save_Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ComboBoxRounds != null) {
				ComboBoxRounds.Dispose ();
				ComboBoxRounds = null;
			}
		}
	}
}
