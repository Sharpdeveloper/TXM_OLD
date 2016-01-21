namespace TOWare.Core
{
    public sealed class Settings
    {
        #region Constants
        private static string[] fileextensions = new string[] { ".towt" };
        private static string languagesFile = "http://apps.piratesoftatooine.de/Languages/Languages.txt";
        private static string languageExtension = ".towlang";     
        #endregion

        public Settings()
        {
            ActiveLanguage = "default";
        }

        #region Properties
        public static string LanguageList
        {
            get
            {
                return languagesFile;
            }
        }

        public static string[] FileExtensions
        {
            get
            {
                return fileextensions;
            }
        }

        public static string LanguageExtension
        {
            get
            {
                return languageExtension;
            }
        }
        #endregion

        public string ActiveLanguage { get; set; }
    }
}
