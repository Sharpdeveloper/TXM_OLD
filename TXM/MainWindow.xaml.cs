

     

        private void ButtonGetResults_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonGetResults.Content.ToString() == lang.GetTranslation(StaticLanguage.Update))
            {
                activeTournament.GetResults((List<Pairing>)DataGridPairing.ItemsSource, true);
                RefreshDataGridPlayer(activeTournament.Participants);
                io.ShowMessage("Update done!");
                ButtonGetResults.IsEnabled = activeTournament.ButtonGetResultState;
                ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
                ButtonNextRound.IsEnabled = activeTournament.ButtonNextRoundState;
                ButtonCut.IsEnabled = activeTournament.ButtonCutState;
                ListBoxRounds.SelectedIndex = ListBoxRounds.Items.Count - 1;
                RefreshDataGridPairings(activeTournament.Pairings);
            }
            else
                GetResults();
        }

        private void ButtonRound_Click(object sender, RoutedEventArgs e)
        {
            string header = ((ListBoxItem)sender).Content.ToString();
            header = header.Remove(0, header.IndexOf(" "));
            int round = Int32.Parse(header);
            RefreshDataGridPairings(activeTournament.Rounds[round - 1].Pairings);
            RefreshDataGridPlayer(activeTournament.Rounds[round - 1].Participants);
            if (activeTournament.Rounds.Count == round)
            {
                ButtonGetResults.IsEnabled = activeTournament.ButtonGetResultState;
                ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
                ButtonNextRound.IsEnabled = activeTournament.ButtonNextRoundState;
                ButtonCut.IsEnabled = activeTournament.ButtonCutState;
            }
            else
            {
                activeTournament.ButtonGetResultState = ButtonGetResults.IsEnabled;
                activeTournament.ButtonNextRoundState = ButtonNextRound.IsEnabled;
                activeTournament.ButtonCutState = ButtonCut.IsEnabled;
                ButtonGetResults.IsEnabled = true;
                ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.Update);
                ButtonNextRound.IsEnabled = false;
                ButtonCut.IsEnabled = false;
            }
            activeTournament.DisplayedRound = round;
        }

        private void ButtonAutosave_Click(object sender, RoutedEventArgs e)
        {
            if (io.AutosavePathExists)
                Load(true);
            else
                io.ShowMessage(lang.GetTranslation(StaticLanguage.NoAutoSaveFolder));
        }

        private void MenuItemShoAutoSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            io.ShowAutosaveFolder();
        }

        private void MenuItemLoadStatistics_Click(object sender, RoutedEventArgs e)
        {
            activeTournament.Statistics = io.LoadContents();
            io.LoadCSV(activeTournament);
            RefreshDataGridPlayer(activeTournament.Participants);
        }

        private void MenuItemTSettings_Click(object sender, RoutedEventArgs e)
        {
            NewTournamentDialog ntd = new NewTournamentDialog(lang, activeTournament);
            ntd.ShowDialog();
            if (ntd.Changes)
            {
                activeTournament.Name = ntd.GetName();
                activeTournament.Cut = ntd.GetCut();
                activeTournament.MaxSquadPoints = ntd.GetMaxSquadSize();
            }
        }

        private void ButtonCut_Click(object sender, RoutedEventArgs e)
        {
            GetSeed(true);
        }

        private void MenuItemDeleteAutosave_Click(object sender, RoutedEventArgs e)
        {
            io.DeleteAutosaveFolder();
        }

        private void MenuItem_Click_Table_Output(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                if (activeTournament.Rounds.Count == activeTournament.DisplayedRound)
                {
                    OutputDialog od = new OutputDialog(IO.TableForForum(activeTournament, activeTournament.Participants), lang);
                    od.Show();
                }
                else
                {
                    OutputDialog od = new OutputDialog(IO.TableForForum(activeTournament, activeTournament.Rounds[activeTournament.DisplayedRound - 1].Participants), lang);
                    od.Show();
                }
            }
        }

        private void MenuItem_Click_Pairing_Output(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                if (activeTournament.Rounds.Count == activeTournament.DisplayedRound)
                {
                    OutputDialog od = new OutputDialog(IO.PairingForForum(activeTournament, activeTournament.Pairings), lang);
                    od.Show();
                }
                else
                {
                    OutputDialog od = new OutputDialog(IO.PairingForForum(activeTournament, activeTournament.Rounds[activeTournament.DisplayedRound - 1].Pairings), lang);
                    od.Show();
                }
            }
        }

        private void MenuItem_Click_Results_Output(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                if (activeTournament.Rounds.Count - 2 >= 0)
                {
                    OutputDialog od = new OutputDialog(IO.PairingForForum(activeTournament, activeTournament.Rounds[activeTournament.Rounds.Count - 2].Pairings), lang);
                    od.Show();
                }
            }
        }

        private void RemovePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlayer.SelectedIndex >= 0)
            {
                Player player = ((List<Player>)DataGridPlayer.ItemsSource)[DataGridPlayer.SelectedIndex];
                if (io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.DoYouWant) + " " + player.DisplayName + " " + lang.GetTranslation(StaticLanguage.ToRemove) + "?"))
                {
                    activeTournament.RemovePlayer(player);
                }
                RefreshDataGridPlayer(activeTournament.Participants);
            }
        }

        private void ButtonChangePairing_Click(object sender, RoutedEventArgs e)
        {
            SetPairingDialog spd = new SetPairingDialog(activeTournament.Participants, activeTournament.Pairings, lang);
            spd.ShowDialog();
            if (spd.OK)
            {
                activeTournament.Pairings = spd.PremadePairing;
                RefreshDataGridPairings(activeTournament.Pairings);
                io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.ChangeSetPairings));
            }
        }

        private void MenuItemResetLastResults_Click(object sender, RoutedEventArgs e)
        {
            List<Pairing> pl = new List<Pairing>();
            foreach (var p in activeTournament.Rounds[activeTournament.Rounds.Count - 1].Pairings)
                pl.Add(new Pairing(p) { ResultEdited = true, }) ;
            activeTournament.RemoveLastRound();
            ChangeGUIState(true);
            RefreshDataGridPairings(pl);
            RefreshDataGridPlayer(activeTournament.Participants);
        }

        private void SetGUIState(bool start, bool tournamentStart = false)
        {
            if (start)
            {
                NewPlayerIsEnabled = true;
                MenuItemLoadStatistics.IsEnabled = true;
                MenuItemTSettings.IsEnabled = activeTournament != null;
                ButtonStart.IsEnabled = activeTournament != null;
                ButtonNewTournament.IsEnabled = true;
                ButtonGOEPPImport.IsEnabled = true;
                EditPlayerIsEnabled = activeTournament != null;
                RemovePlayerIsEnabled = activeTournament != null;
                ChangePairingIsEnabled = activeTournament.Pairings != null;
                SaveIsEnabled = activeTournament != null;
                ResetLastResultsIsEnabled = false;
                ButtonNextRound.IsEnabled = false;
                ButtonGetResults.IsEnabled = false;
                ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
                DisqualifyPlayerIsEnabled = false;
            }
            if (tournamentStart)
            {
                if (activeTournament.Rounds.Count > 1)
                    NewPlayerIsEnabled = false;
                else
                    NewPlayerIsEnabled = true;
                MenuItemLoadStatistics.IsEnabled = false;
                MenuItemTSettings.IsEnabled = true;
                ButtonStart.IsEnabled = false;
                ButtonGOEPPExport.IsEnabled = true;
                ButtonEndTournament.IsEnabled = true;
                EditPlayerIsEnabled = false;
                RemovePlayerIsEnabled = false;
                ChangePairingIsEnabled = true;
                SaveIsEnabled = true;
                ResetLastResultsIsEnabled = true;
                ButtonNextRound.IsEnabled = true;
                DisqualifyPlayerIsEnabled = true;
                ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
            }
        }

        private void ChangeGUIState(bool seed, bool end = false)
        {
            if (seed)
            {
                ButtonGetResults.IsEnabled = true;
                ButtonNextRound.IsEnabled = false;
                ButtonCut.IsEnabled = false;
                MenuItemResetLastResults.IsEnabled = false;
                DisqualifyPlayerIsEnabled = true;
            }
            else
            {
                ButtonGetResults.IsEnabled = false;
                MenuItemResetLastResults.IsEnabled = !end;
                ButtonNextRound.IsEnabled = !end;
                if (activeTournament.Cut == TournamentCut.NoCut || activeTournament.CutStarted)
                    ButtonCut.IsEnabled = false;
                else
                    ButtonCut.IsEnabled = true;
            }
        }

        public bool SaveIsEnabled
        {
            get
            {
                return ButtonSave.IsEnabled;
            }
            set
            {
                ButtonSave.IsEnabled = value;
                MenuItemSave.IsEnabled = value;
            }
        }

        public bool NewPlayerIsEnabled
        {
            get
            {
                return ButtonNewPlayer.IsEnabled;
            }
            set
            {
                ButtonNewPlayer.IsEnabled = value;
                MenuItemNewPlayer.IsEnabled = value;
            }
        }

        public bool EditPlayerIsEnabled
        {
            get
            {
                return ButtonEditPlayer.IsEnabled;
            }
            set
            {
                ButtonEditPlayer.IsEnabled = value;
                MenuItemEditPlayer.IsEnabled = value;
            }
        }

        public bool RemovePlayerIsEnabled
        {
            get
            {
                return ButtonRemovePlayer.IsEnabled;
            }
            set
            {
                ButtonRemovePlayer.IsEnabled = value;
                MenuItemRemovePlayer.IsEnabled = value;
            }
        }

        public bool DisqualifyPlayerIsEnabled
        {
            get
            {
                return ButtonDisqualifyPlayer.IsEnabled;
            }
            set
            {
                ButtonDisqualifyPlayer.IsEnabled = value;
                MenuItemDisqualifyPlayer.IsEnabled = value;
            }
        }

        public bool ChangePairingIsEnabled
        {
            get
            {
                return ButtonChangePairing.IsEnabled;
            }
            set
            {
                ButtonChangePairing.IsEnabled = value;
                MenuItemChangePairing.IsEnabled = value;
            }
        }

        public bool ResetLastResultsIsEnabled
        {
            get
            {
                return ButtonResetLastResults.IsEnabled;
            }
            set
            {
                ButtonResetLastResults.IsEnabled = value;
                MenuItemResetLastResults.IsEnabled = value;
            }
        }

        public bool TimerIsEnabled
        {
            get
            {
                return ButtonTimer.IsEnabled;
            }
            set
            {
                ButtonTimer.IsEnabled = value;
                MenuItemTimer.IsEnabled = value;
            }
        }

        public bool RandomIsEnabled
        {
            get
            {
                return ButtonRandom.IsEnabled;
            }
            set
            {
                ButtonRandom.IsEnabled = value;
                MenuItemRandom.IsEnabled = value;
            }
        }

        public bool StatisticIsEnabled
        {
            get
            {
                return MenuItemStatistic.IsEnabled;
            }
            set
            {
                //ButtonStatistic.IsEnabled = value;
                MenuItemStatistic.IsEnabled = value;
            }
        }

        private void DisqualifyPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlayer.SelectedIndex >= 0)
            {
                Player player = ((List<Player>)DataGridPlayer.ItemsSource)[DataGridPlayer.SelectedIndex];
                if (io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.ThePlayer) + " " + player.DisplayName + " " + lang.GetTranslation(StaticLanguage.GetsDisqualified) + ". " + lang.GetTranslation(StaticLanguage.NoUndo) + "." ))
                {
                    activeTournament.DisqualifyPlayer(player);
                    activeTournament.Sort();
                    RefreshDataGridPlayer(activeTournament.Participants);
                }
            }
        }

        private void MenuItemShowPairings_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                BeamerWindow bw = new Windows.BeamerWindow(DataGridPairing.ItemsSource, lang.GetTranslation(StaticLanguage.Pairings), false, lang);
                bw.Show();
            }
        }

        private void MenuItemShowTable_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                BeamerWindow bw = new Windows.BeamerWindow(DataGridPlayer.ItemsSource, lang.GetTranslation(StaticLanguage.Table), true, lang);
                bw.Show();
            }
        }

        private void MenuItemPrint_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                io.Print(activeTournament);
            }
        }

        private void MenuItemTimeStart_Click(object sender, RoutedEventArgs e)
        {
            if (tw == null)
            {
                io.ShowMessage(lang.GetTranslation(StaticLanguage.TimerMustBeStarted));
            }
            else
                tw.StartTimer();
        }

        private void MenuItemTimePause_Click(object sender, RoutedEventArgs e)
        {
            if (tw == null)
            {
                io.ShowMessage(lang.GetTranslation(StaticLanguage.TimerMustBeStarted));
            }
            else
            {
                tw.PauseTimer();
                if (tw.Started)
                    MenuItemTimePause.Header = lang.GetTranslation(StaticLanguage.PauseTime);
                else
                    MenuItemTimePause.Header = lang.GetTranslation(StaticLanguage.ResumeTime);
            }
        }

        private void MenuItemTimeReset_Click(object sender, RoutedEventArgs e)
        {
            if (tw == null)
            {
                io.ShowMessage(lang.GetTranslation(StaticLanguage.TimerMustBeStarted));
            }
            else
                tw.ResetTimer();
        }

        private void PrintTime()
        {
            MenuItemTime.Header = tw.ActualTime;
        }

        private void MenuItemPrintPairing_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                io.Print(activeTournament, false);
            }
        }

        private void MenuItemLoadLanguages_Click(object sender, RoutedEventArgs e)
        {
            LanguageDialog ld = new LanguageDialog(io);
            ld.ShowDialog();
            if(ld.DialogResult == true)
            {
                lang = io.LoadLanguage(ld.LanguageName, true);
                SetLanguage();
            }
        }

        private void MenuItemLanguageDefault_Click(object sender, RoutedEventArgs e)
        {
            lang = io.ResetLanguage();
            SetLanguage();
        }

        private void RefreshPlayerList(object sender, RoutedEventArgs e)
        {
            RefreshDataGridPlayer(activeTournament.Participants);
        }

        private void RefreshPairingsList(object sender, RoutedEventArgs e)
        {
            RefreshDataGridPairings(activeTournament.Pairings);
        }

        private void MenuItemPrintParingScore_Click(object sender, RoutedEventArgs e)
        {
            io.PrintScoreSheet(activeTournament);
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog ad = new AboutDialog();
            ad.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ad.ShowDialog();
        }

        private void DataGridPlayer_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //(List<Player>)Data.ItemsSource
            if(e.EditAction == DataGridEditAction.Commit)
            {
                Player player = activeTournament.Participants[DataGridPlayer.SelectedIndex];
                player.Present = !player.Present;
                //activeTournament.ChangePlayer(player);
            }
        }

        private void MenuItemPrintResult_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                io.Print(activeTournament, true);
            }
        }
    }
}
