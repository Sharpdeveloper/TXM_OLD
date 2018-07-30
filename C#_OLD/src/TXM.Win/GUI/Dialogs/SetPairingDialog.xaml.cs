using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für SetPairingDialog.xaml
    /// </summary>
    public partial class SetPairingDialog : Window, IPairingDialog
    {
        private List<Player> Players;
        private List<Player> PlayerWithoutPairing;
        private List<Pairing> PremadePairing;
        private List<string> tempList; 
        private string Nick1;
        private string Nick2;
        public bool OK = false;
        private List<int> unusedTables;

        public SetPairingDialog()
        {
            InitializeComponent();

            unusedTables = new List<int>();

            PlayerWithoutPairing = new List<Player>();

            SetComboboxPlayer1();
            ComboboxPlayer2.Items.Add("Player  2");
            ComboboxPlayer2.SelectedIndex = 0;
            ButtonAdd.IsEnabled = false;
        }

        public void SetParticipants(List<Player> participants)
        {
            Players = participants;
        }

        public void SetPairings(List<Pairing> pairings)
        {
            
            PremadePairing = pairings;

            if (PremadePairing == null)
                PremadePairing = new List<Pairing>();
            else
            {
                foreach (Pairing p in PremadePairing)
                {
                    ListboxPairings.Items.Add(p.Player1.DisplayName + " VS " + p.Player2.DisplayName);
                    Players[Players.IndexOf(p.Player1)].Paired = true;
                    try
                    {
                        Players[Players.IndexOf(p.Player2)].Paired = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    { }
                }

                foreach (Player player in Players)
                {
                    if (!player.Paired)
                        PlayerWithoutPairing.Add(player);
                }
            }
        }

        public List<Pairing> GetPairings()
        {
            return PremadePairing;
        }

        public bool GetDialogResult()
        {
            return OK;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Pairing p;
            if(unusedTables.Count == 0)
                p = new Pairing();
            else
            {
                p = new Pairing(unusedTables[0]);
                unusedTables.Remove(unusedTables[0]);
            }
            for (int i = 0; i < PlayerWithoutPairing.Count; i++)
            {
                Player player = PlayerWithoutPairing[i];
                if (player.DisplayName == Nick1)
                {
                    p.Player1 = player;
                    player.Paired = true;
                    PlayerWithoutPairing.Remove(player);
                    i--;
                }
                else if (player.DisplayName == Nick2)
                {
                    p.Player2 = player;
                    player.Paired = true;
                    PlayerWithoutPairing.Remove(player);
                    i--;
                }
                if (p.Player1 != null && (p.Player2 != null || Nick2 == "Bye"))
                    break;
            }
            if (Nick2 == "Bye")
            {
                p.Player1.Bye = true;
                p.Player2 = new Player("Bye");
            }
            PremadePairing.Add(p);
            ListboxPairings.Items.Add(Nick1 + " VS " + Nick2);    
            SetComboboxPlayer1();
            ComboboxPlayer2.IsEnabled = false;
            ComboboxPlayer2.SelectedIndex = 0;
            ButtonAdd.IsEnabled = false;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            OK = true;
            PremadePairing = PremadePairing.OrderBy(x => x.TableNr).ToList<Pairing>();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Player1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)ComboboxPlayer1.SelectedValue;
            if (value != "Player 1" && value != null)
            {
                Nick1 = value;
                ComboboxPlayer2.IsEnabled = true;
                SetComboboxPlayer2();
            }
            else
            {
                Nick1 = "";
                ComboboxPlayer2.IsEnabled = false;
            }
        }

        private void Player2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)ComboboxPlayer2.SelectedValue;
            if (value != "Player 2" && value != null)
            {
                Nick2 = value;
                ButtonAdd.IsEnabled = true;
            }
            else
            {
                Nick2 = "";
                ButtonAdd.IsEnabled = false;
            }
        }

        private void SetComboboxPlayer1()
        {
            ComboboxPlayer1.Items.Clear();
            ComboboxPlayer1.Items.Add("Player 1");
            tempList = new List<string>();
            foreach (Player player in PlayerWithoutPairing)
                tempList.Add(player.DisplayName);
            tempList.Sort();
            foreach (var s in tempList)
                ComboboxPlayer1.Items.Add(s);
            ComboboxPlayer1.SelectedIndex = 0;
        }

        private void SetComboboxPlayer2()
        {
            ComboboxPlayer2.Items.Clear();
            ComboboxPlayer2.Items.Add("Player 2");
            ComboboxPlayer2.Items.Add("Bye");
            tempList = new List<string>();
            foreach (Player player in PlayerWithoutPairing)
            {
                if (player.DisplayName != Nick1)
                    tempList.Add(player.DisplayName);
            }
            tempList.Sort();
            foreach (var s in tempList)
                ComboboxPlayer2.Items.Add(s);
            ComboboxPlayer2.SelectedIndex = 0;
        }

        private void ButtonSub_Click(object sender, RoutedEventArgs e)
        {
            int at = ListboxPairings.SelectedIndex;
            if(PremadePairing[at].Player1.Nickname != "Bye")
                PlayerWithoutPairing.Add(PremadePairing[at].Player1);
            if (PremadePairing[at].Player2.Nickname != "Bye")
                PlayerWithoutPairing.Add(PremadePairing[at].Player2);
            unusedTables.Add(PremadePairing[at].TableNr);
            PremadePairing.RemoveAt(at);
            ListboxPairings.Items.RemoveAt(at);
            string value = (string)ComboboxPlayer1.SelectedValue;
            if (value == "Player 1")
            {
                SetComboboxPlayer1();
            }
            else
            {
                SetComboboxPlayer1();
                ComboboxPlayer1.SelectedValue = value;
                value = (string)ComboboxPlayer2.SelectedValue;
                if (value == "Player 2")
                {
                    SetComboboxPlayer2();
                }
                else
                {
                    SetComboboxPlayer2();
                    ComboboxPlayer2.SelectedValue = value;
                    ButtonAdd.IsEnabled = true;
                }
                ComboboxPlayer2.IsEnabled = true;
            }
        }

        private void LbPairing_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (ListboxPairings.SelectedItem != null)
                ButtonSub.IsEnabled = true;
            else
                ButtonSub.IsEnabled = false;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }
    }
}
