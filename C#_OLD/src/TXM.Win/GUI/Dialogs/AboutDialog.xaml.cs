using System.Windows;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window, IAboutDialog
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            textBox.Text = text;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }
    }
}
