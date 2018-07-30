using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für NewPlayerDialog.xaml
    /// </summary>
    public partial class NewPlayerDialog : Window, IPlayerDialog
    {
        private bool dialogResult = false;
        private bool changes = false;
        private Player CurrentPlayer;
        private bool nicknameRequiered = true;

        public NewPlayerDialog(AbstractRules rules)
        {
            InitializeComponent();
            TextBoxNickname.Focus();
            changes = false;
            //Noch steuern, welcher Spieler angelegt werden soll
            foreach (string s in rules.Factions)
                ComboBoxFaction.Items.Add(s);
        }

        public void SetPlayer(Player player)
        {
            CurrentPlayer = player;
            TextBoxForename.Text = CurrentPlayer.Forename;
            TextBoxName.Text = CurrentPlayer.Name;
            TextBoxNickname.Text = CurrentPlayer.Nickname;
            TextBoxTeam.Text = CurrentPlayer.Team;
            ComboBoxFaction.SelectedValue = CurrentPlayer.Faction;
            if(ComboBoxFaction.SelectedValue == null)
            {
                ComboBoxFaction.Items.Add(CurrentPlayer.Faction);
                ComboBoxFaction.SelectedValue = CurrentPlayer.Faction;
            }
            CheckboxFreeticket.IsChecked = ((Player)CurrentPlayer).WonBye;
            CheckboxPayed.IsChecked = CurrentPlayer.Paid;
            CheckboxSquadListGiven.IsChecked = CurrentPlayer.ListGiven;
            TextBoxNickname.IsEnabled = false;
            nicknameRequiered = false;
            TextBoxTableNR.Text = player.TableNo.ToString();
            CheckboxPresent.IsChecked = CurrentPlayer.Present;
            
        }

        public Player GetPlayer()
        {
            Player player = new Player(TextBoxNickname.Text, ComboBoxFaction.SelectedValue.ToString());
            player.Team = TextBoxTeam.Text;
            player.Name = TextBoxName.Text;
            player.Forename = TextBoxForename.Text;
            player.WonBye = CheckboxFreeticket.IsChecked == true;
            player.ListGiven = CheckboxSquadListGiven.IsChecked == true;
            player.Paid = CheckboxPayed.IsChecked == true;
            player.TableNo = TableNr;
            player.Present = CheckboxPresent.IsChecked == true;
            return player;
        }

        public bool GetDialogResult()
        {
            return dialogResult;
        }

        public bool IsChanged()
        {
            return changes;
        }

        public void SetButtonOKText(string text)
        {
            ButtonOK.Content = text;
        }

        public void SetTitle(string text)
        {
            Title = text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dialogResult = false;
            this.Close();
        }

        private void NewPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewPlayer();
        }

        private void NewPlayer()
        {
            if (TextBoxNickname.Text == "" && nicknameRequiered)
                return;
            else if (ComboBoxFaction.SelectedIndex == -1)
                return;
            else
            {
                dialogResult = true;
                this.Close();
            }
        }

        private void ValueChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        private void ValueChanged(object sender, SelectionChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                NewPlayer();
            }
        }

        private int TableNr
        {
            get
            {
                int t;
                try
                {
                    t = Int32.Parse(TextBoxTableNR.Text);
                }
                catch
                {
                    t = 0;
                }
                return t;
            }
        }

        private void TextBoxNickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        private void IntegerUpDownTableNr_ValueChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }
    }
}
