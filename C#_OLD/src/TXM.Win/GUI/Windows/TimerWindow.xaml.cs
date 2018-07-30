using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TXM.Core;

namespace TXM.GUI.Windows
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class TimerWindow : Window, ITimerWindow
    {
        private TournamentTimer timer;

        public TimerWindow()
        {

            InitializeComponent();
            LabelTime.Content = "MM:SS";
        }

        public void SetTimer(TournamentTimer t)
        {
            timer = t;
            t.Changed += TimeChanged;
        }

        private void TimeChanged(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(AktZeit));
        }

        private void AktZeit()
        {
            LabelTime.Content = timer.ActualTime;
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            timer.StartTimer();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            timer.PauseTimer();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            timer.ResetTimer();
        }

        public void SetLabelColor(bool white)
        {
            if (white)
            {
                LabelTime.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                LabelTime.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        public bool SetImage(Uri uri)
        {
            try
            {
                BackGroundImage.Source = new BitmapImage(uri);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private new void Show()
        {
            base.Show();
        }

        public void SetTextSize(double size)
        {
            LabelTime.FontSize = size * 8.0;
        }

        public void Quit()
        {
            this.Close();
        }
    }
}
