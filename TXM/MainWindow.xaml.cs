using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TXM.GUI.Dialogs;
using TXM.GUI.Windows;
using TXM.Core;

using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace TXM.GUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tournament2 activeTournament;
        private ICollectionView dataView;
        private bool started = false, firststart = false;
        private TimerWindow tw;
        private IO io;
        private Language lang;

        public MainWindow()
        {
            InitializeComponent();

            io = new IO(new WindowsFile(), new WindowsMessage());
            lang = io.GetCurrentLanguage();
            CheckLanguages();

            SetLanguage();
        }

        private void CheckLanguages()
        {
            List<string> languages = io.GetLanguages();
            foreach (var s in languages)
            {
                MenuItemLanguage.Items.Add(new MenuItem() { Header = s });
                ((MenuItem)MenuItemLanguage.Items[MenuItemLanguage.Items.Count - 1]).Click += ChangeLanguage;
            }
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            lang = io.LoadLanguage(((MenuItem)sender).Header.ToString(), false);

            SetLanguage();
        }

        private void SetLanguage()
        {
            MenuItemFile.Header = lang.GetTranslation(StaticLanguage.HeaderFile);
            ButtonNewTournament.Header = lang.GetTranslation(StaticLanguage.NewTournament);
            ButtonLoad.Header = lang.GetTranslation(StaticLanguage.Load);
            MenuItemAutosave.Header = lang.GetTranslation(StaticLanguage.AutoSaveFiles);
            ButtonAutosave.Header = lang.GetTranslation(StaticLanguage.LoadAutoSaveFile);
            MenuItemShowAutoSaveFolder.Header = lang.GetTranslation(StaticLanguage.ShowAutoSaveFolder);
            MenuItemDeleteAutosaveFolder.Header = lang.GetTranslation(StaticLanguage.DeleteAutoSaveFolder);
            MenuItemImportExport.Header = lang.GetTranslation(StaticLanguage.GOEPPImportExport);
            ButtonGOEPPImport.Header = lang.GetTranslation(StaticLanguage.GOEPPImport);
            ButtonGOEPPExport.Header = lang.GetTranslation(StaticLanguage.GOEPPExport);
            MenuItemPrintHeader.Header = lang.GetTranslation(StaticLanguage.Print);
            MenuItemPrint.Header = lang.GetTranslation(StaticLanguage.PrintTable);
            MenuItemPrintPairing.Header = lang.GetTranslation(StaticLanguage.PrintPairing);
            MenuItemPrintResult.Header = lang.GetTranslation(StaticLanguage.PrintResults);
            MenuItemExit.Header = lang.GetTranslation(StaticLanguage.Exit);
            MenuItemTournament.Header = lang.GetTranslation(StaticLanguage.HeaderTournament);
            MenuItemTSettings.Header = lang.GetTranslation(StaticLanguage.EditTournamentSettings);
            ButtonStart.Header = lang.GetTranslation(StaticLanguage.StartTournament);
            ButtonEndTournament.Header = lang.GetTranslation(StaticLanguage.EndTournament);
            MenuItemPlayerManagement.Header = lang.GetTranslation(StaticLanguage.HeaderPlayerManagement);
            MenuItemnOldPlayer.Header = lang.GetTranslation(StaticLanguage.AddExisitngPlayer);
            MenuItemLoadStatistics.Header = lang.GetTranslation(StaticLanguage.LoadStatistics);
            MenuItemView.Header = lang.GetTranslation(StaticLanguage.HeaderView);
            MenuItemShowPairings.Header = lang.GetTranslation(StaticLanguage.ShowPairings);
            MenuItemShowTable.Header = lang.GetTranslation(StaticLanguage.ShowTable);
            MenuItemOutputForForum.Header = lang.GetTranslation(StaticLanguage.OutputForForums);
            MenuItemTableForOutput.Header = lang.GetTranslation(StaticLanguage.TableOutput);
            MenuItemPairingForOutput.Header = lang.GetTranslation(StaticLanguage.PairingOutput);
            MenuItemResultForOutput.Header = lang.GetTranslation(StaticLanguage.ResultOutput);
            MenuItemTools.Header = lang.GetTranslation(StaticLanguage.HeaderTools);
            MenuItemHelp.Header = lang.GetTranslation(StaticLanguage.HeaderHelp);
            MenuItemUserHelp.Header = lang.GetTranslation(StaticLanguage.UserHelp);
            MenuItemAbout.Header = lang.GetTranslation(StaticLanguage.About);
            MenuItemTime.Header = lang.GetTranslation(StaticLanguage.HeaderTime);
            MenuItemTimeStart.Header = lang.GetTranslation(StaticLanguage.StartTime);
            MenuItemTimePause.Header = lang.GetTranslation(StaticLanguage.PauseTime);
            MenuItemTimeReset.Header = lang.GetTranslation(StaticLanguage.EndTime);
            MenuItemLanguage.Header = lang.GetTranslation(StaticLanguage.Language);
            MenuItemLoadLanguages.Header = lang.GetTranslation(StaticLanguage.LoadLanguages);
            MenuItemLanguageDefault.Header = StaticLanguage.DefaultLanguage;

            this.Title = lang.GetTranslation(StaticLanguage.Title);

            LabelRounds.Content = lang.GetTranslation(StaticLanguage.Rounds);

            ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
            ButtonNextRound.Content = lang.GetTranslation(StaticLanguage.NextRound);
            ButtonCut.Content = lang.GetTranslation(StaticLanguage.StartCut);

            SaveText = lang.GetTranslation(StaticLanguage.Save);
            NewPlayerText = lang.GetTranslation(StaticLanguage.AddNewPlayer);
            EditPlayerText = lang.GetTranslation(StaticLanguage.EditPlayer);
            RemovePlayerText = lang.GetTranslation(StaticLanguage.RemovePlayer);
            DisqualifyPlayerText = lang.GetTranslation(StaticLanguage.DisqualifyPlayer);
            ChangePairingText = lang.GetTranslation(StaticLanguage.ChangeSetPairings);
            RestLastResultsText = lang.GetTranslation(StaticLanguage.ResetLastResults);
            TimerText = lang.GetTranslation(StaticLanguage.Timer);
            RandomText = lang.GetTranslation(StaticLanguage.Randomizer);
            StatisticText = lang.GetTranslation(StaticLanguage.Statistic);

            DataGridPlayer.CanUserSortColumns = false;
            DataGridPlayer.SelectionMode = DataGridSelectionMode.Single;

            DataGridPlayer.Columns.Clear();
            DataGridPairing.Columns.Clear();

            DataGridTextColumn dgc;
            DataGridCheckBoxColumn dgcb;

            dgc = new DataGridTextColumn();
            dgc.Header = "#";
            dgc.Binding = new Binding("Rank");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.No);
            dgc.Binding = new Binding("Nr");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Forename);
            dgc.Binding = new Binding("Forename");
            dgc.IsReadOnly = true; 
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Nickname);
            dgc.Binding = new Binding("Nickname");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgcb = new DataGridCheckBoxColumn();
            dgcb.Header = "!";
            dgcb.Binding = new Binding("Present");
            dgcb.IsReadOnly = false;
            DataGridPlayer.Columns.Add(dgcb);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Team);
            dgc.Binding = new Binding("Team");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Faction);
            dgc.Binding = new Binding("PlayersFactionAsString");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.ListPointsShort);
            dgc.Binding = new Binding("PointOfSquad");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Points);
            dgc.Binding = new Binding("Points");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.WinsShort);
            dgc.Binding = new Binding("Wins");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.ModifiedWinsShort);
            dgc.Binding = new Binding("ModifiedWins");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.DrawShort);
            dgc.Binding = new Binding("Draws");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.LoosesShort);
            dgc.Binding = new Binding("Looses");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.MarginOfVictoryShort);
            dgc.Binding = new Binding("MarginOfVictory");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.StrengthOfSceduleShort);
            dgc.Binding = new Binding("PointsOfEnemies");
            dgc.IsReadOnly = true;
            DataGridPlayer.Columns.Add(dgc);
    
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.TableNr);
            dgc.Binding = new Binding("TableNr");
            dgc.IsReadOnly = true;
            DataGridPairing.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Player) + " 1";
            dgc.Binding = new Binding("Player1Name");
            dgc.IsReadOnly = true;
            DataGridPairing.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Player) + " 2";
            dgc.Binding = new Binding("Player2Name");
            dgc.IsReadOnly = true;
            DataGridPairing.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Destroyed) + " (" + lang.GetTranslation(StaticLanguage.Player) + " 1)";
            dgc.Binding = new Binding("Player1Score");
            dgc.IsReadOnly = false;
            DataGridPairing.Columns.Add(dgc);
            dgc = new DataGridTextColumn();
            dgc.Header = lang.GetTranslation(StaticLanguage.Destroyed) + " (" + lang.GetTranslation(StaticLanguage.Player) + " 2)";
            dgc.Binding = new Binding("Player2Score");
            dgc.IsReadOnly = false;
            DataGridPairing.Columns.Add(dgc);
            dgcb = new DataGridCheckBoxColumn();
            dgcb.Header = lang.GetTranslation(StaticLanguage.OK) + "?";
            Binding b = new Binding("ResultEdited");
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            dgcb.Binding = b;
            DataGridPairing.Columns.Add(dgcb);
        }

        private string SaveText
        {
            set
            {
                ButtonSave.ToolTip = value;
                MenuItemSave.Header = value;
            }
        }

        private string NewPlayerText
        {
            set
            {
                ButtonNewPlayer.ToolTip = value;
                MenuItemNewPlayer.Header = value;
            }
        }

        private string EditPlayerText
        {
            set
            {
                ButtonEditPlayer.ToolTip = value;
                MenuItemEditPlayer.Header = value;
            }
        }

        private string RemovePlayerText
        {
            set
            {
                ButtonRemovePlayer.ToolTip = value;
                MenuItemRemovePlayer.Header = value;
            }
        }

        private string DisqualifyPlayerText
        {
            set
            {
                ButtonDisqualifyPlayer.ToolTip = value;
                MenuItemDisqualifyPlayer.Header = value;
            }
        }

        private string ChangePairingText
        {
            set
            {
                ButtonChangePairing.ToolTip = value;
                MenuItemChangePairing.Header = value;
            }
        }

        private string RestLastResultsText
        {
            set
            {
                ButtonResetLastResults.ToolTip = value;
                MenuItemResetLastResults.Header = value;
            }
        }

        private string TimerText
        {
            set
            {
                ButtonTimer.ToolTip = value;
                MenuItemTimer.Header = value;
            }
        }

        private string RandomText
        {
            set
            {
                ButtonRandom.ToolTip = value;
                MenuItemRandom.Header = value;
            }
        }

        private string StatisticText
        {
            set
            {
                //ButtonStatistic.ToolTip = value;
                MenuItemStatistic.Header = value;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerDialog npd = new NewPlayerDialog(activeTournament.Nicknames, lang);
            npd.ShowDialog();
            if (npd.DialogReturn)
            {
                Player xwp = new Player(npd.GetNickName(), npd.SquadPoints, Player.StringToFaction(npd.GetFaction()));
                xwp.Team = npd.GetTeam();
                xwp.Name = npd.GetName();
                xwp.Forename = npd.GetForename();
                xwp.WonFreeticket = npd.FreeTicket();
                xwp.SquadListGiven = npd.SquadListGiven();
                xwp.Payed = npd.Paid();
                xwp.TableNr = npd.TableNr;
                xwp.Present = npd.Present();
                AddPlayer(xwp);
            }
            e.Handled = true;
        }

        private void NewTournament_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament != null)
            {
                if (!io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.Overwrite)))
                    return;
            }
            NewTournamentDialog ntd = new NewTournamentDialog(lang);
            ntd.ShowDialog();
            if (ntd.NewTournament)
            {
                activeTournament = new Tournament2(ntd.GetName(), ntd.GetMaxSquadSize(), ntd.GetCut());
                SetGUIState(true);
                RefreshDataGridPairings(activeTournament.Pairings);
                RefreshDataGridPlayer(activeTournament.Participants);
                SetIO();
            }
        }

        private void NewTimer_Click(object sender, RoutedEventArgs e)
        {
            tw = new TimerWindow(io, lang);
            tw.Show();
            tw.Changed += tw_Changed;
        }

        private void tw_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(PrintTime));
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            RandomizerWindow rw = new RandomizerWindow(lang);
            rw.Show();
        }

        private void StartTournament_Click(object sender, RoutedEventArgs e)
        {
            if (activeTournament.Participants.Count != 0)
            {
                StartTournament();
                SetGUIState(false, true);
            }
            else
            {
                io.ShowMessage(lang.GetTranslation(StaticLanguage.NoPlayer));
            }
        }

        private void RibbonButtonEditPlayer_Click(object sender, RoutedEventArgs e)
        {
            ChangePlayer();
            e.Handled = true;
        }

        private void GOEPPImport_Click(object sender, RoutedEventArgs e)
        {
            activeTournament = io.GOEPPImport();
            if (activeTournament != null)
            {
                SetGUIState(true);
                DataGridPlayer.ItemsSource = activeTournament.Participants;
                RefreshDataGridPlayer(activeTournament.Participants);
                SetIO();
            }
        }

        private void GOEPPExport_Click(object sender, RoutedEventArgs e)
        {
            io.GOEPPExport(activeTournament);
        }

        private void EndTournament_Click(object sender, RoutedEventArgs e)
        {
            GetResults(true);
            ButtonEndTournament.IsEnabled = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            io.Save(activeTournament, false, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load(bool autosave = false)
        {
            Nullable<bool> overwrite = true;
            string filename = "";
            ListBoxRounds.Items.Clear();
            if (autosave)
            {
                AutosaveDialog ad = new AutosaveDialog(io, lang);
                ad.ShowDialog();
                overwrite = ad.DialogReturn;
                filename = ad.FileName;
            }
            else
            {
                if (activeTournament != null)
                {
                    if (!io.ShowMessageWithOKCancel(lang.GetTranslation(StaticLanguage.Overwrite)))
                        overwrite = false;
                }
            }
            if (overwrite == true)
            {
                activeTournament = io.Load(filename);
                //Load Actions
                if (activeTournament != null)
                {
                    if (activeTournament.Rounds != null)
                    {
                        for (int i = 1; i <= activeTournament.Rounds.Count; i++)
                            AddRoundButton(i);
                    }
                    if (activeTournament.Rounds != null && activeTournament.Rounds.Count > 0)
                        DataGridPairing.ItemsSource = activeTournament.Rounds[activeTournament.Rounds.Count - 1].Pairings;
                    if (activeTournament.FirstRound && (activeTournament.Rounds == null || activeTournament.Rounds.Count == 0))
                    {
                        SetGUIState(true);
                    }
                    else
                    {
                        SetGUIState(false, true);
                    }
                    ButtonGetResults.Content = lang.GetTranslation(StaticLanguage.GetResults);
                    ButtonGetResults.IsEnabled = activeTournament.ButtonGetResultState == true;
                    ButtonNextRound.IsEnabled = activeTournament.ButtonNextRoundState == true;
                    ButtonCut.IsEnabled = activeTournament.ButtonCutState == true;
                    activeTournament.Sort();
                    RefreshDataGridPlayer(activeTournament.Participants);
                    if (activeTournament.Pairings != null)
                        RefreshDataGridPairings(activeTournament.Pairings);
                    SetIO();
                }
            }
        }

        private void SetIO()
        {
            activeTournament.Io = new IO(new WindowsFile(), new WindowsMessage());
        }

        private void Statistic_Click(object sender, RoutedEventArgs e)
        {
            bool english = lang.GetTranslation(StaticLanguage.LanguageName) != "Deutsch";
            TXM.GUI.Windows.StatisticsWindow mw = new TXM.GUI.Windows.StatisticsWindow(io, lang, english);
            mw.Show();
        }

        public void AddPlayer(Player player)
        {
            activeTournament.AddPlayer(player);
            RefreshDataGridPlayer(activeTournament.Participants);
            RefreshDataGridPairings(activeTournament.Pairings);
        }

        public void StartTournament()
        {
            firststart = true;
            started = true;
            io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.TournamentStart));
        }

        public void GetSeed(bool cut = false)
        {
            List<Pairing> pairings = activeTournament.GetSeed(firststart, cut);
            RefreshDataGridPairings(pairings);
            AddRoundButton();
            ChangeGUIState(true);
            io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.Pairings) + "_" + lang.GetTranslation(StaticLanguage.Round) + activeTournament.Rounds.Count);
            firststart = false;
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

        public void GetResults(bool end = false)
        {
            List<Pairing> pairings = (List<Pairing>)DataGridPairing.ItemsSource;
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
                    io.ShowMessage(lang.GetTranslation(StaticLanguage.InvalidResult));
                    return;
                }
            }
            else
            {
                io.ShowMessage(lang.GetTranslation(StaticLanguage.MissingResult));
                return;
            }

            if (end)
            {
                if (!activeTournament.CutStarted)
                {
                    activeTournament.CalculateWonFreeticket();
                }
                DataGridPairing.Visibility = System.Windows.Visibility.Hidden;
                ChangeGUIState(false, true);
            }
            else
            {
                ChangeGUIState(false);
            }
            activeTournament.Sort();
            RefreshDataGridPlayer(activeTournament.Participants);
            io.Save(activeTournament, true, ButtonGetResults.IsEnabled, ButtonNextRound.IsEnabled, ButtonCut.IsEnabled, lang.GetTranslation(StaticLanguage.Result) + "_" + lang.GetTranslation(StaticLanguage.Round) + activeTournament.Rounds.Count);
        }

        private void PariringCurrentCellChanged(object sender, EventArgs e)
        {
            DataGridPairing.CommitEdit();
        }

        private void RefreshDataGridPlayer(List<Player> players)
        {
            DataGridPlayer.ItemsSource = null;
            DataGridPlayer.ItemsSource = players;
            dataView = CollectionViewSource.GetDefaultView(DataGridPlayer.ItemsSource);
            dataView.SortDescriptions.Clear();

            activeTournament.Sort();

            dataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("Points", System.ComponentModel.ListSortDirection.Descending));
            dataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("MarginOfVictory", System.ComponentModel.ListSortDirection.Descending));
            dataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("PointsOfEnemies", System.ComponentModel.ListSortDirection.Descending));
            dataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("Nr", System.ComponentModel.ListSortDirection.Ascending));
            dataView.Refresh();
            RefreshRanks();
        }

        private void RefreshDataGridPairings(List<Pairing> pairings)
        {
            DataGridPairing.ItemsSource = null;
            DataGridPairing.ItemsSource = pairings;
        }

        private void DataGridPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPlayer.SelectedItem != null && !started)
            {
                RemovePlayerIsEnabled = true;
                EditPlayerIsEnabled = true;
            }
            else
            {
                RemovePlayerIsEnabled = false;
                EditPlayerIsEnabled = false;
            }
        }

        public void ChangePlayer()
        {
            if (DataGridPlayer.SelectedIndex >= 0)
            {
                Player player = activeTournament.Participants[DataGridPlayer.SelectedIndex];
                NewPlayerDialog npd = new NewPlayerDialog(activeTournament.Nicknames, lang, player);
                npd.Title = lang.GetTranslation(StaticLanguage.Player) + " " + player.Nickname + " " + lang.GetTranslation(StaticLanguage.ToChange);
                npd.ButtonOK.Content = lang.GetTranslation(StaticLanguage.AcceptChanges);
                npd.ShowDialog();
                if (npd.DialogReturn && npd.Changes)
                {
                    Player xwp = new Player(npd.GetNickName(), npd.SquadPoints, Player.StringToFaction(npd.GetFaction()));
                    xwp.GetResults(player);
                    xwp.Team = npd.GetTeam();
                    xwp.Name = npd.GetName();
                    xwp.Forename = npd.GetForename();
                    xwp.Nr = player.Nr;
                    xwp.WonFreeticket = npd.FreeTicket();
                    xwp.SquadListGiven = npd.SquadListGiven();
                    xwp.Payed = npd.Paid();
                    xwp.TableNr = npd.TableNr;
                    xwp.Present = npd.Present();
                    activeTournament.ChangePlayer(xwp);
                    RefreshDataGridPlayer(activeTournament.Participants);
                }
            }
        }

        private void RefreshRanks()
        {
            for (int i = 1; i <= activeTournament.Participants.Count; i++)
                activeTournament.Participants[i - 1].Rank = i;
        }

        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

        private void GridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as DataGrid;

            if (grid == null)
            {
                return;
            }

            // Assume first column is the checkbox column.
            if (grid.CurrentColumn == grid.Columns[5])
            {
                var gridCheckBox = (grid.CurrentColumn.GetCellContent(grid.SelectedItem) as CheckBox);

                if (gridCheckBox != null)
                {
                    gridCheckBox.IsChecked = !gridCheckBox.IsChecked;
                }
            }
        }

        private void DataGridPlayer_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            ChangePlayer();
        }

        private void ButtonNextRound_Click(object sender, RoutedEventArgs e)
        {
            GetSeed();
        }

        private void AddRoundButton(int actRound = -1)
        {
            ListBoxItem newListItem = new ListBoxItem();
            if (actRound == -1)
                actRound = activeTournament.Rounds.Count;
            newListItem.Content = lang.GetTranslation(StaticLanguage.Round) + " " + actRound;
            newListItem.MouseUp += ButtonRound_Click;
            ListBoxRounds.Items.Add(newListItem);
        }

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
