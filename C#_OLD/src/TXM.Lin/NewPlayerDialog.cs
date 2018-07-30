using System;
using System.Collections.Generic;

using TXM.Core;

namespace TXM.Lin
{
	public partial class NewPlayerDialog : Gtk.Dialog
	{
		public bool DialogReturn { get; private set; }
		public bool Changes { get; private set; }
		public int SquadPoints { get; private set; }
		public Player CurrentPlayer { get; private set; }
		private List<string> Nicknames;
		private bool uniqueName = false;
		private bool nicknameRequiered = true;

		public NewPlayerDialog (List<string> nicknames, Player player = null)
		{
			this.Build ();

			Nicknames = nicknames;
			TextBoxNickname.HasFocus = true;
			Changes = false;
			//Noch steuern, welcher Spieler angelegt werden soll
			foreach (string s in Player.GetFactions())
				ComboBoxFaction.AppendText(s);
			if(player !=null)
			{
				CurrentPlayer = player;
				TextBoxForename.Text = CurrentPlayer.Forename;
				TextBoxName.Text = CurrentPlayer.Name;
				TextBoxNickname.Text = CurrentPlayer.Nickname;
				TextBoxTeam.Text = CurrentPlayer.Team;
				IntegerUpDownSquadPoints.Value = ((Player)CurrentPlayer).PointOfSquad;
				IntegerUpDownStartNr.Value = CurrentPlayer.Nr;
				if (Player.StringToFaction("Imperium") == ((Player)CurrentPlayer).PlayersFaction)
					ComboBoxFaction.Active = 0;
				else if (Player.StringToFaction("Rebellen") == ((Player)CurrentPlayer).PlayersFaction)
					ComboBoxFaction.Active = 1;
				else
					ComboBoxFaction.Active = 2;
				CheckboxFreeticket.Active = ((Player)CurrentPlayer).WonFreeticket;
				CheckboxPayed.Active = CurrentPlayer.Payed;
				CheckboxSquadListGiven.Active = CurrentPlayer.SquadListGiven;
				TextBoxNickname.Sensitive = false;
				uniqueName = true;
				labelWarning.Text = "";
				nicknameRequiered = false;
			}
		}

		public string ButtonOKContent {
			set {
				buttonOk.Label = value;

			}
		}

		protected void ValueChanged (object sender, EventArgs e)
		{
			Changes = true;
		}

		protected void IntegerUpDownSquadPoints_ValueChanged (object sender, EventArgs e)
		{
			SquadPoints = (int) IntegerUpDownSquadPoints.Value;
			Changes = true;
		}

		protected void NewPlayer_click (object sender, EventArgs e)
		{
			NewPlayer ();
		}

		private void NewPlayer()
		{
			if (TextBoxNickname.Text == "" && nicknameRequiered)
				MessageBox.Show("Der Nickname muss angegeben werden.");
			else if (ComboBoxFaction.Active == -1)
				MessageBox.Show("Die Fraktion muss ausgewählt sein.");
			//else if(!uniqueName)
			//	MessageBox.Show("Der Nickname muss einmalig sein.");
			else
			{
				DialogReturn = true;
			}
		}

		public string GetFaction()
		{
			return ComboBoxFaction.ActiveText;
		}

		public string GetNickName()
		{
			return TextBoxNickname.Text;                
		}

		public string GetName()
		{
			return TextBoxName.Text;
		}

		public string GetForename()
		{
			return TextBoxForename.Text;
		}

		public string GetTeam()
		{
			return TextBoxTeam.Text;
		}

		public int GetStartNr()
		{
			return (int)IntegerUpDownStartNr.Value;
		}

		public bool FreeTicket()
		{
			return CheckboxFreeticket.Active == true;
		}

		public bool Paid()
		{
			return CheckboxPayed.Active == true;
		}

		public bool SquadListGiven()
		{
			return CheckboxSquadListGiven.Active == true;
		}

		private void TextBoxNickname_TextChanged(object sender, EventArgs e)
		{
			Changes = true;
			if (!Nicknames.Contains (TextBoxNickname.Text)) {
				uniqueName = true;
				labelWarning.Visible = false;
			} else {
				uniqueName = false;
				labelWarning.Visible = true;
			}

		}
	}
}

