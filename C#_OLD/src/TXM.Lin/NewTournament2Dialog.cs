using System;

using TXM.Core;

namespace TXM.Lin
{
	public partial class NewTournament2Dialog : Gtk.Dialog
	{
		public bool NewTournament { get; private set; }
		public bool Changes { get; private set; }
		private string[] tournamentTypesXwing = { "Dogfight (SWISS)" };

		public NewTournament2Dialog (Tournament2 tournament = null)
		{
			this.Build ();

			foreach (string s in tournamentTypesXwing)
			{
				ComboBoxTournamentType.AppendText (s);
			}
			ComboBoxTournamentType.Active = 0;
			Changes = false;

			if (tournament != null) {
				TextboxName.Text = tournament.Name;
				if (tournament.Cut == TournamentCut.Top4)
					RadioButtonTop4.Active = true;
				else if (tournament.Cut == TournamentCut.Top8)
					RadioButtonTop8.Active = true;
				else if (tournament.Cut == TournamentCut.Top16)
					RadioButtonTop16.Active = true;
				else
					RadioButtonNoCut.Active = true;
				IntegerUpDownMaxSquad.Value = tournament.MaxSquadPoints;
				ButtonOK.Label = "Speichern";
			} else
				IntegerUpDownMaxSquad.Value = 100;
		}

		protected void ComboBoxTournamentType_SelectionChanged (object sender, EventArgs e)
		{
			Changes = true;
		}

		protected void RadioButtonPressed (object sender, EventArgs e)
		{
			Changes = true;
		}

		protected void TextboxName_TextChanged (object sender, EventArgs e)
		{
			Changes = true;
		}

		protected void NewTournament_Click (object sender, EventArgs e)
		{
			if (ComboBoxTournamentType.ActiveText == null)
			{
				MessageBox.Show("Der Turniermodus muss ausgewählt werden.");
				NewTournament = false;
				return;
			}
			if (TextboxName.Text == "")
			{
				MessageBox.Show("Der Turniername muss ausgewählt werden.");
				NewTournament = false;
				return;
			}
			NewTournament = true;
		}

		protected void Cancel_Click (object sender, EventArgs e)
		{
			NewTournament = false;
			Changes = false;
		}

		public string GetTournamentMode()
		{
			return ComboBoxTournamentType.ActiveText;
		}

		public string GetName()
		{
			return TextboxName.Text;
		}

		public int GetMaxSquadSize()
		{
			return (int)IntegerUpDownMaxSquad.Value;
		}

		public TournamentCut GetCut()
		{
			if (RadioButtonTop4.Active == true)
				return TournamentCut.Top4;
			else if (RadioButtonTop8.Active == true)
				return TournamentCut.Top8;
			else if (RadioButtonTop16.Active == true)
				return TournamentCut.Top16;
			else
				return TournamentCut.NoCut;
		}
	}
}

