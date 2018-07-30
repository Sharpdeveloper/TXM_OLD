using System;
using Gtk;

using TXM.Core;

namespace TXM.Lin
{
	public partial class StatisticsWindow : Gtk.Window
	{
		private Statistic stats;
		private IO io;

		public StatisticsWindow (IO _io) :
		base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			io = _io;
			stats = new Statistic();

			//nodeViews Vorbereiten
			DataGridPilots.AppendColumn ("Piloten", new Gtk.CellRendererText (), "text", 0);
			DataGridPilots.AppendColumn ("Anzahl", new Gtk.CellRendererText (), "text", 1);

			DataGridShips.AppendColumn ("Schiff", new Gtk.CellRendererText (), "text", 0);
			DataGridShips.AppendColumn ("Anzahl", new Gtk.CellRendererText (), "text", 1);

			DataGridUpgrades.AppendColumn ("Aufrüstung", new Gtk.CellRendererText (), "text", 0);
			DataGridUpgrades.AppendColumn ("Anzahl", new Gtk.CellRendererText (), "text", 1);
		}

		protected void Save_Click (object sender, EventArgs e)
		{
			io.SaveStatistic(stats, true);
		}

		protected void Load_Click (object sender, EventArgs e)
		{
			stats = io.LoadStatistic(true);
			if (stats != null)
				Refresh();
		}

		protected void CSV_Import_Click (object sender, EventArgs e)
		{
			stats = io.LoadContents();
			if (stats == null)
				return;
			io.LoadCSV(stats);
			Refresh();
			TextBoxURL.Text = "";
		}

		protected void CSV_Import_Overview_Click (object sender, EventArgs e)
		{
			stats = io.LoadContents(false, true);
			if (stats == null)
				return;
			io.LoadCSV(stats, true);
			Refresh();
			TextBoxURL.Text = "";
		}

		protected void Beenden(object sender, EventArgs e)
		{
			this.Destroy();		
		}

		protected void Update_Data_Click (object sender, EventArgs e)
		{
			io.LoadYASBFiles();
			io.LoadContents(true);
			stats.Reset();
			MessageBox.Show("Alle Daten geladen");
		}

		protected void ButtonPlus_Click (object sender, EventArgs e)
		{
			if (stats.Ships.Count == 0)
				stats = io.LoadContents();
			if (stats == null)
				return;
			if (TextBoxURL.Text.Contains(@"geordanr.github.io/xwing"))
			{
				stats.Parse(TextBoxURL.Text);
				Refresh();
				TextBoxURL.Text = "";
			}
			else
			{
				MessageBox.Show("Es sind nur YASB Links erlaubt.");
			}
		}

		protected void ButtonMinus_Click (object sender, EventArgs e)
		{
			if (stats.Ships.Count == 0)
				stats = io.LoadContents();
			if (stats == null)
				return;
			if (TextBoxURL.Text.Contains(@"geordanr.github.io/xwing"))
			{
				if (stats.IPilots.Count == 0)
				{
					MessageBox.Show("Es sind noch keine Daten da, deswegen kann auch nix rausgenommen werden)");
				}
				try
				{
					stats.Parse(TextBoxURL.Text, false);
				}
				catch (ArgumentException aex)
				{
					MessageBox.Show("Der Squad ist wurde noch nicht erfasst.");
				}
				Refresh();
				TextBoxURL.Text = "";
			}
			else
			{
				MessageBox.Show("Es sind nur YASB Links erlaubt.");
			}
		}

		private void Refresh()
		{
			NodeStore ns = new NodeStore (typeof(StatisticsTreeNode));
			foreach (Pilot p in stats.IPilots)
			{
				ns.AddNode (new StatisticsTreeNode (p.Count,p.Gername));
			}
			DataGridPilots.NodeStore = ns;

			ns = new NodeStore (typeof(StatisticsTreeNode));
			foreach (IUpgrade u in stats.IUpgrades)
			{
				ns.AddNode (new StatisticsTreeNode (u.Count,u.Gername));
			}
			DataGridShips.NodeStore = ns;

			ns = new NodeStore (typeof(StatisticsTreeNode));
			foreach (Ship s in stats.IShips)
			{
				ns.AddNode (new StatisticsTreeNode (s.Count,s.Gername));
			}
			DataGridUpgrades.NodeStore = ns;
		}

		private void CSV_Export_Click(object sender, EventArgs e)
		{
			io.Export(stats, true);
		}

		private void Forum_click(object sender, EventArgs e)
		{
			io.Export(stats, false);
		}
	}
}

