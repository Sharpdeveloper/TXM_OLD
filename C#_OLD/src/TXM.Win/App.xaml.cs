using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace TXM
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TXM", "error.log");
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                string line = "Exception: " + e.Exception.ToString();
                sw.WriteLine(line);
                line = "InnerException: " + Convert.ToString(e.Exception.InnerException);
                sw.WriteLine(line);
            }
            // Prevent default unhandled exception processing
            StringBuilder StringBuilder1 = new StringBuilder();
            StringBuilder1.AppendFormat("Operation System:  {0}\n", Environment.OSVersion);
            if (Environment.Is64BitOperatingSystem)
                StringBuilder1.AppendFormat("\t\t  64 Bit Operating System\n");
            else
                StringBuilder1.AppendFormat("\t\t  32 Bit Operating System\n");
            StringBuilder1.AppendFormat(".Net Framework:  {0}\n", Environment.Version);
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                string line = "System:\n" + StringBuilder1.ToString();
                sw.WriteLine(line);
                line = "TXM Version: " + TXM.Core.Settings.TXMVERSION;
                sw.WriteLine(line);
            }

            MessageBox.Show("An error occured. Please check the following file: " + path);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
