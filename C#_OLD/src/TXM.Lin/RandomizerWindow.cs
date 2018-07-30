using System;

namespace TXM.Lin
{
	public partial class RandomizerWindow : Gtk.Window
	{
		private Random rnd;

		public RandomizerWindow () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			rnd = new Random();
		}

		protected void Close_Click (object sender, EventArgs e)
		{
			this.Destroy();
		}

		protected void Random_Click (object sender, EventArgs e)
		{
			labelRandom.Text = rnd.Next(1, ((int) IntegerUpDownPlayerCount.Value) + 1).ToString();
		}
	}
}

