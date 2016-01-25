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
			//TODO NewTournamentDialog anlegen
//			if (activeTournament != null)
//			{
//				if (!io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.Overwrite)))
//					return;
//			}
//			NewTournamentDialog ntd = new NewTournamentDialog(lang);
//			ntd.ShowDialog();
//			if (ntd.NewTournament)
//			{
//				activeTournament = new Tournament2(ntd.GetName(), ntd.GetMaxSquadSize(), ntd.GetCut());
//				SetGUIState(true);
//				RefreshDataGridPairings(activeTournament.Pairings);
//				RefreshDataGridPlayer(activeTournament.Participants);
//				SetIO();
//			}
		}

		protected void NewPlayer_Click (object sender, EventArgs e)
		{
			//TODO: NewPlayerDialog anlegen
//			NewPlayerDialog npd = new NewPlayerDialog(activeTournament.Nicknames, lang);
//			npd.ShowDialog();
//			if (npd.DialogReturn)
//			{
//				Player xwp = new Player(npd.GetNickName(), npd.SquadPoints, Player.StringToFaction(npd.GetFaction()));
//				xwp.Team = npd.GetTeam();
//				xwp.Name = npd.GetName();
//				xwp.Forename = npd.GetForename();
//				xwp.WonFreeticket = npd.FreeTicket();
//				xwp.SquadListGiven = npd.SquadListGiven();
//				xwp.Payed = npd.Paid();
//				xwp.TableNr = npd.TableNr;
//				xwp.Present = npd.Present();
//				AddPlayer(xwp);
				//TODO: AddPlayer in Controller einrichten
			//TODO: Ansicht Refreshen
//			}
		}

		protected void LoadTournament_Click (object sender, EventArgs e)
		{
			controller.Load ();
		}

		protected void LoadAutosavefile_Click (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void SaveTournament_Click (object sender, EventArgs e)
		{
			controller.Save ();
		}

		protected void ImportT3_Click (object sender, EventArgs e)
		{
			//TODO: T3 Import
//			activeTournament = io.GOEPPImport();
//			if (activeTournament != null)
//			{
//				SetGUIState(true);
//				DataGridPlayer.ItemsSource = activeTournament.Participants;
//				RefreshDataGridPlayer(activeTournament.Participants);
//				SetIO();
//			}
		}

		protected void ExportT3_Click (object sender, EventArgs e)
		{
			//TODO: T3 Export
			//io.GOEPPExport(activeTournament);
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

		/// <summary>
		/// Exists the application
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
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

		protected void EditPlayer_Click (object sender, EventArgs e)
		{
			//TODO Edit Player bauen
			//TODO Prüfen ob ein Player ausgewählt wurde
			//ChangePlayer();
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
			//TODO: TimerWindow bauen
			//TODO: Timer Klasse bauen
//			tw = new TimerWindow(io, lang);
//			tw.Show();
			//			tw.Changed += tw_Changed; => Dispatcher.Invoke(new Action(PrintTime)); Dispatcher für UI
		}

		protected void Randomizer_Click (object sender, EventArgs e)
		{
			//TODO: Randomizer bauen
//			RandomizerWindow rw = new RandomizerWindow(lang);
//			rw.Show();
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

		protected void TournamentProcess_Click (object sender, EventArgs e)
		{
			//TODO: im Controller je nach TurnierStatus reagieren. 
			// Start
			// Nächste Runde
			// ggf. Cut Starten
			// Ende
//Start
//			if (activeTournament.Participants.Count != 0)
//			{
//				StartTournament();
//				SetGUIState(false, true);
//			}
//			else
//			{
//				io.ShowMessage(lang.GetTranslation(StaticLanguage.NoPlayer));
//			}
//			firststart = true;
//			started = true;
//			io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.TournamentStart));
//


//Ende
//			GetResults(true);
//			ButtonEndTournament.IsEnabled = false;
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