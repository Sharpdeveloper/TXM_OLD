using System;
using System.Collections.Generic;

using Gtk;

using TXM.Core;

namespace TXM.Lin
{
	public partial class BeamerWindow : Gtk.Window
	{
		public BeamerWindow (List<Player> data) :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			this.Title = "Tabelle";
			DataGridOutput.AppendColumn ("#", new Gtk.CellRendererText (), "text", 0);
			DataGridOutput.AppendColumn ("Nr", new Gtk.CellRendererText (), "text", 1);
			DataGridOutput.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 2);
			DataGridOutput.AppendColumn ("Nickname", new Gtk.CellRendererText (), "text", 3);
			DataGridOutput.AppendColumn ("GF", new Gtk.CellRendererText (), "text", 4);
			DataGridOutput.AppendColumn ("€", new Gtk.CellRendererText (), "text", 5);
			DataGridOutput.AppendColumn ("A", new Gtk.CellRendererText (), "text", 6);
			DataGridOutput.AppendColumn ("Team", new Gtk.CellRendererText (), "text", 7);
			DataGridOutput.AppendColumn ("Fraktion", new Gtk.CellRendererText (), "text", 8);
			DataGridOutput.AppendColumn ("Squad", new Gtk.CellRendererText (), "text", 9);
			DataGridOutput.AppendColumn ("Punkte", new Gtk.CellRendererText (), "text", 10);
			DataGridOutput.AppendColumn ("S", new Gtk.CellRendererText (), "text", 11);
			DataGridOutput.AppendColumn ("MS", new Gtk.CellRendererText (), "text", 12);
			DataGridOutput.AppendColumn ("U", new Gtk.CellRendererText (), "text", 13);
			DataGridOutput.AppendColumn ("N", new Gtk.CellRendererText (), "text", 14);
			DataGridOutput.AppendColumn ("HdS", new Gtk.CellRendererText (), "text", 15);
			DataGridOutput.AppendColumn ("GS", new Gtk.CellRendererText (), "text", 16);
			RefreshDataGridPlayer(data);
			DataGridOutput.ShowAll ();
		}

		public BeamerWindow (List<Pairing> data) :
		base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			this.Title = "Paarungen";
			DataGridOutput.AppendColumn ("T-Nr.", new Gtk.CellRendererText (), "text", 0);
			DataGridOutput.AppendColumn ("Spieler 1", new Gtk.CellRendererText (), "text", 1);
			DataGridOutput.AppendColumn ("Spieler 2", new Gtk.CellRendererText (), "text", 2);
			DataGridOutput.AppendColumn ("Zerstört (S1)", new Gtk.CellRendererText (), "text", 3);
			DataGridOutput.AppendColumn ("Zerstört (S2)", new Gtk.CellRendererText (), "text", 4);
			DataGridOutput.AppendColumn ("OK?", new Gtk.CellRendererText (), "text", 5);
			RefreshDataGridPairings(data);
			DataGridOutput.ShowAll ();
		}

		private void RefreshDataGridPlayer(List<Player> players)
		{
			NodeStore ns = new NodeStore (typeof(PlayerTreeNode));
			foreach (Player p in players) 
			{
				ns.AddNode (new PlayerTreeNode (p));
			}
			DataGridOutput.NodeStore = ns;
		}

		private void RefreshDataGridPairings(List<Pairing> pairings)
		{
			NodeStore ns = new NodeStore (typeof(PairingTreeNode));
			foreach (Pairing p in pairings) 
			{
				ns.AddNode (new PairingTreeNode (p));
			}
			DataGridOutput.NodeStore = ns;
		}
	}
}

