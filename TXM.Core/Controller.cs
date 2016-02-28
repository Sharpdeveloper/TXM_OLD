using System;
using System.Collections.Generic;

namespace TXM.Core
{
    public class Controller
    {
        public Tournament ActiveTournament { get; private set; }

        public Timer ActiveTimer { get; private set; }

        public Language ActiveLanguage { get { return activeIO.ActiveLanguage; } }

        public bool TournamentStarted { get; private set; }

        public bool LastRound { get; set; }

        public List<Pairing> ActivePairings{ get; set; }

        public List<Player> ActiveTable{ get; set; }

        public string Title
        {
            get
            {
                if (ActiveTournament != null)
                    return ActiveTournament.Name + " - TXM";
                else
                    return "TXM";
            }
        }

        private IO activeIO;
        private Settings activeSettings;

        public Controller(IFile fileManager, IMessage messageManager)
        {
            activeIO = new IO(fileManager, messageManager);
            ActiveTimer = new Timer();
            //TODO: Einstellungen lesen
        }

        public void LoadLanguage(string languageName, bool download)
        {
            activeIO.LoadLanguage(languageName, download);
        }

        /// <summary>
        /// Creates a new Tournament
        /// </summary>
        /// <param name="iNewTournamentDialog">An Dialog which implents the INewTournamentDialog interface.</param>
        public void NewTournament(ITournamentDialog itd)
        {
            //Check if there is an existing Tournament
            if (ActiveTournament != null)
            {
                if (!activeIO.ShowMessageWithOKCancel(ActiveLanguage.GetTranslation(StaticLanguage.Overwrite)))
                    return;
            }

            //Set Language and show Dialog
            itd.DisplayedLanguage = ActiveLanguage;
            itd.NewTournament = true;
            itd.MaxPoints = 100;
            itd.ShowDialog();

            //if a new Tournament should be created, get the information from the Dialog
            if (itd.OK)
            {
                ActiveTournament = new Tournament(ActiveLanguage.GetTranslation(StaticLanguage.Imperium), ActiveLanguage.GetTranslation(StaticLanguage.Bye), ActiveLanguage.GetTranslation(StaticLanguage.WonBye))
                {
                    Name = itd.TournamentName,
                    MaxSquadPoints = itd.MaxPoints,
                    Cut = itd.Cut,
                    CutTo = itd.CutTo
                }; 
            }
        }

        public bool AddPlayer(IPlayerDialog ipd)
        {
            if (ActiveTournament == null)
            {
                ipd.Delete();

                activeIO.ShowMessage(ActiveLanguage.GetTranslation(StaticLanguage.StartTournamentFirst));
                return false;
            }
            if (ActiveTournament.ActiveRound != 0)
            {
                ipd.Delete();

                activeIO.ShowMessage(ActiveLanguage.GetTranslation(StaticLanguage.TournamentStarted));
                return false;
            }
            ipd.DisplayedLanguage = ActiveLanguage;
            ipd.NewPlayer = true;
            ipd.AddFaction(ActiveLanguage.GetTranslation(StaticLanguage.Imperium));
            ipd.AddFaction(ActiveLanguage.GetTranslation(StaticLanguage.Rebels));
            ipd.AddFaction(ActiveLanguage.GetTranslation(StaticLanguage.Scum));
            ipd.ShowDialog();
                
            if (ipd.OK)
            {
                var p = new Player();
                p.Name = ipd.LastName;
                p.Forename = ipd.ForeName;
                p.Faction = ipd.Faction;
                p.Paid = ipd.Paid;
                p.SquadListGiven = ipd.SquadlistGiven;
                p.WonBye = ipd.WonBye;
                p.Nickname = ipd.NickName;
                p.TableNr = ipd.TableNr; 
                p.Team = ipd.Team;
                p.Present = ipd.Present;

                ActiveTournament.AddPlayer(p);
                return true;
            }
            return false;
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

        public void GetSeed(bool cut = false)
        {
            //TODO: Seeding
            //List<Pairing> pairings = activeTournament.GetSeed(firststart, cut);
            //activeIO.Save(activeTournament, true, lang.GetTranslation(StaticLanguage.Pairings) + "_" + lang.GetTranslation(StaticLanguage.Round) + activeTournament.Rounds.Count);
        }

        public void GetResults()
        {
            //TODO: GetResults
//			List<Pairing> pairings = (List<Pairing>)DataGridPairing.ItemsSource;
//			if (pairings.Count == 1)
//				end = true;
//			bool allResultsEdited = true;
//			foreach (Pairing p in pairings)
//			{
//				if (!p.ResultEdited)
//				{
//					allResultsEdited = false;
//					break;
//				}
//			}
//			if (allResultsEdited)
//			{
//				if (CheckResults(pairings))
//				{
//					activeTournament.GetResults(pairings);
//				}
//				else
//				{
//					io.ShowMessage(lang.GetTranslation(StaticLanguage.InvalidResult));
//					return;
//				}
//			}
//			else
//			{
//				io.ShowMessage(lang.GetTranslation(StaticLanguage.MissingResult));
//				return;
//			}
//
//			if (end)
//			{
//				if (!activeTournament.CutStarted)
//				{
//					activeTournament.CalculateWonFreeticket();
//				}
//				DataGridPairing.Visibility = System.Windows.Visibility.Hidden;
//				ChangeGUIState(false, true);
//			}
//			else
//			{
//				ChangeGUIState(false);
//			}
//			activeTournament.Sort();
//			RefreshDataGridPlayer(activeTournament.Participants);
//			io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.Result) + "_" + lang.GetTranslation(StaticLanguage.Round) + activeTournament.Rounds.Count);

            //private bool CheckResults(List<Pairing> pairings)
//			{
//				foreach (Pairing p in pairings)
//				{
//					if (p.Player1Score != 0 && p.Player1Score < 12)
//						return false;
//					if (p.Player2Score != 0 && p.Player2Score < 12)
//						return false;
//				}
//				return true;
//			}

            //TODO Refresh Ranks
//			private void RefreshRanks()
//			{
//				for (int i = 1; i <= activeTournament.Participants.Count; i++)
//					activeTournament.Participants[i - 1].Rank = i;
//			}
        }

        public bool ImportT3()
        {
            if (ActiveTournament != null)
            {
                if (!activeIO.ShowMessageWithOKCancel(ActiveLanguage.GetTranslation(StaticLanguage.Overwrite)))
                    return false;
            }
            ActiveTournament = activeIO.GOEPPImport();
            return ActiveTournament != null;
        }

        public void ExportT3()
        {
            if (ActiveTournament == null)
                return;
            if (ActiveTournament.GOEPPVersion == null)
            {
                activeIO.ShowMessage(ActiveLanguage.GetTranslation(StaticLanguage.NoT3Tournament));
                return;
            }
            activeIO.GOEPPExport(ActiveTournament);
        }

        public void EditPlayer()
        {
            //TODO Edit Player
//			if (DataGridPlayer.SelectedIndex >= 0)
//			{
//				Player player = activeTournament.Participants[DataGridPlayer.SelectedIndex];
//				NewPlayerDialog npd = new NewPlayerDialog(activeTournament.Nicknames, lang, player);
//				npd.Title = lang.GetTranslation(StaticLanguage.Player) + " " + player.Nickname + " " + lang.GetTranslation(StaticLanguage.ToChange);
//				npd.ButtonOK.Content = lang.GetTranslation(StaticLanguage.AcceptChanges);
//				npd.ShowDialog();
//				if (npd.DialogReturn && npd.Changes)
//				{
//					Player xwp = new Player(npd.GetNickName(), npd.SquadPoints, Player.StringToFaction(npd.GetFaction()));
//					xwp.GetResults(player);
//					xwp.Team = npd.GetTeam();
//					xwp.Name = npd.GetName();
//					xwp.Forename = npd.GetForename();
//					xwp.Nr = player.Nr;
//					xwp.WonFreeticket = npd.FreeTicket();
//					xwp.SquadListGiven = npd.SquadListGiven();
//					xwp.Payed = npd.Paid();
//					xwp.TableNr = npd.TableNr;
//					xwp.Present = npd.Present();
//					activeTournament.ChangePlayer(xwp);
//					RefreshDataGridPlayer(activeTournament.Participants);
//				}
//			}
        }

        public void RefreshActiveTable()
        {
            ActiveTable = new List<Player>();
            if (ActiveTournament != null)
            {
                ActiveTournament.Sort();
                foreach (var p in ActiveTournament.Participants)
                    ActiveTable.Add(p);
            }
        }

        public void RefreshActivePairings()
        {
            ActivePairings = new List<Pairing>();
            if (ActiveTournament != null)
            {
                foreach (var p in ActiveTournament.Pairings)
                    ActivePairings.Add(p);
            }
        }
    }
}

