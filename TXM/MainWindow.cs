using System;
using Gtk;


using TXM.Core;

namespace TXM
{
	public partial class MainWindow: Gtk.Window
	{
		private Controller controller;
		private Language activeLanguage {get{ return controller.ActiveLanguage; }}

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			controller = new Controller (new MultiFile (), new MultiMessage ());

			//TODO: ermittelte Sprachen anzeigen (lokal installierte)

			SetLanguage ();
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

		//internal functions
		/// <summary>
		/// Translate the UI to the choosen language
		/// </summary>
		private void SetLanguage()
		{
			Menu30Minutes.Label = "30 " + activeLanguage.GetTranslation (StaticLanguage.Minutes);
			Menu45Minutes.Label = "45 " + activeLanguage.GetTranslation (StaticLanguage.Minutes);
			Menu60Minutes.Label = "60 " + activeLanguage.GetTranslation (StaticLanguage.Minutes);
			Menu75Minutes.Label = "75 " + activeLanguage.GetTranslation (StaticLanguage.Minutes);
			Menu90Minutes.Label = "90 " + activeLanguage.GetTranslation (StaticLanguage.Minutes);
			MenuAbout.Label = activeLanguage.GetTranslation (StaticLanguage.About);
			MenuAutosavefile.Label = activeLanguage.GetTranslation (StaticLanguage.LoadAutoSaveFile);
			MenuChangePairings.Label = activeLanguage.GetTranslation (StaticLanguage.ChangePairings);
			MenuDefaultLanguage.Label = activeLanguage.GetTranslation (StaticLanguage.DefaultLanguage);
			MenuEditPlayer.Label = activeLanguage.GetTranslation (StaticLanguage.EditPlayer);
			MenuExportBBCode.Label = activeLanguage.GetTranslation (StaticLanguage.ExportToBBCode);
			MenuExportT3.Label = activeLanguage.GetTranslation (StaticLanguage.T3Export);
			MenuFile.Label = activeLanguage.GetTranslation (StaticLanguage.File);
			MenuHelp.Label = activeLanguage.GetTranslation (StaticLanguage.Help);
			MenuImportExport.Label = activeLanguage.GetTranslation (StaticLanguage.ImportExport);
			MenuImportT3.Label = activeLanguage.GetTranslation (StaticLanguage.T3Import);
			MenuLanguage.Label = activeLanguage.GetTranslation (StaticLanguage.Language);
			MenuLoad.Label = activeLanguage.GetTranslation (StaticLanguage.Load);
			MenuLoadLanguages.Label = activeLanguage.GetTranslation (StaticLanguage.LoadLanguages);
			MenuLoadTournament.Label = activeLanguage.GetTranslation (StaticLanguage.LoadTournament);
			MenuNew.Label = activeLanguage.GetTranslation (StaticLanguage.New);
			MenuNewPlayer.Label = activeLanguage.GetTranslation (StaticLanguage.NewPlayer);
			MenuNewTournament.Label = activeLanguage.GetTranslation (StaticLanguage.NewTournament);
			MenuPauseTimer.Label = activeLanguage.GetTranslation (StaticLanguage.PauseTimer);
			MenuPrint.Label = activeLanguage.GetTranslation (StaticLanguage.Print);
			MenuPrintPairings.Label = activeLanguage.GetTranslation (StaticLanguage.PrintPairings);
			MenuPrintResults.Label = activeLanguage.GetTranslation (StaticLanguage.PrintResults);
			MenuPrintScoresheet.Label = activeLanguage.GetTranslation (StaticLanguage.PrintScoresheet);
			MenuPrintTable.Label = activeLanguage.GetTranslation (StaticLanguage.PrintTable);
			MenuQuit.Label = activeLanguage.GetTranslation (StaticLanguage.Quit);
			MenuRandomizer.Label = activeLanguage.GetTranslation (StaticLanguage.Randomizer);
			if (!controller.TournamentStarted)
				MenuRemovePlayer.Label = activeLanguage.GetTranslation (StaticLanguage.RemovePlayer);
			else
				MenuRemovePlayer.Label = activeLanguage.GetTranslation (StaticLanguage.DisqualifyPlayer);
			MenuResetLastResults.Label = activeLanguage.GetTranslation (StaticLanguage.ResetLastResults);
			MenuResetTimer.Label = activeLanguage.GetTranslation (StaticLanguage.ResetTimer);
			MenuSave.Label = activeLanguage.GetTranslation (StaticLanguage.Save);
			MenuSetTime.Label = activeLanguage.GetTranslation (StaticLanguage.SetTime);
			MenuShow.Label = activeLanguage.GetTranslation (StaticLanguage.ShowTablePairings);
			MenuShowTime.Label = activeLanguage.GetTranslation (StaticLanguage.TimeForTO);
			MenuStartTimer.Label = activeLanguage.GetTranslation (StaticLanguage.StartTimer);
			MenuTimer.Label = activeLanguage.GetTranslation (StaticLanguage.Timer);
			MenuTools.Label = activeLanguage.GetTranslation (StaticLanguage.Tools);
			MenuTournament.Label = activeLanguage.GetTranslation (StaticLanguage.Tournament);
			MenuTournamentSettings.Label = activeLanguage.GetTranslation (StaticLanguage.TournamentSettings);
			MenuVariable.Label = activeLanguage.GetTranslation (StaticLanguage.VariableMinutes);
			buttonChangePairings.Label = activeLanguage.GetTranslation (StaticLanguage.ChangePairings);
			buttonSave.Label = activeLanguage.GetTranslation (StaticLanguage.Save);
			buttonEditPlayer.Label = activeLanguage.GetTranslation (StaticLanguage.EditPlayer);
			buttonNewPlayer.Label = activeLanguage.GetTranslation (StaticLanguage.NewPlayer);
			buttonReset.Label = activeLanguage.GetTranslation (StaticLanguage.ResetLastResults);
			if (!controller.TournamentStarted)
				buttonTournament.Label = activeLanguage.GetTranslation (StaticLanguage.StartTournament);
			else {
				if (controller.LastRound)
					buttonTournament.Label = activeLanguage.GetTranslation (StaticLanguage.EndTournament);
				else
					buttonTournament.Label = activeLanguage.GetTranslation (StaticLanguage.GetResults);
			}

			//TODO: Translate Roundbuttons
			//TODO: Translate Table Headings
		}

		/// <summary>
		/// Click-Event for all language Actions
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void Language_Click(object sender, EventArgs e)
		{
			controller.LoadLanguage(((Gtk.Action)sender).Label, false);
			SetLanguage ();
		}
	}
}