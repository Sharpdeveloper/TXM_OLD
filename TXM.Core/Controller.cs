using System;

namespace TXM.Core
{
	public class Controller
	{
		public Tournament ActiveTournament { get; private set;}
		public Timer ActiveTimer { get; private set;}
		public Language ActiveLanguage {get{ return activeIO.ActiveLanguage; }}
		public bool TournamentStarted { get; private set;}
		public bool LastRound { get; set;}

		private IO activeIO;
		private Settings activeSettings;

		public Controller (IFile fileManager, IMessage messageManager)
		{
			activeIO = new IO (fileManager, messageManager);
			ActiveTimer = new Timer ();
			//TODO: Einstellungen lesen
		}

		public void LoadLanguage(string languageName, bool download)
		{
			activeIO.LoadLanguage(languageName, download);
		}

		/// <summary>
		/// Save the tournament
		/// </summary>
		public void Save()
		{
			activeIO.Save(ActiveTournament, false);
		}

		public void Load(bool autosave = false)
		{
			//TODO: Load implementieren
//			Nullable<bool> overwrite = true;
//			string filename = "";
//			ListBoxRounds.Items.Clear();
//			if (autosave)
//			{
//				AutosaveDialog ad = new AutosaveDialog(io, lang);
//				ad.ShowDialog();
//				overwrite = ad.DialogReturn;
//				filename = ad.FileName;
//			}
//			else
//			{
//				if (activeTournament != null)
//				{
//					if (!io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.Overwrite)))
//						overwrite = false;
//				}
//			}
//			if (overwrite == true)
//			{
//				activeTournament = io.Load(filename);
//				//Load Actions
//				if (activeTournament != null)
//				{
//					if (activeTournament.Rounds != null)
//					{
//						for (int i = 1; i <= activeTournament.Rounds.Count; i++)
//							AddRoundButton(i);
//					}
//					if (activeTournament.Rounds != null && activeTournament.Rounds.Count > 0)
//						DataGridPairing.ItemsSource = activeTournament.Rounds[activeTournament.Rounds.Count - 1].Pairings;
//					if (activeTournament.FirstRound && (activeTournament.Rounds == null || activeTournament.Rounds.Count == 0))
//					{
//						SetGUIState(true);
//					}
//					else
//					{
//						SetGUIState(false, true);
//					}
//					ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
//					ButtonGetResults.IsEnabled = activeTournament.ButtonGetResultState == true;
//					ButtonNextRound.IsEnabled = activeTournament.ButtonNextRoundState == true;
//					ButtonCut.IsEnabled = activeTournament.ButtonCutState == true;
//					activeTournament.Sort();
//					RefreshDataGridPlayer(activeTournament.Participants);
//					if (activeTournament.Pairings != null)
//						RefreshDataGridPairings(activeTournament.Pairings);
//					SetIO();
//				}
//			}
		}

	}
}

