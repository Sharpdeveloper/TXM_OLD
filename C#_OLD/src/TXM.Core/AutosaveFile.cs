using System;
using System.IO;

namespace TXM.Core
{
    public class AutosaveFile
    {
        //Autosave_131430321409284710_Das Erwachen der Nacht - Episode III_Pairings_Round2
        public string Date { get; set; }
        public string Time { get; set; }
        public string Tournament { get; set; }
        public string State { get; set; }
        public string Round { get; set; }
        public string Filename { get; set; }

        public AutosaveFile(string filename)
        {
            Filename = filename;
            filename = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar)+1);
            string[] parts = filename.Split('_');
            DateTime dt = new DateTime(long.Parse(parts[1]));
            dt = dt.AddYears(1600);
            Date = dt.ToShortDateString();
            Time = dt.ToShortTimeString();
            Tournament = parts[2];
            try
            {
                State = parts[3].Split('.')[0];
            }
            catch (Exception)
            {
                State = parts[3];
            }
            try
            {
                Round = parts[4].Split('.')[0];
            }
            catch(Exception)
            {
                Round = "";
            }
        }
    }
}
