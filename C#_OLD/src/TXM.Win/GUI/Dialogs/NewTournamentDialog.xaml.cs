using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für NewTournamentDilaog.xaml
    /// </summary>
    public partial class NewTournamentDialog : Window, ITournamentDialog
    {
        public bool dialogResult = false;
        public bool changes = false;
        private List<string> tournamentTypes;
        private IO io;

        public NewTournamentDialog()
        {
            InitializeComponent();

            tournamentTypes = new List<string>
            {
                "SWISS"
            };
            foreach (string s in tournamentTypes)
                ComboBoxTournamentType.Items.Add(s);

            foreach (string s in AbstractRules.GetAllRuleNames())
                ComboBoxGameSystem.Items.Add(s);

            ComboBoxTournamentType.SelectedIndex = 0;
            changes = false;
        }

        public void SetTournament(Tournament tournament)
        {
            bool t3 = tournament == null ? false : tournament.T3ID != 0;

            ComboBoxGameSystem.Items.Clear();

            foreach (string s in AbstractRules.GetAllRuleNames(t3))
                ComboBoxGameSystem.Items.Add(s);

            TextboxName.Text = tournament.Name;
            if (tournament.Cut == TournamentCut.Top4)
                RadioButtonTop4.IsChecked = true;
            else if (tournament.Cut == TournamentCut.Top8)
                RadioButtonTop8.IsChecked = true;
            else if (tournament.Cut == TournamentCut.Top16)
                RadioButtonTop16.IsChecked = true;
            else
                RadioButtonNoCut.IsChecked = true;
            radioButtonTPYes.IsChecked = tournament.TeamProtection;
            radioButtonTPNo.IsChecked = !tournament.TeamProtection;
            TextBoxMaxSquad.Text = tournament.MaxPoints.ToString();
            if (tournament.Rule != null)
            {
                ComboBoxGameSystem.SelectedValue = tournament.Rule.GetName();
            }
            ComboBoxGameSystem.IsEnabled = true;
        }

        public void SetGameSystemIsChangeable(bool isGametypeChangeable)
        {
            ComboBoxGameSystem.IsEnabled = isGametypeChangeable;
        }

        private void NewTournament_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTournamentType.SelectedValue == null)
            {
                io.ShowMessage("The tournamenttype must be choosen.");
                return;
            }
            if (TextboxName.Text == "")
            {
                io.ShowMessage("You have to choose a tournamentname.");
                return;
            }
            if (ComboBoxGameSystem.SelectedValue == null)
            {
                io.ShowMessage("You didn't choose a gamesystem.");
                return;
            }
            dialogResult = true;
            this.Close();
        }

        public Tournament GetTournament()
        {
            Tournament tournament = new Tournament(TextboxName.Text, GetMaxSquadSize(), GetCut(), GetRule())
            {
                TeamProtection = radioButtonTPYes.IsChecked == true,
                Single = radioButtonTypeSingle.IsChecked == true,
                PrintDDGER = PrintDDGER(),
                PrintDDENG = PrintDDENG()
            };
            return tournament;
        }

        public bool IsChanged()
        {
            return changes;
        }

        public bool GetDialogResult()
        {
            return dialogResult;
        }

        public void SetIO (IO _io)
        {
            io = _io;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dialogResult = false;
            changes = false;
            this.Close();
        }

        private string GetTournamentMode()
        {
            return ComboBoxTournamentType.SelectedValue.ToString();
        }

        private int GetMaxSquadSize()
        {
            int t;
            try
            {
                t = Int32.Parse(TextBoxMaxSquad.Text);
            }
            catch
            {
                t = GetRule().DefaultMaxPoints;
            }
            return t;
        }

        private TournamentCut GetCut()
        {
            if (RadioButtonTop4.IsChecked == true)
                return TournamentCut.Top4;
            else if (RadioButtonTop8.IsChecked == true)
                return TournamentCut.Top8;
            else if (RadioButtonTop16.IsChecked == true)
                return TournamentCut.Top16;
            else
                return TournamentCut.NoCut;
        }

        private bool PrintDDGER()
        {
            return false;
            //return radioButtonPDD_GER.IsChecked == true;
        }

        private bool PrintDDENG()
        {
            return false;
            //return radioButtonPDD_ENG.IsChecked == true;
        }

        private AbstractRules GetRule()
        {
            if (ComboBoxGameSystem.SelectedValue == null)
                return AbstractRules.GetRule(XWingRules.GetRuleName());
            return AbstractRules.GetRule(ComboBoxGameSystem.SelectedValue.ToString());
        }

        private void ComboBoxTournamentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changes = true;
            if (ComboBoxGameSystem.SelectedValue == null)
                return;
            AbstractRules r = GetRule();
            if (r.IsDoubleElimination)
            {
                RadioButtonNoCut.IsEnabled = false;
                RadioButtonNoCut.IsChecked = true;
                RadioButtonTop4.IsEnabled = false;
                RadioButtonTop4.IsChecked = false;
                RadioButtonTop8.IsEnabled = false;
                RadioButtonTop8.IsChecked = false;
                RadioButtonTop16.IsEnabled = false;
                RadioButtonTop16.IsChecked = false;
            }
            else
            {
                RadioButtonNoCut.IsEnabled = true;
                RadioButtonTop4.IsEnabled = true;
                RadioButtonTop8.IsEnabled = true;
                RadioButtonTop16.IsEnabled = true;
            }
            TextBoxMaxSquad.Text = GetRule().DefaultMaxPoints.ToString();
            e.Handled = true;
        }

        private void TextboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        private void IntegerUpDownMaxSquad_ValueChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
            e.Handled = true;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
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
