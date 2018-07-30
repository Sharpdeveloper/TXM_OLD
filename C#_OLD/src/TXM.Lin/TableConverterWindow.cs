using System;
using System.Collections.Generic;
using Gtk;

using TXM.Core;

namespace TXM.Lin
{
	public partial class TableConverterWindow : Gtk.Window
	{
		private string file, fileOut, separator;
		private IO io;

		public TableConverterWindow (IO _io) :
		base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			io = _io;
		}

		protected void ButtonConvert_Click (object sender, EventArgs e)
		{
			if(TextboxSeperator.Text == "")
			{
				io.ShowMessage("Es muss ein Trennzeichen angegeben sein");
			}
			else
			{
				io.ConvertCSV(TextboxSeperator.Text, file, fileOut);
			}
		}

		protected void Button_Click (object sender, EventArgs e)
		{
			file = io.LoadCSV();

			if (file != null) 
			{
				ButtonConvert.Sensitive = true;
				LabelInput.Text = file;
				fileOut = file.Remove(file.LastIndexOf(".")) + "_out.txt";
				LabelOutput.Text = fileOut;
			}
		}
	}
}

