using System;

namespace TXM.Lin
{
	public partial class OutputDialog : Gtk.Window
	{
		public OutputDialog (string text) :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			textblockOutput.Buffer.Text = text;
		}

		protected void Button_Click_1 (object sender, EventArgs e)
		{
			Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
			clipboard.Text = textblockOutput.Buffer.Text;
		}

		protected void Button_Click (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

