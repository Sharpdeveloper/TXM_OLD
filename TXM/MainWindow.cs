using System;
using Gtk;


using TXM.Core;

namespace TXM
{
	public partial class MainWindow: Gtk.Window
	{
		Controller controller;

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			controller = new Controller (new MultiFile (), new MultiMessage ());
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void NewTournament_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void NewPlayer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void LoadTournament_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void LoadAutosavefile_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void SaveTournament_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ImportT3_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ExportT3_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ExportBBCode_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void PrintTable_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void PrintPairings_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void PrintResults_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void PrintScoresheet_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void Quit_Click (object sender, EventArgs e)
		{
			Application.Quit ();
		}

		protected void ChangePairings_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ResetLastResults_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void TournamentSettings_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ChangePlayer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void RemovePlayer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void Show_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void Timer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void Randomizer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void LoadLanguage_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void DefaultLanguage_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void About_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void StartTimer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void PauseTimer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void ResetTimer_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void M30_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void M45_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void M60_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void M75_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void M90_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void Variable_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}
	}
}