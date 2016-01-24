using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
	public sealed class Settings
	{
		//TODO: Textfarbe speichern
		//TODO: Hintergrundbild speichern
		//TODO: installierte Sprachen speichern

		public string ActiveLanguage { get; set; }

		#region Constants

		private static string fileextension = ".txmb2";
		private static string languagesFile = "http://apps.piratesoftatooine.de/Languages/Languages.txt";
		private static string languageExtension = ".txmlang";

		#endregion

		public Settings ()
		{
			ActiveLanguage = "default";
		}

		#region Properties

		public static string LanguageList {
			get {
				return languagesFile;
			}
		}

		public static string FileExtension {
			get {
				return fileextension;
			}
		}

		public static string LanguageExtension {
			get {
				return languageExtension;
			}
		}

		#endregion

	}
}