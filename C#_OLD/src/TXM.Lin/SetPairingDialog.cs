using System;
using System.Collections.Generic;
using Gtk;

using TXM.Core;

namespace TXM.Lin
{
	public partial class SetPairingDialog : Gtk.Dialog
	{
		private List<Player> Players;
		private List<Player> PlayerWithoutPairing;
		public List<Pairing> PremadePairing;
		private List<string> tempList; 
		private string Nick1;
		private string Nick2;
		public bool OK = false;
		private int selectedPairing;

		public SetPairingDialog (List<Player> players, List<Pairing> prePaired)
		{
			this.Build ();

			Players = players;

			PlayerWithoutPairing = new List<Player>();

			PremadePairing = prePaired;

			ListboxPairings.AppendColumn ("Nr", new Gtk.CellRendererText (), "text", 0);
			ListboxPairings.AppendColumn ("Paarungen", new Gtk.CellRendererText (), "text", 1);

			if (PremadePairing == null)
				PremadePairing = new List<Pairing>();
			else
			{
				RefreshListBoxPairings ();
				foreach (Pairing p in PremadePairing)
				{
					players[players.IndexOf(p.Player1)].Paired = true;
					players[players.IndexOf(p.Player2)].Paired = true;
				}
			}

			foreach (Player player in Players)
			{
				if (!player.Paired)
					PlayerWithoutPairing.Add(player);
			}

			SetComboboxPlayer1();
			ComboboxPlayer2.AppendText("Spieler 2");
			ComboboxPlayer2.Active = 0;
		}

		protected void Player2_SelectionChanged (object sender, EventArgs e)
		{
			string value = (string)ComboboxPlayer2.ActiveText;
			if (value != "Spieler 2" && value != null)
			{
				Nick2 = value;
				ButtonAdd.Sensitive = true;
			}
			else
			{
				Nick2 = "";
				ButtonAdd.Sensitive = false;
			}
		}

		protected void LbPairing_Changed (object sender, EventArgs e)
		{
			TreeSelection sel = ((TreeView)sender).Selection;
			TreeModel model;
			TreeIter iter;
			if (sel.GetSelected (out model, out iter)) {
				selectedPairing = Int32.Parse(model.GetValue (iter, 0).ToString ());
			}
			buttonSub.Sensitive = true;
		}

		protected void Player1_SelectionChanged (object sender, EventArgs e)
		{
			string value = (string)ComboboxPlayer1.ActiveText;
			if (value != "Spieler 1" && value != null)
			{
				Nick1 = value;
				ComboboxPlayer2.Sensitive = true;
				SetComboboxPlayer2();
			}
			else
			{
				Nick1 = "";
				ComboboxPlayer2.Sensitive = false;
			}
		}

		protected void ButtonAdd_Click (object sender, EventArgs e)
		{
			Pairing p = new Pairing();
			for (int i = 0; i < PlayerWithoutPairing.Count; i++)
			{
				Player player = PlayerWithoutPairing[i];
				if (player.DisplayName == Nick1)
				{
					p.Player1 = player;
					player.Paired = true;
					PlayerWithoutPairing.Remove(player);
					i--;
				}
				else if (player.DisplayName == Nick2)
				{
					p.Player2 = player;
					player.Paired = true;
					PlayerWithoutPairing.Remove(player);
					i--;
				}
				if (p.Player1 != null && (p.Player2 != null || Nick2 == "Freilos"))
					break;
			}
			if (Nick2 == "Freilos")
			{
				p.Player1.Freeticket = true;
				p.Player2 = new Player("Freilos");
			}
			PremadePairing.Add(p);
			RefreshListBoxPairings ();
			SetComboboxPlayer1();
			ComboboxPlayer2.Sensitive = false;
			ComboboxPlayer2.Active = 0;
			ButtonAdd.Sensitive = false;
		}

		private void RefreshListBoxPairings()
		{
			int nr = 0;
			NodeStore ns = new NodeStore (typeof(PrePairedTreeNode));
			foreach (Pairing p in PremadePairing)
			{
				ns.AddNode (new PrePairedTreeNode (nr, p.Player1.DisplayName + " gegen " + p.Player2.DisplayName));
				nr++;
			}
			ListboxPairings.NodeStore = ns;
		}

		protected void ButtonOK_Click (object sender, EventArgs e)
		{
			OK = true;
			Pairing.ResetTableNr();
			foreach (var p in PremadePairing)
				p.GetTableNr();
		}

		protected void ButtonSub_Click (object sender, EventArgs e)
		{
			int at = selectedPairing;
			PlayerWithoutPairing.Add(PremadePairing[at].Player1);
			PlayerWithoutPairing.Add(PremadePairing[at].Player2);
			PremadePairing.RemoveAt(at);
			string value = (string)ComboboxPlayer1.ActiveText;
			RefreshListBoxPairings ();
			buttonSub.Sensitive = false;
		}

		private void SetComboboxPlayer1()
		{
			ComboboxPlayer1.Model = new ListStore (typeof(string));
			ComboboxPlayer1.AppendText("Spieler 1");
			tempList = new List<string>();
			foreach (Player player in PlayerWithoutPairing)
				tempList.Add(player.DisplayName);
			tempList.Sort();
			foreach (var s in tempList)
				ComboboxPlayer1.AppendText(s);
			ComboboxPlayer1.Active = 0;
		}

		private void SetComboboxPlayer2()
		{
			ComboboxPlayer2.Model = new ListStore (typeof(string));
			ComboboxPlayer2.AppendText("Spieler 2");
			ComboboxPlayer2.AppendText("Freilos");
			tempList = new List<string>();
			foreach (Player player in PlayerWithoutPairing)
			{
				if (player.DisplayName != Nick1)
					tempList.Add(player.DisplayName);
			}
			tempList.Sort();
			foreach (var s in tempList)
				ComboboxPlayer2.AppendText(s);
			ComboboxPlayer2.Active = 0;
		}
	}
}

