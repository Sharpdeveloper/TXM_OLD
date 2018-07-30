using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using TXM.Core;

namespace TXM.GUI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für AutosaveDialog.xaml
    /// </summary>
    public partial class AutosaveDialog : Window, IAutoSaveDialog
    {
        private bool dialogReturn = false;
        private string filename = "";
        private ICollectionView dataView;

        public AutosaveDialog()
        {
            InitializeComponent();
        }

        public void Init(List<AutosaveFile> files)
        {
            DataGridAutosave.Columns.Clear();

            DataGridTextColumn dgc;

            dgc = new DataGridTextColumn()
            {
                Header = "Date",
                Binding = new Binding("Date"),
                IsReadOnly = true
            };
            DataGridAutosave.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Time",
                Binding = new Binding("Time"),
                IsReadOnly = true
            };
            DataGridAutosave.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Tournament",
                Binding = new Binding("Tournament"),
                IsReadOnly = true
            };
            DataGridAutosave.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "State",
                Binding = new Binding("State"),
                IsReadOnly = true
            };
            DataGridAutosave.Columns.Add(dgc);
            dgc = new DataGridTextColumn()
            {
                Header = "Round",
                Binding = new Binding("Round"),
                IsReadOnly = true
            };
            DataGridAutosave.Columns.Add(dgc);

            DataGridAutosave.ItemsSource = null;
            DataGridAutosave.ItemsSource = files;
            dataView = CollectionViewSource.GetDefaultView(DataGridAutosave.ItemsSource);
            dataView.SortDescriptions.Clear();

            dataView.Refresh();
        }

        public string GetFileName()
        {
            return filename;
        }

        public bool GetDialogReturn()
        {
            return dialogReturn;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dialogReturn = true;
            filename = ((AutosaveFile)DataGridAutosave.SelectedItem).Filename;
            Close();
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        private void DataGridAutosave_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(DataGridAutosave.SelectedItem == null)
            {
                return;
            }
            dialogReturn = true;
            filename = ((AutosaveFile)DataGridAutosave.SelectedItem).Filename;
            Close();
        }
    }
}
