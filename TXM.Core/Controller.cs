using System;

namespace TXM.Core
{
	public class Controller
	{
		public Tournament ActiveTournament { get; private set;}
		public Timer ActiveTimer { get; private set;}
		public Language ActiveLanguage {get{ return activeIO.ActiveLanguage; }}
		public bool TournamentStarted { get; private set;}
		public bool LastRound { get; set;}

		private IO activeIO;
		private Settings activeSettings;

		public Controller (IFile fileManager, IMessage messageManager)
		{
			activeIO = new IO (fileManager, messageManager);
			ActiveTimer = new Timer ();
			//TODO: Einstellungen lesen
		}

		public void LoadLanguage(string languageName, bool download)
		{
			activeIO.LoadLanguage(languageName, download);
		}
	}
}

