using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TXM.Core
{
	[Serializable]
	public class IO
	{
		#region private fields

		private string imgurl;
		private string languageList;
		private string langseturl;
		private string languageURL = "http://apps.piratesoftatooine.de/Languages/";
		private int imgnr = 0;
		private string imgending;

		#endregion

		#region public properties

		//Path Strings
		public string AutosavePath { get; private set; }

		public string TempPath { get; private set; }

		public string LanguagePath { get; private set; }

		public string BinLanguagePath { get; private set; }

		public string PrintFile { get; private set; }

		public string SavePath { get; private set; }

		public string TempImgPath { get; private set; }

		public IFile FileManager { get; set; }

		public IMessage MessageBox { get; set; }

		public Language ActiveLanguage { get; private set; }

		public bool AutosavePathExists {
			get {
				return Directory.Exists (AutosavePath);
			}
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TXM.Core.IO"/> class.
		/// </summary>
		/// <param name="fileManager">File manager which is used to open and save files.</param>
		/// <param name="messageBox">Message box to show hints.</param>
		public IO (IFile fileManager, IMessage messageBox)
		{
			//Initialising all Paths
			SavePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "TXM");
			AutosavePath = Path.Combine (SavePath, "Autosave");
			TempPath = Path.Combine (SavePath, "Temp");
			PrintFile = Path.Combine (TempPath, "print.html");
			LanguagePath = Path.Combine (SavePath, "Language");
			BinLanguagePath = Path.Combine (LanguagePath, "Bin");

			FileManager = fileManager;
			MessageBox = messageBox;

			langseturl = Path.Combine (SavePath, "language.txt");
			ActiveLanguage = new Language ();
			languageList = Path.Combine (TempPath, "LanguageList.txt");

			if (File.Exists (langseturl)) {
				string setFile = "";
				using (StreamReader sr = new StreamReader (langseturl)) {
					setFile = sr.ReadLine ();
				}
				ActiveLanguage = LoadLanguage (setFile, false, false);
			}
		}

		#region Saving/Loading functions

		/// <summary>
		/// Imports GOEPP files from T3 (*.gip)
		/// </summary>
		/// <returns>the imported Tournament</returns>
		public Tournament GOEPPImport ()
		{
			FileManager.AddFilter ("*.gip", "GÖPP Import Datei"); //ShouldBeTranslated
			if (FileManager.Open (ActiveLanguage.GetTranslation(StaticLanguage.Open), ActiveLanguage.GetTranslation(StaticLanguage.Cancel))) {
				try {
					List<string> gipFile = new List<string> ();
					using (StreamReader sr = new StreamReader (FileManager.FileName, Encoding.GetEncoding (28591))) {
						string line;
						while ((line = sr.ReadLine ()) != null) {
							gipFile.Add (line);
						}
					}
					//Fileformat:
					//1. Version:   v1.3.3
					//2. Name:      1. Schlacht um Tatooine
					//3. T3ID:      12484
					//4. Number:    30
					//              0     1          2     3          4         5            6                 7  8  9  10  11
					//5.+ Player:   ID  ||Forename ||Name||Nickname ||Faction ||City       ||Team            ||A||B||C||D||x
					//              7619||Dieter   ||Chri||DieChri  ||Rebellen||Leverkusen ||PGF Siegen e. V.||3|| ||1|| ||x
					//A: ArmyListGiven 3 = Yes, 0 = No
					//B: ?
					//C: Paid 1 = Yes, 0 = No
					//D: Actual T3 Rank
					//x = End
					Tournament tournament = new Tournament (Int32.Parse (gipFile [2]), gipFile [1], ActiveLanguage.GetTranslation (StaticLanguage.Bye), ActiveLanguage.GetTranslation (StaticLanguage.WonBye), ActiveLanguage.GetTranslation (StaticLanguage.Imperium), gipFile [0]);
					for (int i = 4; i < gipFile.Count; i++) {
						tournament.AddPlayer (ConvertLineToPlayer (gipFile [i]));
					}
					return tournament;
				} catch {
					ShowMessage ("Bitte eine gültige GÖPP Import Datei auswählen."); //ShouldBeTranslated
					return null;
				}
			}
			return null;
		}

		/// <summary>
		/// Converts the line to player.
		/// </summary>
		/// <returns>The converted Player.</returns>
		/// <param name="line">Line from *.gip file.</param>
		private Player ConvertLineToPlayer (string line)
		{
			string[] splitedLine = new string[11];
			int sepBegin = 0, sepEnd;
			for (int i = 0; i < 11; i++) {
				sepEnd = line.IndexOf ("|", sepBegin);
				splitedLine [i] = line.Substring (sepBegin, sepEnd - sepBegin);
				sepBegin = sepEnd + 2;
			}
			return new Player (Int32.Parse (splitedLine [0]), splitedLine [1], splitedLine [2], splitedLine [3], splitedLine [4], splitedLine [5], splitedLine [6], Int32.Parse (splitedLine [9]) == 1, Int32.Parse (splitedLine [7]) == 3);
		}

		/// <summary>
		/// Export the tournament to a *.gep file
		/// </summary>
		/// <param name="tournament">Tournament.</param>
		public void GOEPPExport (Tournament tournament)
		{
			FileManager.AddFilter ("*.gep", "GÖPP Export Datei"); //ShouldBeTranslated
			if (FileManager.Save (ActiveLanguage.GetTranslation(StaticLanguage.Save), ActiveLanguage.GetTranslation(StaticLanguage.Cancel))) {
				string file = FileManager.FileName;
				List<string> temp = new List<string> ();
				string lastname, city;
				//1. Version:   #GoePP-Exportdatei, v1.3.3 Export vom 19.08.2014||x
				//2. T3ID:      #TID-12484||x
				//3.+ Spieler:  T3ID ||Fore.||Name     ||Nick  ||Faction ||City          ||Team               ||A ||   B  ||C ||D ||E ||F ||G ||H ||I ||x
				//              22343||Chris||Chrissy  ||NiChri||Rebellen||Colonia       ||Expendable Squadron||15||131   ||32||-2||99||88||77||78||55||x
				//A: Rank
				//B: Points + ArmyRank
				//C: Points
				//D: Difference
				//E: ArmyRank
				//F: Painting
				//G: 
				//H: Fairplay + Fairplay Tournament
				//I: Other

				if (!file.EndsWith (".gep"))
					file += ".gep";
				string line = "#GoePP-Exportdatei, " + tournament.GOEPPVersion + " Export vom " + DateTime.Now.ToShortDateString (); //ShouldBeTranslated
				string sep = "||", rest = sep + 0 + sep + 0 + sep + 0 + sep + 0;
				using (StreamWriter f = new StreamWriter (file, false, Encoding.GetEncoding (28591))) {
					f.WriteLine (line + "||x");
				}
				line = "#TID-" + tournament.T3ID;
				WriteLine (file, line);
				foreach (Player xwp in tournament.Participants) {
					if (temp.Contains (xwp.Nickname))
						lastname = xwp.Name + " 1";
					else
						lastname = xwp.Name;
					if (xwp.City.Length > 20)
						city = xwp.City.Substring (0, 20);
					else
						city = xwp.City;
					line = xwp.T3ID + sep + xwp.Forename + sep + lastname + sep + xwp.Nickname + sep + xwp.Faction + sep + city + sep + xwp.Team + sep + xwp.Rank + sep + (xwp.Points + xwp.ArmyRank) + sep + xwp.Points + sep + xwp.MarginOfVictory + sep + xwp.ArmyRank + rest;
					temp.Add (xwp.Nickname);
					WriteLine (file, line);
				}
			}
		}

		/// <summary>
		/// Writes the line in the *.gep file
		/// </summary>
		/// <param name="file">Filename.</param>
		/// <param name="line">Line which should be written.</param>
		private void WriteLine (string file, string line)
		{
			using (StreamWriter f = new StreamWriter (file, true, Encoding.GetEncoding (28591))) {
				f.WriteLine (line + "||x");
			}
		}

		/// <summary>
		/// Save the specified tournament.
		/// </summary>
		/// <param name="tournament">Tournament.</param>
		/// <param name="autosave">If set to <c>true</c> autosave, it is an autosave.</param>
		/// <param name="Autosavetype">Autosaveheader.</param>
		public void Save (Tournament tournament, bool autosave, string Autosavetype = "")
		{
			string file = "";
			if (autosave) {
				file = AutosavePath;
				if (!Directory.Exists (file)) {
					Directory.CreateDirectory (file);
				}
				file += "\\Autosave_" + DateTime.Now.ToFileTime () + "_" + tournament.Name + "_" + Autosavetype + Settings.FileExtension;
			} else {
				FileManager.AddFilter ("*" + Settings.FileExtension, ActiveLanguage.GetTranslation (StaticLanguage.FileDescription));
				if (FileManager.Save (ActiveLanguage.GetTranslation(StaticLanguage.Save), ActiveLanguage.GetTranslation(StaticLanguage.Cancel))) {
					file = FileManager.FileName;
				} else
					return;
			}
			if (!file.EndsWith (Settings.FileExtension))
				file += Settings.FileExtension;

			IFormatter formatter = new BinaryFormatter ();
			using (Stream stream = new FileStream (file, FileMode.Create, FileAccess.Write, FileShare.None)) {
				formatter.Serialize (stream, tournament);
			}
		}

		/// <summary>
		/// Load the specified filename.
		/// </summary>
		/// <param name="filename">Filename.</param>
		public Tournament Load (string filename = "")
		{
			string file;
			if (filename != "") {
				file = filename;
			} else {
				FileManager.AddFilter ("*" + Settings.FileExtension, ActiveLanguage.GetTranslation (StaticLanguage.FileDescription));
				if (FileManager.Open (ActiveLanguage.GetTranslation(StaticLanguage.Open), ActiveLanguage.GetTranslation(StaticLanguage.Cancel)))
					file = FileManager.FileName;
				else
					return null;
			}
			try {
				Tournament t = null;
				IFormatter formatter = new BinaryFormatter ();
				using (Stream stream = new FileStream (file, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					t = (Tournament)formatter.Deserialize (stream);
				}
				return t;
			} catch {
				ShowMessage (ActiveLanguage.GetTranslation (StaticLanguage.ChooseValidFile));
				return null;
			}
		}

		#endregion

		#region Language handling

		/// <summary>
		/// Loads the exisiting languages
		/// </summary>
		/// <returns>A list of languages</returns>
		public List<string> GetLanguages ()
		{
			List<string> l = new List<string> ();
			if (Directory.Exists (LanguagePath)) {
				string[] invis = Directory.GetFiles (LanguagePath, ".*");
				foreach (var s in Directory.GetFiles(LanguagePath)) {
					if (!invis.Contains (s)) {
						l.Add (s.Substring (s.LastIndexOf (Path.DirectorySeparatorChar) + 1, s.Length - s.LastIndexOf ('.') + 2));
					}
				}
			}
			return l;
		}

		/// <summary>
		/// Loads the language.
		/// </summary>
		/// <returns>The language.</returns>
		/// <param name="name">Name.</param>
		/// <param name="download">If set to <c>true</c> the language will be downloaded.</param>
		/// <param name="writeLang">If set to <c>true</c> the language will be saved</param>
		public void LoadLanguage (string name, bool download, bool writeLang = true)
		{
			string fileBin = Path.Combine (BinLanguagePath, name + ".binlang");
			string file = Path.Combine (LanguagePath, name + ".lang");
			Language l = new Language ();
			IFormatter formatter = new BinaryFormatter ();

			//Download file.
			if (download) {
				if (!Directory.Exists (LanguagePath))
					Directory.CreateDirectory (LanguagePath);
				string webfile = languageURL + name + ".lang";
				WebClient wc = new WebClient ();
				wc.DownloadFile (webfile, file);
				if (File.Exists (fileBin))
					File.Delete (fileBin);
			}

			//if the bin file exists, it can be load faster than parsing
			if (File.Exists (fileBin)) {
				using (Stream stream = new FileStream (fileBin, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					l = (Language)formatter.Deserialize (stream);
				}
			}
			//parsing the language file
            else {
				List<string> langFile = new List<string> ();
				using (StreamReader sr = new StreamReader (file)) {
					string line;
					while ((line = sr.ReadLine ()) != null) {
						langFile.Add (line);
					}
				}
				l.SetTranslations (langFile);
				if (!Directory.Exists (BinLanguagePath))
					Directory.CreateDirectory (BinLanguagePath);
				using (Stream stream = new FileStream (fileBin, FileMode.Create, FileAccess.Write, FileShare.None)) {
					formatter.Serialize (stream, l);
				}
				if (writeLang) {
					using (StreamWriter f = new StreamWriter (langseturl)) {
						f.WriteLine (name);
					}
				}
			}
			ActiveLanguage = l;
		}

		/// <summary>
		/// Loads the languagelist 
		/// </summary>
		/// <returns>The web languages.</returns>
		public List<string> GetWebLanguages ()
		{
			List<string> l = new List<string> ();
			SetTempPath ();
			WebClient wc = new WebClient ();
			wc.DownloadFile (Settings.LanguageList, languageList);
			using (StreamReader sr = new StreamReader (languageList)) {
				string line;
				while ((line = sr.ReadLine ()) != null) {
					l.Add (line);
				}
			}
			return l;
		}

		/// <summary>
		/// Resets the language.
		/// </summary>
		/// <returns>The language.</returns>
		public Language ResetLanguage ()
		{
			ActiveLanguage = new Language ();
			if (File.Exists (langseturl))
				File.Delete (langseturl);
			return ActiveLanguage;
		}

		#endregion

		#region Export in BBCODE and for Print

		/// <summary>
		/// Table output in BBCODE
		/// </summary>
		/// <returns>The for forum.</returns>
		/// <param name="tournament">Tournament.</param>
		/// <param name="players">Players.</param>
		public static string TableForForum (Tournament tournament, List<Player> players)
		{
			//ShouldBeTranslated
			string output = tournament.Name + " - Tabelle - Runde " + tournament.DisplayedRound + "\n[table][tr][td]Platz[/td][td]Name[/td][td]Siege[/td][td]mod. Siege[/td][td]Unentschieden[/td][td]Niederlagen[/td][td]Punkte[/td][td]HdS[/td][td]Stärke der Gegner[/td][/tr]\n";
			for (int i = 1; i <= players.Count; i++) {
				Player p = players [i - 1];
				output += "[tr][td]" + i + "[/td][td]" + p.DisplayName + "[/td][td]" + p.Wins + "[/td][td]" + p.ModifiedWins + "[/td][td]" + p.Draws + "[/td][td]" + p.Looses + "[/td][td]" + p.Points + "[/td][td]" + p.MarginOfVictory + "[/td][td]" + p.PointsOfEnemies + "[/td][/tr]\n";
			}
			output += "[/table]\n";
			return output;
		}

		/// <summary>
		/// Pairings output for BBCODE
		/// </summary>
		/// <returns>The for forum.</returns>
		/// <param name="tournament">Tournament.</param>
		/// <param name="pairing">Pairing.</param>
		public static string PairingForForum (Tournament tournament, List<Pairing> pairing)
		{
			//ShouldBeTranslated
			string output = tournament.Name + " - Tabelle - Runde " + tournament.DisplayedRound + "\n[table][tr][td]Tisch Nr.[/td][td]Spieler 1[/td][td]Spieler 2[/td][td]Ergebnis[/td][/tr]\n";
			for (int i = 1; i <= pairing.Count; i++) {
				Pairing p = pairing [i - 1];
				output += "[tr][td]" + p.TableNr + "[/td][td]" + p.Player1Name + "[/td][td]" + p.Player2Name + "[/td][td]" + p.Player1Score + ":" + p.Player2Score + "[/td][/tr]\n";
			}
			output += "[/table]\n";
			return output;
		}

		/// <summary>
		/// Print the specified tournament.
		/// </summary>
		/// <param name="tournament">Tournament.</param>
		public void Print (Tournament tournament)
		{
			//ShouldBeTranslated
			string title = tournament.Name + " - Tabelle - Runde " + tournament.DisplayedRound; 

			List<string> print = new List<string> ();
			string temp = "<!DOCTYPE html><html><head><title>" + title + "</title></head><body><table>";
			print.Add (temp);
			temp = "<h2>" + title + "</h2></br>";
			print.Add (temp);
			temp = "<tr><td>#</td><td>Name</td><td>Punkte</td><td>S</td><td>MS</td><td>U</td><td>N</td><td>HdS</td><td>GS</td></tr>";
			print.Add (temp);
			foreach (Player p in tournament.Rounds[tournament.Rounds.Count - 1].Participants) {
				temp = "<tr><td>" + p.Rank + "</td><td>" + p.DisplayName + "</td><td>" + p.Points + "</td><td>" + p.Wins + "</td><td>" + p.ModifiedWins + "</td><td>" + p.Draws + "</td><td>" + p.Looses + "</td><td>" + p.MarginOfVictory + "</td><td>" + p.PointsOfEnemies + "</td></tr>";
				print.Add (temp);
			}
			temp = "</table></body></html>";
			print.Add (temp);
			if (!Directory.Exists (TempPath))
				Directory.CreateDirectory (TempPath);
			using (StreamWriter sw = new StreamWriter (PrintFile, false)) {
				foreach (string s in print)
					sw.WriteLine (s);
			}

			Process.Start ("file://" + PrintFile);
		}

		/// <summary>
		/// Print the specified pairing with or without the result.
		/// </summary>
		/// <param name="tournament">Tournament.</param>
		/// <param name="result">If set to <c>true</c> result will be printed too.</param>
		public void Print (Tournament tournament, bool result)
		{
			//ShouldBeTranslated
			string title;
			if (result)
				title = tournament.Name + " - Ergebnisse - Runde " + tournament.DisplayedRound;
			else
				title = tournament.Name + " - Paarungen - Runde " + tournament.DisplayedRound;
			List<string> print = new List<string> ();
			string temp = "<!DOCTYPE html><html><head><title>" + title + "</title></head><body><table>";
			print.Add (temp);
			temp = "<h2>" + title + "</h2></br>";
			print.Add (temp);
			temp = "<tr><td>Tisch-Nr</td><td>Spieler 1</td><td>Spieler2</td>";
			if (result)
				temp += "<td>Ergebnis</td>";
			temp += "</tr>";
			print.Add (temp);
			foreach (Pairing p in tournament.Rounds[tournament.Rounds.Count - 1].Pairings) {
				temp = "<tr><td>" + p.TableNr + "</td><td>" + p.Player1Name + "</td><td>" + p.Player2Name + "</td>";
				if (result)
					temp += "<td>" + p.Player1Score + ":" + p.Player2Score + "</td>";
				temp += "</tr>";
				print.Add (temp);
			}
			temp = "</table></body></html>";
			print.Add (temp);
			if (!Directory.Exists (TempPath))
				Directory.CreateDirectory (TempPath);
			using (StreamWriter sw = new StreamWriter (PrintFile, false)) {
				foreach (string s in print)
					sw.WriteLine (s);
			}

			Process.Start ("file://" + PrintFile);
		}

		/// <summary>
		/// Prints the score sheet.
		/// </summary>
		/// <param name="tournament">Tournament.</param>
		public void PrintScoreSheet (Tournament tournament)
		{
			//ShouldBeTranslated
			string title = tournament.Name + " - Pairings - Round " + tournament.DisplayedRound;
			List<string> print = new List<string> ();
			string temp = "<!DOCTYPE html><html><head><title>" + title + "</title></head><body>";
			title = "Round " + tournament.DisplayedRound + " - Table ";
			foreach (Pairing p in tournament.Rounds[tournament.Rounds.Count - 1].Pairings) {
				temp = "<table width=100%><tr><td><h4>" + title + p.TableNr + "</h4></td><td><h4>" + p.Player1Name + "</h4></td><td><h4>" + p.Player2Name + "</h4></td></tr>";
				print.Add (temp);
				temp = "<tr><td></td><td>Points destroyed    _________</td><td>Points destroyed    _________</td></tr></table><hr/>";
				print.Add (temp);
			}
			temp = "</body></html>";
			print.Add (temp);
			if (!Directory.Exists (TempPath))
				Directory.CreateDirectory (TempPath);
			using (StreamWriter sw = new StreamWriter (PrintFile, false, Encoding.UTF8)) {
				foreach (string s in print)
					sw.WriteLine (s);
			}

			Process.Start ("file://" + PrintFile);
		}

		#endregion

		#region Show Warnings

		public bool ShowMessageWithOKCancel (string text)
		{
			return MessageBox.ShowWithOKCancel (text);
		}

		public void ShowMessage (string text)
		{
			MessageBox.Show (text);
		}

		#endregion

		#region Autosave Handling

		/// <summary>
		/// Opens the Autosave folder in Finder/Explorer etc.
		/// </summary>
		public void ShowAutosaveFolder ()
		{
			if (AutosavePathExists)
				Process.Start ("file://" + AutosavePath);
			else
				ShowMessage ("Es exisitiert kein Autosave Ordner."); //ShouldBeTranslated
		}

		/// <summary>
		/// Deletes the Autosavepath, after Confirmation
		/// </summary>
		public void DeleteAutosaveFolder ()
		{
			if (AutosavePathExists) {
				if (ShowMessageWithOKCancel ("Are you sure to delete the Autosavefolder?")) { //ShouldBeTranslated
					Directory.Delete (AutosavePath, true);
					ShowMessage ("Autosave Ordner wurde gelöscht."); //ShouldBeTranslated
				}
			} else
				ShowMessage ("Es exisitiert kein Autosave Ordner."); //ShouldBeTranslated
		}

		/// <summary>
		/// Gets the autosave files.
		/// </summary>
		/// <returns>The autosave files.</returns>
		public string[] GetAutosaveFiles ()
		{
			return Directory.GetFiles (AutosavePath, "*" + Settings.FileExtension);
		}

		#endregion

		public bool GetColor ()
		{
			//TODO: Umarbeiten in neue Settingsdatei
//            if (File.Exists(TextColorFile))
//            {
//                string line;
//                using (StreamReader sr = new StreamReader(TextColorFile))
//                {
//                    line = sr.ReadLine();
//                }
//                return line == "White";
//            }
//            else
			return false;
		}

		public void WriteColor (bool whiteText)
		{
			//TODO: Umarbeiten in neue Settingsdatei
//            using (StreamWriter f = new StreamWriter(TextColorFile))
//            {
//                f.WriteLine(whiteText ? "White" : "Black");
//            }
		}

		public string GetImagePath ()
		{
			//TODO: Optimieren
			string img = Path.Combine (SavePath, "background.png");
			imgurl = null;
			if (File.Exists (img)) {
				imgurl = img;
				imgending = ".png";
			}
			img = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "TXM", "background.jpg");
			if (File.Exists (img)) {
				imgurl = img;
				imgending = ".jpg";
			}
			img = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "TXM", "background.jpeg");
			if (File.Exists (img)) {
				imgurl = img;
				imgending = ".jpeg";
			}
			img = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "TXM", "background.tif");
			if (File.Exists (img)) {
				imgurl = img;
				imgending = ".tif";
			}
			img = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "TXM", "background.tiff");
			if (File.Exists (img)) {
				imgurl = img;
				imgending = ".tiff";
			}
			return imgurl;
		}

		#region Image handling for background

		public void CopyImage ()
		{
			if (!Directory.Exists (TempPath))
				Directory.CreateDirectory (TempPath);
			TempImgPath = Path.Combine (TempPath, "background" + imgnr + imgending);
			imgnr++;
			File.Copy (imgurl, TempImgPath, true);
		}

		public void NewImage ()
		{
			FileManager.AddFilter ("*.jpg;*.jpeg;*.png;*.tif;*.tiff", "Alle Bilderformate"); //ShouldBeTranslated
			if (FileManager.Open (ActiveLanguage.GetTranslation(StaticLanguage.Open), ActiveLanguage.GetTranslation(StaticLanguage.Cancel))) {
				string imageSrc = FileManager.FileName;
				string newImageSrc = Path.Combine (SavePath, "background" + imageSrc.Remove (0, imageSrc.LastIndexOf (".")));
				File.Copy (imageSrc, newImageSrc, true);
				imgurl = newImageSrc;
				CopyImage ();
			}
		}

		#endregion

		/// <summary>
		/// Sets the temp path.
		/// </summary>
		private void SetTempPath ()
		{
			if (!Directory.Exists (TempPath))
				Directory.CreateDirectory (TempPath);
		}
	}
}
