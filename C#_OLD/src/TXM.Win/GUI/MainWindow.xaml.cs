using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using TXM.GUI.Dialogs;
using TXM.GUI.Windows;
using TXM.Core;

namespace TXM.GUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollectionView dataView;
        private TournamentController tournamentController;
        private int refresh = 0;
        private List<Pairing> currentPairingList;
        private bool hide = false;

        public MainWindow()
        {
            InitializeComponent();

            Closing += WindowClosed;

            tournamentController = new TournamentController(new IO(new WindowsFile(), new WindowsMessage()));

            if (tournamentController.ActiveIO.GetColor())
                SliderText.Value = 1.0;
            else
                SliderText.Value = 2.0;

            double size = tournamentController.ActiveIO.GetSize();
            if (size > 100.0)
                SliderSize.Value = 100.0;
            else
                SliderSize.Value = size;

            tournamentController.ActiveTimer.Changed += Time_Changed;

            DataGridPlayer.SelectionMode = DataGridSelectionMode.Extended;
            DataGridPairing.SelectionMode = DataGridSelectionMode.Single;

            InitDataGridPlayer();
            InitDataGridPairing();
        }

        private void InitDataGridPlayer()
        {
            DataGridPlayer.Columns.Clear();

            DataGridTextColumn dgc;
            DataGridCheckBoxColumn dgcb;

            dgc = new DataGridTextColumn()
            {
                Header = "#",
                Binding = new Binding("Rank"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Forename",
                Binding = new Binding("Forename"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Nickname",
                Binding = new Binding("Nickname"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgcb = new DataGridCheckBoxColumn()
            {
                Header = "$",
                Binding = new Binding("Paid"),
                IsReadOnly = false
            };
            DataGridPlayer.Columns.Add(dgcb);
            dgcb = new DataGridCheckBoxColumn()
            {
                Header = "L",
                Binding = new Binding("ListGiven"),
                IsReadOnly = false
            };
            DataGridPlayer.Columns.Add(dgcb);
            dgcb = new DataGridCheckBoxColumn()
            {
                Header = "!",
                Binding = new Binding("Present"),
                IsReadOnly = false
            };
            DataGridPlayer.Columns.Add(dgcb);
            dgc = new DataGridTextColumn()
            {
                Header = "Team",
                Binding = new Binding("Team"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Faction",
                Binding = new Binding("Faction"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "TP",
                Binding = new Binding("TournamentPoints"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "W",
                Binding = new Binding("Wins"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.OptionalFields.Contains("ModWins"))
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "MW",
                    Binding = new Binding("ModifiedWins"),
                    IsReadOnly = true
                };
                DataGridPlayer.Columns.Add(dgc);
            }
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.IsDrawPossible)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "D",
                    Binding = new Binding("Draws"),
                    IsReadOnly = true
                };
                DataGridPlayer.Columns.Add(dgc);
            }
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.OptionalFields.Contains("ModLoss"))
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "ML",
                    Binding = new Binding("ModifiedLosses"),
                    IsReadOnly = true
                };
                DataGridPlayer.Columns.Add(dgc);
            }
            dgc = new DataGridTextColumn()
            {
                Header = "L",
                Binding = new Binding("Losses"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.OptionalFields.Contains("MoV"))
            {
                dgc = new DataGridTextColumn()
                {
                    Header = tournamentController.ActiveTournament.Rule.MoVName,
                    Binding = new Binding("MarginOfVictory"),
                    IsReadOnly = true
                };
                DataGridPlayer.Columns.Add(dgc);
            }
            dgc = new DataGridTextColumn()
            {
                Header = "SoS",
                Binding = new Binding("StrengthOfSchedule"),
                IsReadOnly = true
            };
            DataGridPlayer.Columns.Add(dgc);
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.OptionalFields.Contains("eSoS"))
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "eSoS",
                    Binding = new Binding("ExtendedStrengthOfSchedule"),
                    IsReadOnly = true
                };
                DataGridPlayer.Columns.Add(dgc);
            }
        }

        private void InitDataGridPairing(bool update = false, bool bonus = false)
        {
            List<String> winners = new List<string>
            {
                "Automatic",
                "Player 1",
                "Player 2"
            };
            DataGridPairing.Columns.Clear();

            DataGridTextColumn dgc;
            DataGridCheckBoxColumn dgcb;
            DataGridComboBoxColumn dgcbc;

            dgcb = new DataGridCheckBoxColumn()
            {
                Header = "Lock"
            };
            Binding b = new Binding("Locked")
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            dgcb.Binding = b;
            DataGridPairing.Columns.Add(dgcb);
            if (!bonus)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "T#",
                    Binding = new Binding("TableNr"),
                    IsReadOnly = true
                };
                DataGridPairing.Columns.Add(dgc);
            }
            dgc = new DataGridTextColumn()
            {
                Header = "Player 1",
                Binding = new Binding("Player1Name"),
                IsReadOnly = true
            };
            DataGridPairing.Columns.Add(dgc);
            if (!bonus)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "Player 2",
                    Binding = new Binding("Player2Name"),
                    IsReadOnly = true
                };
                DataGridPairing.Columns.Add(dgc);
            }
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.IsTournamentPointsInputNeeded && !bonus)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "TP (P1)",
                    Binding = new Binding("Player1Points"),
                    IsReadOnly = false
                };
                DataGridPairing.Columns.Add(dgc);
            }
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.IsTournamentPointsInputNeeded && !bonus)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "TP (P2)",
                    Binding = new Binding("Player2Points"),
                    IsReadOnly = false
                };
                DataGridPairing.Columns.Add(dgc);
            }
            dgc = new DataGridTextColumn()
            {
                Header = "Score (P1)",
                Binding = new Binding("Player1Score"),
                IsReadOnly = false
            };
            DataGridPairing.Columns.Add(dgc);
            if (!bonus)
            {
                dgc = new DataGridTextColumn()
                {
                    Header = "Score (P2)",
                    Binding = new Binding("Player2Score"),
                    IsReadOnly = false
                };
                DataGridPairing.Columns.Add(dgc);
            }
            if (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.IsWinnerDropDownNeeded && !bonus)
            {
                dgcbc = new DataGridComboBoxColumn()
                {
                    Header = "Winner",
                    ItemsSource = winners,
                    TextBinding = new Binding("Winner"),
                    IsReadOnly = false
                };
                DataGridPairing.Columns.Add(dgcbc);
            }
            if (bonus || (!update && (tournamentController.ActiveTournament != null && tournamentController.ActiveTournament.Rule.IsDrawPossible)))
            {
                dgcb = new DataGridCheckBoxColumn()
                {
                    Header = "OK?"
                };
                b = new Binding("ResultEdited")
                {
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                dgcb.Binding = b;
                DataGridPairing.Columns.Add(dgcb);
            }
            if ((update))
            {
                dgcb = new DataGridCheckBoxColumn()
                {
                    Header = "Changed?"
                };
                b = new Binding("ResultEdited")
                {
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                dgcb.Binding = b;
                DataGridPairing.Columns.Add(dgcb);
            }

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            tournamentController.Close();
        }

        private void NewPlayer_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.NewPlayer(new NewPlayerDialog(tournamentController.ActiveTournament.Rule));
            Refresh();
            e.Handled = true;
        }

        private void Refresh()
        {
            currentPairingList = tournamentController.ActiveTournament.Pairings;
            RefreshDataGridPairings();
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
        }

        private void NewTournament_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.NewTournament(new NewTournamentDialog());
            if(tournamentController.ActiveTournament != null)
            {
                SetGUIState(true);
                Refresh();
                InitDataGridPlayer();
                InitDataGridPairing();
            }
            e.Handled = true;
        }

        private void NewTimer_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ShowTimerWindow(new TimerWindow());
        }

        private void Time_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(PrintTime));
        }

        private void RibbonButtonEditPlayer_Click(object sender, RoutedEventArgs e)
        {
            EditPlayer();
            e.Handled = true;
        }

        private void EditPlayer()
        {
            if(DataGridPlayer.SelectedItems.Count > 1)
            {
                tournamentController.ActiveIO.ShowMessage("Please select only 1 player you want to edit.");
                return;
            }
            if (DataGridPlayer.SelectedIndex >= 0)
            {
                tournamentController.EditPlayer(new NewPlayerDialog(tournamentController.ActiveTournament.Rule), DataGridPlayer.SelectedIndex);
                RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
            }
        }

        private void GOEPPImport_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Import(new NewTournamentDialog(), false);
            if (tournamentController.ActiveTournament != null)
            {
                ComboBoxRounds.Items.Clear();
                InitDataGridPlayer();
                InitDataGridPairing();
                SetGUIState(true);
                DataGridPlayer.ItemsSource = tournamentController.ActiveTournament.Participants;
                RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
            }
        }

        private void GOEPPExport_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.GOEPPExport();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Save(ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load(bool autosave = false)
        {
            tournamentController.Load(new AutosaveDialog(), autosave);
            if(tournamentController.ActiveTournament != null)
            {
                ComboBoxRounds.Items.Clear();
                if (tournamentController.ActiveTournament.Rounds != null)
                {
                    for (int i = 1; i <= tournamentController.ActiveTournament.Rounds.Count; i++)
                        AddRoundButton(i);
                    if (tournamentController.ActiveTournament.Rounds != null && tournamentController.ActiveTournament.Rounds.Count > 0)
                        DataGridPairing.ItemsSource = tournamentController.ActiveTournament.Rounds[tournamentController.ActiveTournament.Rounds.Count - 1].Pairings;
                    if (tournamentController.ActiveTournament.FirstRound && (tournamentController.ActiveTournament.Rounds == null || tournamentController.ActiveTournament.Rounds.Count == 0))
                    {
                        SetGUIState(true);
                    }
                    else
                    {
                        SetGUIState(false, true);
                    }
                    ButtonGetResults.Content = tournamentController.ActiveTournament.ButtonGetResultsText;
                    ButtonGetResults.IsEnabled = true;
                    ButtonCut.IsEnabled = tournamentController.ActiveTournament.ButtonCutState == true;
                    tournamentController.ActiveTournament.Sort();
                    RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
                    if (tournamentController.ActiveTournament.Pairings != null)
                    {
                        currentPairingList = tournamentController.ActiveTournament.Pairings;
                        RefreshDataGridPairings();
                    }

                    InitDataGridPlayer();
                    if (tournamentController.ActiveTournament.bonus)
                    {
                        InitDataGridPairing(false, true);
                    }
                    else
                    {
                        InitDataGridPairing();
                    }
                    ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
                }
            }
        }

        private void GetSeed(bool cut = false)
        {
            List<Pairing> pairings = tournamentController.GetSeed(cut);
            currentPairingList = pairings;
            RefreshDataGridPairings();
            AddRoundButton();
            ChangeGUIState(true);
            tournamentController.Save(ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled, true);
        }

        private void PariringCurrentCellChanged(object sender, EventArgs e)
        { 
            DataGridPairing.CommitEdit();
            refresh++;
            if (refresh >= 5 && currentPairingList != null)
            {
                RefreshDataGridPairings();
                refresh = 0;
            }
        }

        private void RefreshDataGridPlayer(List<Player> players)
        {
            DataGridPlayer.ItemsSource = null;
            DataGridPlayer.ItemsSource = players;
            dataView = CollectionViewSource.GetDefaultView(DataGridPlayer.ItemsSource);
            dataView.SortDescriptions.Clear();

            tournamentController.ActiveTournament.Sort();

            dataView.SortDescriptions.Add(new SortDescription("Rank", ListSortDirection.Ascending));
            dataView.Refresh();
        }

        private void RefreshDataGridPairings()
        {
            if (currentPairingList == null)
                return;
            DataGridPairing.ItemsSource = null;
            List<Pairing> p = new List<Pairing>();
            if (hide)
            {
                foreach (var pa in currentPairingList)
                {
                    if (!pa.Hidden)
                        p.Add(pa);
                }
            }
            else
            {
                p = currentPairingList;
            }
            DataGridPairing.ItemsSource = p;
        }

        private void DataGridPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPlayer.SelectedItem != null)
            {
                RemovePlayerIsEnabled = true;
                DisqualifyPlayerIsEnabled = tournamentController.Started;
                EditPlayerIsEnabled = true;
            }
            else
            {
                RemovePlayerIsEnabled = false;
                EditPlayerIsEnabled = false;
                DisqualifyPlayerIsEnabled = false;
            }
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

            try
            {
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
            catch (ArgumentOutOfRangeException) { }
            catch (ArgumentNullException) { }
        }

        private void DataGridPlayer_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            EditPlayer();
            e.Handled = true;
        }

        private void ButtonNextRound_Click(object sender, RoutedEventArgs e)
        {
            GetSeed();
        }

        private void AddRoundButton(int actRound = -1)
        {
            ComboBoxItem newListItem = new ComboBoxItem();
            if (actRound == -1)
                actRound = tournamentController.ActiveTournament.Rounds.Count;
            newListItem.Content = actRound;
            ComboBoxRounds.Items.Add(newListItem);
        }

        private void ButtonGetResults_Click(object sender, RoutedEventArgs e)
        {
            if(ButtonGetResults.Content.ToString() == "Start Tournament")
            {
                if (tournamentController.StartTournament(ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled))
                {
                    SetGUIState(false, true);
                }
                return;
            }
            if(ButtonGetResults.Content.ToString() == "Next Round")
            {
                GetSeed();
                return;
            }
            if (ButtonGetResults.Content.ToString() == "Update")
            {
                if (tournamentController.GetResults((List<Pairing>)DataGridPairing.ItemsSource, ButtonGetResults.Content.ToString(), false, true))
                {
                    RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
                    tournamentController.ActiveIO.ShowMessage("Update done!");
                    ButtonGetResults.IsEnabled = true;
                    ButtonGetResults.Content = tournamentController.ActiveTournament.ButtonGetResultsText;
                    ButtonCut.IsEnabled = tournamentController.ActiveTournament.ButtonCutState;
                    ComboBoxRounds.SelectedIndex = ComboBoxRounds.Items.Count - 1;
                    currentPairingList = tournamentController.ActiveTournament.Pairings;
                    RefreshDataGridPairings();
                    ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
                    tournamentController.Save(ButtonGetResults.Content.ToString(), false, true, "Update_Round");
                }
                return;
            }
            if (ButtonGetResults.Content.ToString() == "Get Results")
            {
                if (tournamentController.GetResults((List<Pairing>)DataGridPairing.ItemsSource, ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled))
                {
                    RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
                    ChangeGUIState(false);
                    tournamentController.Save(ButtonGetResults.Content.ToString(), false, true, "Result_Round");
                }
                return;
            }
        }

        private void ComboBoxRounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string header = ((ComboBox)sender).SelectedValue.ToString();
            header = header.Remove(0, header.IndexOf(" "));
            int round = Int32.Parse(header);
            currentPairingList = tournamentController.ActiveTournament.Rounds[round - 1].Pairings;
            RefreshDataGridPairings();
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Rounds[round - 1].Participants);
            if (tournamentController.ActiveTournament.Rounds.Count == round)
            {
                ButtonGetResults.IsEnabled = true;
                ButtonGetResults.Content = tournamentController.ActiveTournament.ButtonGetResultsText;
                ButtonCut.IsEnabled = tournamentController.ActiveTournament.ButtonCutState;
                InitDataGridPairing();
            }
            else
            {
                tournamentController.ActiveTournament.ButtonGetResultsText = ButtonGetResults.Content.ToString();
                tournamentController.ActiveTournament.ButtonCutState = ButtonCut.IsEnabled;
                ButtonGetResults.IsEnabled = true;
                ButtonGetResults.Content = "Update";
                ButtonCut.IsEnabled = false;
                InitDataGridPairing(true);
            }
            tournamentController.ActiveTournament.DisplayedRound = round;
            ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
        }

        private void ButtonAutosave_Click(object sender, RoutedEventArgs e)
        {
            if (tournamentController.ActiveIO.AutosavePathExists)
                Load(true);
            else
                tournamentController.ActiveIO.ShowMessage("No Autosavefolder");
        }

        private void MenuItemOpenAutoSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ActiveIO.OpenAutosaveFolder();
        }

        private void MenuItemTSettings_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.EditTournament(new NewTournamentDialog());           
        }

        private void ButtonCut_Click(object sender, RoutedEventArgs e)
        {
            GetSeed(true);
        }

        private void MenuItemDeleteAutosave_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ActiveIO.DeleteAutosaveFolder();
        }

        private void RemovePlayer_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridPlayer.SelectedItems.Count > 1)
            {
                foreach(Player s in DataGridPlayer.SelectedItems)
                {
                    tournamentController.RemovePlayer(s);
                }
            }
            else if (DataGridPlayer.SelectedIndex >= 0)
            {
                tournamentController.RemovePlayer(DataGridPlayer.SelectedIndex);
            }
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
        }

        private void ButtonChangePairing_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.EditPairings(new SetPairingDialog(), ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled);
            currentPairingList = tournamentController.ActiveTournament.Pairings;
            RefreshDataGridPairings();
        }

        private void MenuItemResetLastResults_Click(object sender, RoutedEventArgs e)
        {
            List<Pairing> pl = tournamentController.ResetLastResults();
            ChangeGUIState(true);
            currentPairingList = pl;
            RefreshDataGridPairings();
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
        }

        private void SetGUIState(bool start, bool tournamentStart = false)
        {
            if (start)
            {
                NewPlayerIsEnabled = true;
                MenuItemTSettings.IsEnabled = tournamentController.ActiveTournament != null;
                ButtonNewTournament.IsEnabled = true;
                ButtonGOEPPImport.IsEnabled = true;
                EditPlayerIsEnabled = tournamentController.ActiveTournament != null;
                RemovePlayerIsEnabled = tournamentController.ActiveTournament != null;
                ChangePairingIsEnabled = tournamentController.ActiveTournament.Pairings != null;
                SaveIsEnabled = tournamentController.ActiveTournament != null;
                ResetLastResultsIsEnabled = false;
                ButtonGetResults.IsEnabled = true;
                ButtonGetResults.Content = "Start Tournament";
                DisqualifyPlayerIsEnabled = false;
            }
            if (tournamentStart)
            {
                if (tournamentController.ActiveTournament.Rounds.Count > 1)
                    NewPlayerIsEnabled = false;
                else
                    NewPlayerIsEnabled = true;
                MenuItemTSettings.IsEnabled = true;
                ButtonGOEPPExport.IsEnabled = true;
                //ButtonEndTournament.IsEnabled = true;
                EditPlayerIsEnabled = false;
                RemovePlayerIsEnabled = false;
                ChangePairingIsEnabled = true;
                SaveIsEnabled = true;
                ResetLastResultsIsEnabled = true;
                DisqualifyPlayerIsEnabled = true;
                ButtonGetResults.Content = "Next Round";
            }
            ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
        }

        private void ChangeGUIState(bool seed, bool end = false)
        {
            if (seed)
            {
                ButtonGetResults.Content = "Get Results";
                ButtonCut.IsEnabled = false;
                MenuItemResetLastResults.IsEnabled = false;
                DisqualifyPlayerIsEnabled = true;
            }
            else
            {
                ButtonGetResults.Content = "Next Round";
                MenuItemResetLastResults.IsEnabled = !end;
                ButtonGetResults.IsEnabled = !end;
                if (tournamentController.ActiveTournament.Cut == TournamentCut.NoCut || tournamentController.ActiveTournament.CutStarted)
                    ButtonCut.IsEnabled = false;
                else
                    ButtonCut.IsEnabled = true;
            }
            ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
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

        private void DisqualifyPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlayer.SelectedItems.Count > 1)
            {
                foreach (Player s in DataGridPlayer.SelectedItems)
                {
                    tournamentController.RemovePlayer(s, true);
                }
            }
            else if (DataGridPlayer.SelectedIndex >= 0)
            {
                tournamentController.RemovePlayer(DataGridPlayer.SelectedIndex, true);
            }
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
        }

        private void MenuItemShowPairings_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ShowProjector(new ProjectorWindow(), false);
        }

        private void MenuItemShowTable_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ShowProjector(new ProjectorWindow(), true);
        }

        private void MenuItemPrint_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Print(true);
        }

        private void MenuItemTimeStart_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.StartTimer(TextBoxStartTime.Text);
        }

        private void MenuItemTimePause_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.PauseTimer();
            if (tournamentController.ActiveTimer.Started)
                ButtonPause.Content = "Pause";
            else
                ButtonPause.Content = "Resume";
        }

        private void MenuItemTimeReset_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ResetTimer();
        }

        private void PrintTime()
        {
            LabelTime.Content = tournamentController.ActiveTimer.ActualTime;
        }

        private void MenuItemPrintPairing_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Print(true, true);
        }

        private void RefreshPlayerList(object sender, RoutedEventArgs e)
        {
            RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
        }

        private void RefreshPairingsList(object sender, RoutedEventArgs e)
        {
            currentPairingList = tournamentController.ActiveTournament.Pairings;
            RefreshDataGridPairings();
        }

        private void MenuItemPrintParingScore_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.PrintScoreSheet();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog ad = new AboutDialog()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            tournamentController.ShowAbout(ad);
        }

        private void DataGridPlayer_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //(List<Player>)Data.ItemsSource
            if(e.EditAction == DataGridEditAction.Commit)
            {
                if (DataGridPlayer.SelectedItems.Count > 1)
                    return;
                Player player = tournamentController.ActiveTournament.Participants[DataGridPlayer.SelectedIndex];
                player.Present = !player.Present;
                //activeTournament.ChangePlayer(player);
            }
        }

        private void MenuItemPrintResult_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Print(true, true, true);
        }

        private void MenuItemBBCode_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.GetBBCode(new OutputDialog(), new WindowsClipboard());
        }

        private void MenuItemCSVImport_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Import(new NewTournamentDialog(), true);
            if (tournamentController.ActiveTournament != null)
            {
                ComboBoxRounds.Items.Clear();
                InitDataGridPlayer();
                InitDataGridPairing();
                SetGUIState(true);
                DataGridPlayer.ItemsSource = tournamentController.ActiveTournament.Participants;
                RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
            }
        }

        private void TextBoxTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxTime.Text = tournamentController.SetTimer(TextBoxTime.Text);
        }

        private void SetImage_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.SetImage();
        }

        private void SliderColor_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                tournamentController.SetTimerLabelColor(SliderText.Value == 1.0);
            }
            catch (NullReferenceException)
            { }
        }

        private void SliderSize_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                tournamentController.SetTimerTextSize(SliderSize.Value);
            }
            catch (NullReferenceException)
            { }
        }

        private void EndTournament_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.CalculateWonByes();
        }

        private void MenuItemUserHelp_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.ShowUserManual();
        }

        private void MenuItemBonusPoints_Click(object sender, RoutedEventArgs e)
        {
            List<Pairing> pairings = tournamentController.AwardBonusPoints();
            InitDataGridPairing(false, true);
            currentPairingList = pairings;
            RefreshDataGridPairings();
            AddRoundButton();
            ChangeGUIState(true);
            tournamentController.Save(ButtonGetResults.Content.ToString(), ButtonCut.IsEnabled, true);
        }

        private void DataGridPairing_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if(e.Column.DisplayIndex != 0)
            {
                int t = e.Row.GetIndex();
                if (tournamentController.ActiveTournament.Pairings[e.Row.GetIndex()].Locked)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SliderHide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            hide = SliderHide.Value == 2.0;
            RefreshDataGridPairings();
        }
    }
}
