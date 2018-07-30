using System;
using Gtk;
using System.Reflection;
using System.Collections.Generic;

using TXM.Core;

namespace TXM.Lin
{
	public partial class MainWindow: Gtk.Window
	{
		private TimerWindow tw;
		private Tournament2 activeTournament;
		private bool started = false, firststart = false;
		private IO io;
		private int selectedPlayer = -1;
		private List<Pairing> activePairings;
		private List<Player> activeTable;

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build();

			io = new IO (new LinuxFile(),new LinuxMessage(), null);

			CellRendererText editableScore1 = new CellRendererText ();
			editableScore1.Edited += new EditedHandler (NumberCellEditedScore1);
			editableScore1.Editable = true;

			CellRendererText editableScore2 = new CellRendererText ();
			editableScore2.Edited += new EditedHandler (NumberCellEditedScore2);
			editableScore2.Editable = true;

			CellRendererText editableBool = new CellRendererText ();
			editableBool.Edited += new EditedHandler (BoolCellEdited);
			editableBool.Editable = true;

			dataGridPlayer.AppendColumn ("#", new Gtk.CellRendererText (), "text", 0);
			dataGridPlayer.AppendColumn ("Nr", new Gtk.CellRendererText (), "text", 1);
			dataGridPlayer.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 2);
			dataGridPlayer.AppendColumn ("Nickname", new Gtk.CellRendererText (), "text", 3);
			dataGridPlayer.AppendColumn ("GF", new Gtk.CellRendererText (), "text", 4);
			dataGridPlayer.AppendColumn ("€", new Gtk.CellRendererText (), "text", 5);
			dataGridPlayer.AppendColumn ("A", new Gtk.CellRendererText (), "text", 6);
			dataGridPlayer.AppendColumn ("Team", new Gtk.CellRendererText (), "text", 7);
			dataGridPlayer.AppendColumn ("Fraktion", new Gtk.CellRendererText (), "text", 8);
			dataGridPlayer.AppendColumn ("Squad", new Gtk.CellRendererText (), "text", 9);
			dataGridPlayer.AppendColumn ("Punkte", new Gtk.CellRendererText (), "text", 10);
			dataGridPlayer.AppendColumn ("S", new Gtk.CellRendererText (), "text", 11);
			dataGridPlayer.AppendColumn ("MS", new Gtk.CellRendererText (), "text", 12);
			dataGridPlayer.AppendColumn ("U", new Gtk.CellRendererText (), "text", 13);
			dataGridPlayer.AppendColumn ("N", new Gtk.CellRendererText (), "text", 14);
			dataGridPlayer.AppendColumn ("HdS", new Gtk.CellRendererText (), "text", 15);
			dataGridPlayer.AppendColumn ("GS", new Gtk.CellRendererText (), "text", 16);

			dataGridPlayer.ShowAll ();

			dataGridPairing.AppendColumn ("T-Nr.", new Gtk.CellRendererText (), "text", 0);
			dataGridPairing.AppendColumn ("Spieler 1", new Gtk.CellRendererText (), "text", 1);
			dataGridPairing.AppendColumn ("Spieler 2", new Gtk.CellRendererText (), "text", 2);
			dataGridPairing.AppendColumn ("Zerstört (S1)", editableScore1, "text", 3);
			dataGridPairing.AppendColumn ("Zerstört (S2)", editableScore2, "text", 4);
			dataGridPairing.AppendColumn ("OK?", editableBool, "text", 5);

			dataGridPairing.ShowAll ();
		}

		private void NumberCellEditedScore1 (object o, EditedArgs args)
		{
			TreePath path = new TreePath (args.Path);
			PairingTreeNode changed = (PairingTreeNode) dataGridPairing.NodeStore.GetNode (path);

			try {
				changed.Player1Score = Int32.Parse(args.NewText);
				activePairings[Int32.Parse(args.Path)] = changed.GetPairing;
			} catch (Exception e) {
				return;
			}
		}

		private void NumberCellEditedScore2 (object o, EditedArgs args)
		{
			TreePath path = new TreePath (args.Path);;
			PairingTreeNode changed = (PairingTreeNode) dataGridPairing.NodeStore.GetNode (path);

			try {
				changed.Player2Score = Int32.Parse(args.NewText);
				activePairings[Int32.Parse(args.Path)] = changed.GetPairing;
			} catch (Exception e) {
				return;
			}
		}

		private void BoolCellEdited (object o, EditedArgs args)
		{
			TreePath path = new TreePath (args.Path);
			PairingTreeNode changed = (PairingTreeNode) dataGridPairing.NodeStore.GetNode (path);

			try {
				changed.ResultEdited = args.NewText;
				activePairings[Int32.Parse(args.Path)] = changed.GetPairing;
			} catch (Exception e) {
				return;
			}
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit();
			a.RetVal = true;
		}

		private void Close_Click(object sender, EventArgs e)
		{
			Application.Quit();
		}

		private void ChangeGUIState(bool seed, bool end = false)
		{
			if (seed) {
				ButtonGetResults.Sensitive = true;
				ButtonNextRound.Sensitive = false;
				ButtonCut.Sensitive = false;
				MenuItemResetLastResults.Sensitive = false;
				DisqualifyPlayerIsEnabled = true;
			} else {
				ButtonGetResults.Sensitive = false;
				MenuItemResetLastResults.Sensitive = !end;
				ButtonNextRound.Sensitive = !end;
				if (activeTournament.Cut == TournamentCut.NoCut || activeTournament.CutStarted)
					ButtonCut.Sensitive = false;
				else
					ButtonCut.Sensitive = true;
			}
		}

		public void AddPlayer(Player player)
		{
			if (activeTournament != null) {
				activeTournament.AddPlayer (player);
				RefreshDataGridPlayer (activeTournament.Participants);
				RefreshDataGridPairings (activeTournament.Pairings);
			}
		}

		protected void NewTournament_Click (object sender, EventArgs e)
		{
			if (activeTournament != null)
			{
				if (!io.ShowMessageWithOKCancel("Das aktuelle Turnier wird überschrieben."))
					return;
			}
			NewTournament2Dialog ntd = new NewTournament2Dialog();
			ntd.Run ();

			if (ntd.NewTournament)
			{
				activeTournament = new Tournament2(ntd.GetName(), ntd.GetMaxSquadSize(), ntd.GetCut());
				SetGUIState(true);
				selectedPlayer = -1;
				SetIO(); 
			}
			ntd.Destroy ();
		}

		protected void Load_Click (object sender, EventArgs e)
		{
			Load();
		}

		private void Load(bool autosave = false)
		{
			int response;
			bool overwrite = true;
			string filename = "";
			for (int i = 3; i < vbuttonboxRounds.Children.Length; i++)
				vbuttonboxRounds.Remove (vbuttonboxRounds.Children [i]);
			if (autosave)
			{
				AutosaveDialog ad = new AutosaveDialog(io);
				ad.Run();
				overwrite = ad.Result;
				filename = ad.FileName;
				ad.Destroy ();
			}
			else
			{
				if (activeTournament != null)
				{
					if (!io.ShowMessageWithOKCancel("Das aktuelle Turnier wird überschrieben."))
						overwrite = false;
				}
			}
			if (overwrite == true)
			{
				activeTournament = io.Load(filename);
				//Load Actions
				if (activeTournament != null)
				{
					for (int i = 1; i <= activeTournament.Rounds.Count; i++)
						AddRoundButton(i);
				}
				if (activeTournament.FirstRound && (activeTournament.Rounds == null || activeTournament.Rounds.Count == 0))
				{
					SetGUIState(true);
				}
				else
				{
					SetGUIState(false, true);
				}
				ButtonGetResults.Label = "Erg. übernehmen";
				ButtonGetResults.Sensitive = activeTournament.ButtonGetResultState == true;
				ButtonNextRound.Sensitive = activeTournament.ButtonNextRoundState == true;
				ButtonCut.Sensitive = activeTournament.ButtonCutState == true;
				activeTournament.Sort();
				RefreshDataGridPlayer(activeTournament.Participants);
				if(activeTournament.Pairings != null)
					RefreshDataGridPairings(activeTournament.Pairings);
				selectedPlayer = -1;
				SetIO(); 
			}
		}

		private void SetIO()
		{
			//activeTournament.Io = new IO(new WindowsFile(), new WindowsMessage(), lang);
			activeTournament.Io = new IO(new LinuxFile(), new LinuxMessage(), null);
		} 

		private void RefreshDataGridPlayer(List<Player> players)
		{
			NodeStore ns = new NodeStore (typeof(PlayerTreeNode));
			activeTable = new List<Player> ();
			foreach (Player p in players) 
			{
				ns.AddNode (new PlayerTreeNode (p));
				activeTable.Add (p);
			}
			dataGridPlayer.NodeStore = ns;
		}

		private void RefreshDataGridPairings(List<Pairing> pairings)
		{
			NodeStore ns = new NodeStore (typeof(PairingTreeNode));
			activePairings = new List<Pairing> ();
			foreach (Pairing p in pairings) 
			{
				ns.AddNode (new PairingTreeNode (p));
				activePairings.Add (p);
			}
			dataGridPairing.NodeStore = ns;
		}

		private void AddRoundButton(int actRound = -1)
		{
			if (actRound == -1)
				actRound = activeTournament.Rounds.Count;
			Button b = new Button ();
			b.Label = "Runde " + actRound;
			b.Clicked += new global::System.EventHandler (this.ButtonRound_Click);
			vbuttonboxRounds.Add(b);
			vbuttonboxRounds.ShowAll ();
		}

		private void ButtonRound_Click(object sender, EventArgs e)
		{
			string header = ((Button)sender).Label;
			header = header.Remove(0, header.IndexOf(" "));
			int round = Int32.Parse(header);
			RefreshDataGridPairings(activeTournament.Rounds[round - 1].Pairings);
			RefreshDataGridPlayer(activeTournament.Rounds[round - 1].Participants);
			if (activeTournament.Rounds.Count == round)
			{
				ButtonGetResults.Sensitive = activeTournament.ButtonGetResultState;
				ButtonGetResults.Label = "Erg. übernehmen";
				ButtonNextRound.Sensitive = activeTournament.ButtonNextRoundState;
				ButtonCut.Sensitive = activeTournament.ButtonCutState;
			}
			else
			{
				activeTournament.ButtonGetResultState = ButtonGetResults.Sensitive;
				activeTournament.ButtonNextRoundState = ButtonNextRound.Sensitive;
				activeTournament.ButtonCutState = ButtonCut.Sensitive;
				ButtonGetResults.Sensitive = true;
				ButtonGetResults.Label = "Aktualisieren";
				ButtonNextRound.Sensitive = false;
				ButtonCut.Sensitive = false;
			}
			activeTournament.DisplayedRound = round;
		}

		protected void Save_Click (object sender, EventArgs e)
		{
			io.Save(activeTournament, false, ButtonGetResults.Sensitive, ButtonNextRound.Sensitive, ButtonCut.Sensitive);
		}

		protected void ButtonAutosave_Click (object sender, EventArgs e)
		{
			if (io.AutosavePathExists)
				Load(true);
			else
				io.ShowMessage("Es exisitiert kein Autosave Ordner.");
		}

		protected void MenuItemShoAutoSaveFolder_Click (object sender, EventArgs e)
		{
			io.ShowAutosaveFolder();
		}

		protected void MenuItemDeleteAutosave_Click (object sender, EventArgs e)
		{
			io.DeleteAutosaveFolder();
		}

		protected void GOEPPImport_Click (object sender, EventArgs e)
		{
			activeTournament = io.GOEPPImport();
			if (activeTournament != null)
			{
				SetGUIState(true);
				RefreshDataGridPlayer(activeTournament.Participants);
				SetIO(); 
			}
		}

		protected void GOEPPExport_Click (object sender, EventArgs e)
		{
			io.GOEPPExport(activeTournament);
		}

		protected void MenuItemResetLastResults_Click (object sender, EventArgs e)
		{
			activeTournament.RemoveLastRound();
			ChangeGUIState(true);
			RefreshDataGridPairings(activeTournament.Pairings);
			RefreshDataGridPlayer(activeTournament.Participants);
		}

		protected void MenuItemTSettings_Click (object sender, EventArgs e)
		{
			NewTournament2Dialog ntd = new NewTournament2Dialog(activeTournament);
			ntd.Run();
			if(ntd.Changes)
			{
				activeTournament.Name = ntd.GetName();
				activeTournament.Cut = ntd.GetCut();
				activeTournament.MaxSquadPoints = ntd.GetMaxSquadSize();
			}
			ntd.Destroy ();
		}

		protected void RemovePlayer_Click (object sender, EventArgs e)
		{
			if (selectedPlayer >= 0) {
				Player player = activeTournament.GetPlayerByNr (selectedPlayer);
				if (io.ShowMessageWithOKCancel ("Möchtest du " + player.DisplayName + " entfernen?")) {
					activeTournament.RemovePlayer (player);
					RefreshDataGridPlayer (activeTournament.Participants);
				}
			}
		}

		protected void DisqualifyPlayer_Click (object sender, EventArgs e)
		{
			if (selectedPlayer >= 0) {
				Player player = activeTournament.GetPlayerByNr (selectedPlayer);
				if (io.ShowMessageWithOKCancel("Der Spieler " + player.DisplayName + " soll disqualifiziert werden? Dies kann nicht Rückgängig gemacht werden!")) {
					activeTournament.DisqualifyPlayer (player);
					activeTournament.Sort ();
					RefreshDataGridPlayer (activeTournament.Participants);
					RefreshDataGridPairings (activeTournament.Pairings);
				}
			}
		}

		protected void DataGridPlayer_CursorChanged (object sender, EventArgs e)
		{
			TreeSelection sel = ((TreeView)sender).Selection;
			TreeModel model;
			TreeIter iter;
			if (sel.GetSelected (out model, out iter)) {
				selectedPlayer = Int32.Parse(model.GetValue (iter, 1).ToString ());
			}
		}


		protected void StartTournament_Click (object sender, EventArgs e)
		{
			if (activeTournament.Participants.Count != 0)
			{
				StartTournament();
				SetGUIState(false,true);
			}
			else
			{
				io.ShowMessage("Turnier kann ohne Teilnehmer nicht gestartet werden."); 
			}
		}

		protected void EndTournament_Click (object sender, EventArgs e)
		{
			GetResults(true);
			ButtonEndTournament.Sensitive = false;
		}

		protected void NewTimer_Click (object sender, EventArgs e)
		{
			tw = new TimerWindow(io);
			tw.Show();
			tw.Changed += tw_Changed;
		}

		private void tw_Changed(object sender, EventArgs e)
		{
			PrintTime ();
		}

		private void PrintTime()
		{
			MenuItemTime.Label = tw.ActualTime;
		}

		protected void Random_Click (object sender, EventArgs e)
		{
			RandomizerWindow rw = new RandomizerWindow();
			rw.Show();
		}

		protected void MenuItemTimeStart_Click (object sender, EventArgs e)
		{
			if (tw == null)
				io.ShowMessage("Es muss erst ein Timer gestartet werden (Turnierhilfen => X-Wing Timer).");
			else
				tw.StartTimer();
		}

		protected void MenuItemTimePause_Click (object sender, EventArgs e)
		{
			if (tw == null)
				io.ShowMessage ("Es muss erst ein Timer gestartet werden (Turnierhilfen => X-Wing Timer).");
			else
			{
				tw.PauseTimer ();
				MenuItemTimePause.Label = tw.Started ? "Zeit Pausieren" : "Zeit Fortsetzen";
			}
		}

		protected void MenuItemTimeReset_Click (object sender, EventArgs e)
		{
			if (tw == null)
				io.ShowMessage("Es muss erst ein Timer gestartet werden (Turnierhilfen => X-Wing Timer).");
			else
				tw.ResetTimer();
		}

		public void ChangePlayer()
		{
			if (selectedPlayer >= 0) {
				Player player = activeTournament.GetPlayerByNr (selectedPlayer);
				NewPlayerDialog npd = new NewPlayerDialog (activeTournament.Nicknames, player);
				npd.Title = "Spieler " + player.Nickname + " ändern";
				npd.ButtonOKContent = "Änderung übernehmen";
				npd.Run ();
				if (npd.DialogReturn && npd.Changes) {
					Player xwp = new Player (npd.GetNickName (), npd.SquadPoints, Player.StringToFaction (npd.GetFaction ()));
					xwp.Team = npd.GetTeam ();
					xwp.Name = npd.GetName ();
					xwp.Forename = npd.GetForename ();
					xwp.Nr = player.Nr;
					xwp.WonFreeticket = npd.FreeTicket ();
					xwp.SquadListGiven = npd.SquadListGiven ();
					xwp.Payed = npd.Paid ();
					activeTournament.ChangePlayer (xwp);
					RefreshDataGridPlayer (activeTournament.Participants);
				}
				npd.Destroy ();
			}
		}

		protected void NewPlayer_Click (object sender, EventArgs e)
		{
			NewPlayerDialog npd = new NewPlayerDialog(activeTournament.Nicknames);
			npd.Run();
			if (npd.DialogReturn)
			{
				Player xwp = new Player(npd.GetNickName(), npd.SquadPoints, Player.StringToFaction(npd.GetFaction()));
				xwp.Team = npd.GetTeam();
				xwp.Name = npd.GetName();
				xwp.Forename = npd.GetForename();
				xwp.WonFreeticket = npd.FreeTicket();
				xwp.SquadListGiven = npd.SquadListGiven();
				xwp.Payed = npd.Paid();
				AddPlayer(xwp);
			}
			npd.Destroy();
		}

		protected void RibbonButtonEditPlayer_Click (object sender, EventArgs e)
		{
			ChangePlayer();
		}

		protected void ButtonSetPairing_Click (object sender, EventArgs e)
		{
			Tournament2 t = activeTournament;
			SetPairingDialog spd = new SetPairingDialog(t.Participants, t.PrePaired);
			spd.Run();
			if (spd.OK)
			{
				t.PrePaired = spd.PremadePairing;
			}
			spd.Destroy ();
		}

		protected void ButtonChangePairing_Click (object sender, EventArgs e)
		{
			SetPairingDialog spd = new SetPairingDialog(activeTournament.Participants, activeTournament.Pairings);
			spd.Run();
			if (spd.OK)
			{
				activeTournament.Pairings = spd.PremadePairing;
				RefreshDataGridPairings(activeTournament.Pairings);
			}
			spd.Destroy ();
		}

		protected void MenuItemLoadStatistics_Click (object sender, EventArgs e)
		{
			activeTournament.Statistics = io.LoadContents();
			io.LoadCSV(activeTournament);
			RefreshDataGridPlayer(activeTournament.Participants);
		}

		protected void MenuItem_Click_Table_Output (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				activeTournament.Sort ();
				OutputDialog od = new OutputDialog (IO.TableForForum (activeTournament.Participants));
				od.Show ();
			}
		}

		protected void MenuItem_Click_Pairing_Output (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				OutputDialog od = new OutputDialog (IO.PairingForForum (activeTournament.Pairings));
				od.Show ();
			}
		}

		protected void MenuItem_Click_Results_Output (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				OutputDialog od = new OutputDialog (IO.PairingForForum (activeTournament.Rounds [activeTournament.Rounds.Count - 2].Pairings));
				od.Show ();	
			}
		}

		protected void Converter_Click (object sender, EventArgs e)
		{
			TableConverterWindow tc = new TableConverterWindow(io);
			tc.Show();
		}

		protected void Statistic_Click (object sender, EventArgs e)
		{
			StatisticsWindow mw = new StatisticsWindow(io);
			mw.Show();
		}

		public bool SaveIsEnabled
		{
			get
			{
				return MenuItemSave.IsSensitive;
			}
			set
			{
				ButtonSave.Sensitive = value;
				MenuItemSave.Sensitive = value;
			}
		}

		public bool NewPlayerIsEnabled
		{
			get
			{
				return MenuItemNewPlayer.IsSensitive;
			}
			set
			{
				ButtonNewPlayer.Sensitive = value;
				MenuItemNewPlayer.Sensitive = value;
			}
		}

		public bool EditPlayerIsEnabled
		{
			get
			{
				return MenuItemEditPlayer.IsSensitive;
			}
			set
			{
				ButtonEditPlayer.Sensitive = value;
				MenuItemEditPlayer.Sensitive = value;
			}
		}

		public bool RemovePlayerIsEnabled
		{
			get
			{
				return MenuItemRemovePlayer.IsSensitive;
			}
			set
			{
				ButtonRemovePlayer.Sensitive = value;
				MenuItemRemovePlayer.Sensitive = value;
			}
		}

		public bool DisqualifyPlayerIsEnabled
		{
			get
			{
				return MenuItemDisqualifyPlayer.IsSensitive;
			}
			set
			{
				ButtonDisqualifyPlayer.Sensitive = value;
				MenuItemDisqualifyPlayer.Sensitive = value;
			}
		}

		public bool SetPairingIsEnabled
		{
			get
			{
				return MenuItemSetPairing.IsSensitive;
			}
			set
			{
				ButtonSetPairing.Sensitive = value;
				MenuItemSetPairing.Sensitive = value;
			}
		}

		public bool ChangePairingIsEnabled
		{
			get
			{
				return MenuItemChangePairing.IsSensitive;
			}
			set
			{
				ButtonChangePairing.Sensitive = value;
				MenuItemChangePairing.Sensitive = value;
			}
		}

		public bool ResetLastResultsIsEnabled
		{
			get
			{
				return MenuItemResetLastResults.IsSensitive;
			}
			set
			{
				ButtonResetLastResults.Sensitive = value;
				MenuItemResetLastResults.Sensitive = value;
			}
		}

		public bool TimerIsEnabled
		{
			get
			{
				return MenuItemTimer.IsSensitive;
			}
			set
			{
				ButtonTimer.Sensitive = value;
				MenuItemTimer.Sensitive = value;
			}
		}

		public bool RandomIsEnabled
		{
			get
			{
				return MenuItemRandom.IsSensitive;
			}
			set
			{
				ButtonRandom.Sensitive = value;
				MenuItemRandom.Sensitive = value;
			}
		}

		public bool ConverterIsEnabled
		{
			get
			{
				return MenuItemConverter.IsSensitive;
			}
			set
			{
				ButtonConverter.Sensitive = value;
				MenuItemConverter.Sensitive = value;
			}
		}

		public bool StatisticIsEnabled
		{
			get
			{
				return MenuItemStatistic.IsSensitive;
			}
			set
			{
				ButtonStatistic.Sensitive = value;
				MenuItemStatistic.Sensitive = value;
			}
		}

		private void SetGUIState(bool start, bool tournamentStart = false)
		{
			if (start)
			{
				NewPlayerIsEnabled = true;
				MenuItemLoadStatistics.Sensitive = true;
				MenuItemTSettings.Sensitive = activeTournament != null;
				ButtonStart.Sensitive = activeTournament != null;
				ButtonNewTournament.Sensitive = true;
				ButtonGOEPPImport.Sensitive = true;
				EditPlayerIsEnabled = activeTournament != null;
				RemovePlayerIsEnabled = activeTournament != null;
				SetPairingIsEnabled = activeTournament != null;
				ChangePairingIsEnabled = activeTournament.Pairings != null;
				SaveIsEnabled = activeTournament != null;
				ResetLastResultsIsEnabled = false;
				ButtonNextRound.Sensitive = false;
				ButtonGetResults.Sensitive = false;
				ButtonGetResults.Label = "Erg. übernehmen";
				DisqualifyPlayerIsEnabled = false;
			}
			if(tournamentStart)
			{
				if (activeTournament.Rounds.Count > 1)
					NewPlayerIsEnabled = false;
				else
					NewPlayerIsEnabled = true;
				MenuItemLoadStatistics.Sensitive = false;
				MenuItemTSettings.Sensitive = true;
				ButtonStart.Sensitive = false;
				ButtonGOEPPExport.Sensitive = true;
				ButtonEndTournament.Sensitive = true;
				EditPlayerIsEnabled = false;
				RemovePlayerIsEnabled = false;
				SetPairingIsEnabled = true;
				ChangePairingIsEnabled = true;
				SaveIsEnabled = true;
				ResetLastResultsIsEnabled = true;
				ButtonNextRound.Sensitive = true;
				DisqualifyPlayerIsEnabled = true;
				ButtonGetResults.Label = "Erg. übernehmen";
			}
		}

		protected void ButtonNextRound_Click (object sender, EventArgs e)
		{
			GetSeed();
		}

		protected void ButtonCut_Click (object sender, EventArgs e)
		{
			GetSeed(true);
		}

		private bool CheckResults(List<Pairing> pairings)
		{
			foreach (Pairing p in pairings)
			{
				if (p.Player1Score != 0 && p.Player1Score < 12)
					return false;
				if (p.Player2Score != 0 && p.Player2Score < 12)
					return false;
			}
			return true;
		}

		public void StartTournament()
		{
			firststart = true;
			started = true;
			io.Save(activeTournament,true, ButtonGetResults.Sensitive, ButtonNextRound.Sensitive, ButtonCut.Sensitive, "Turnierstart");
		}

		public void GetSeed(bool cut = false)
		{
			List<Pairing> pairings = activeTournament.GetSeed(firststart, cut);
			RefreshDataGridPairings(pairings);
			AddRoundButton();
			ChangeGUIState(true);
			io.Save(activeTournament,true, ButtonGetResults.Sensitive, ButtonNextRound.Sensitive, ButtonCut.Sensitive, "Paarung_Runde" + activeTournament.Rounds.Count);
			firststart = false;
		}

		private void RefreshRanks()
		{
			for (int i = 1; i <= activeTournament.Participants.Count; i++)
				activeTournament.Participants[i - 1].Rank = i;
		}

		public void GetResults(bool end = false)
		{
			List<Pairing> pairings = activePairings;
			if (pairings.Count == 1)
				end = true;
			bool allResultsEdited = true;
			foreach (Pairing p in pairings)
			{
				if (!p.ResultEdited)
				{
					allResultsEdited = false;
					break;
				}
			}
			if (allResultsEdited)
			{
				if (CheckResults(pairings))
				{
					activeTournament.GetResults(pairings);
				}
				else
				{
					io.ShowMessage("Ergebnisse müssen 0 oder mindestens 12 sein.");
					return;
				}
			}
			else
			{
				io.ShowMessage("Es müssen alle Ergebnisse eingegeben werden.");
				return;
			}

			if (end)
			{
				if (!activeTournament.CutStarted)
				{
					activeTournament.CalculateWonFreeticket();
				}
				dataGridPairing.Visible = false;
				ChangeGUIState(false, true);
			}
			else
			{
				ChangeGUIState(false);
			}
			activeTournament.Sort();
			RefreshDataGridPlayer(activeTournament.Participants);
			io.Save(activeTournament,true, ButtonGetResults.Sensitive, ButtonNextRound.Sensitive, ButtonCut.Sensitive, "Ergebnisse_Runde" + activeTournament.Rounds.Count);
		}

		protected void ButtonGetResults_Click (object sender, EventArgs e)
		{
			if (ButtonGetResults.Label.ToString() == "Aktualisieren")
				activeTournament.GetResults(activePairings, true);
			else
				GetResults();
		}
		protected void MenuItemPrint_Click (object sender, EventArgs e)
		{
			if (activeTournament != null)
			{
				io.Print (activeTournament);
			}
		}

		protected void MenuItemPrintPairing_Click (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				io.Print(activeTournament, false); 
			}
		}

		protected void MenuItemPrintResult_Click (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				io.Print(activeTournament, true); 
			}
		}

		protected void MenuItemShowPairings_Click (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				BeamerWindow bw = new BeamerWindow (activePairings);
				bw.Show ();
			}
		}

		protected void MenuItemShowTable_Click (object sender, EventArgs e)
		{
			if (activeTournament != null) {
				BeamerWindow bw = new BeamerWindow (activeTable);
				bw.Show ();
			}
		}
	}
}

