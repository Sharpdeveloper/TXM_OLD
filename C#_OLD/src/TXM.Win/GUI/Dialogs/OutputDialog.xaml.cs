using System.Windows;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für OuputDialog.xaml
    /// </summary>
    public partial class OutputDialog : Window, IOutputDialog
    {
        private bool ok = false;

        public OutputDialog()
        {
            InitializeComponent();
        }

        public bool GetDialogResult()
        {
            return ok;
        }

        public bool IsPairingOutput()
        {
            return CheckBoxCurrentPairings.IsChecked == true;
        }

        public bool IsResultOutput()
        {
            return CheckBoxLastResult.IsChecked == true;
        }

        public bool IsTableOutput()
        {
            return CheckBoxCurrentTable.IsChecked == true;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ok = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
