//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//
//using Newtonsoft.Json;
//
//namespace TOWare.Core
//{
//    /// <summary>
//    /// This class offers all functions which are needed to save and load files
//    /// </summary>
//    public class IO
//    {
//        private IFileManager fileManager;
//        private IMessageManager messageManager;
//        private string SettingsFile
//        {
//            get
//            {
//                return Path.Combine(WorkPath, "settings.towset");
//            }
//        }
//        private string LanguagePath
//        {
//            get
//            {
//                return Path.Combine(WorkPath, "Language");
//            }
//        }
//
//        /// <summary>
//        /// active Settings of the application
//        /// </summary>
//        public Settings ActiveSettings { get; private set; }
//        /// <summary>
//        /// active Langauge of the application
//        /// </summary>
//        public Lang ActiveLanguage { get; private set; }
//        /// <summary>
//        /// Workpath (User/TOWare/..) where settings, autosave etc. are saved
//        /// </summary>
//        public string WorkPath { get; private set; }
//
//
//        public IO(IFileManager filemanager, IMessageManager messagemanager, Lang language)
//        {
//            fileManager = filemanager;
//            messageManager = messagemanager;
//            ActiveLanguage = language;
//
//            WorkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TOWare");
//
//            if (!Directory.Exists(WorkPath))
//                Directory.CreateDirectory(WorkPath);
//            if (!Directory.Exists(LanguagePath))
//                Directory.CreateDirectory(LanguagePath);
//            if (File.Exists(SettingsFile))
//            {
//                LoadSettings();
//                if (File.Exists(Path.Combine(LanguagePath, ActiveSettings.ActiveLanguage + ".towlang")))
//                    LoadLanguage();
//                else
//                    SaveLanguage();
//            }
//            else
//            {
//                ActiveSettings = new Settings();
//                SaveSettings(ActiveSettings);
//                SaveLanguage();
//            }
//
//        }
//
//        /// <summary>
//        /// loads the settings from filesystem
//        /// </summary>
//        private void LoadSettings()
//        {
//            using (StreamReader sr = new StreamReader(SettingsFile))
//                ActiveSettings = JsonConvert.DeserializeObject<Settings>(sr.ReadToEnd());
//        }
//
//        /// <summary>
//        /// Saves the settings to the filesystem
//        /// </summary>
//        /// <param name="settings">the new/current settings which should be saved.</param>
//        public void SaveSettings(Settings settings)
//        {
//            using (StreamWriter fs = new StreamWriter(SettingsFile))
//                fs.Write(JsonConvert.SerializeObject(settings));
//            ActiveSettings = settings;
//        }
//
//        /// <summary>
//        /// Saves the tournament
//        /// </summary>
//        /// <param name="tournament">the tournament whcich should be saved</param>
//        /// <param name="autosave">if its an autosave, the user shouldn  see a savefile-dialog</param>
//        public void SaveTournament(Tournament tournament, bool autosave = false)
//        {
//            string file = "";
//            if (autosave)
//            {
//
//            }
//            else
//            {
//                for (int i = 0; i < Settings.FileExtensions.Length / 2; i++)
//                    fileManager.AddFilter("*" + Settings.FileExtensions[i], ActiveLanguage.FileExtensionNames[i], i != 0);
//                if (fileManager.Save())
//                    file = fileManager.FileName;
//                else
//                    return;
//            }
//            if (file != "")
//            {
//                using (StreamWriter fs = new StreamWriter(file))
//                    fs.Write(JsonConvert.SerializeObject(tournament));
//
//            }
//        }
//
//        /// <summary>
//        /// loads a saved tournament
//        /// </summary>
//        /// <param name="autosave">if an autosave file should be loaded, the autosave-dialog will be shown</param>
//        /// <returns>the loaded tournament or null</returns>
//        public Tournament LoadTournament(bool autosave)
//        {
//            for (int i = 0; i < Settings.FileExtensions.Length / 2; i++)
//                fileManager.AddFilter("*" + Settings.FileExtensions[i], ActiveLanguage.FileExtensionNames[i], i != 0); if (fileManager.Open())
//            {
//                using (StreamReader sr = new StreamReader(fileManager.FileName))
//                {
//                    return JsonConvert.DeserializeObject<Tournament>(sr.ReadToEnd());
//                }
//            }
//            return null;
//        }
//
//        /// <summary>
//        /// saves the default language
//        /// </summary>
//        private void SaveLanguage()
//        {
//            if (File.Exists(Path.Combine(LanguagePath, "default.towlang")))
//                File.Delete(Path.Combine(LanguagePath, "default.towlang"));
//            using (StreamWriter fs = new StreamWriter(Path.Combine(LanguagePath, "default.towlang"), false, Encoding.UTF8))
//                fs.Write(JsonConvert.SerializeObject(new Lang()));
//
//        }
//
//        /// <summary>
//        /// loads the language which is named in the settings file
//        /// </summary>
//        private void LoadLanguage()
//        {
//            using (StreamReader sr = new StreamReader(Path.Combine(LanguagePath, ActiveSettings.ActiveLanguage + ".towlang"), Encoding.UTF8))
//            ActiveLanguage = JsonConvert.DeserializeObject<Lang>(sr.ReadToEnd());
//        }
//
//        public bool ShowMessageWithOKCancel(string text)
//        {
//            return messageManager.ShowWithOKCancel(text);
//        }
//
//    }
//}
//
