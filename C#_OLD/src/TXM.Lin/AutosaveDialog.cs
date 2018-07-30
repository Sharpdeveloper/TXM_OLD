using System;
using System.IO;

using TXM.Core;

namespace TXM.Lin
{
	public partial class AutosaveDialog : Gtk.Dialog
	{
		public bool Result { get; private set;}
		public string FileName { get; private set;}

		public AutosaveDialog (IO io)
		{
			this.Build ();

			string[] filenames = io.GetAutosaveFiles();
			foreach(string s in filenames)
			{
				ListBoxFiles.AppendText (s);
			}
			ListBoxFiles.Active = 0;
			Result = false;
		}

		protected void ButtonOK_Clicked (object sender, EventArgs e)
		{
			FileName = ListBoxFiles.ActiveText;
			Result = true;
		}
	}
}

