using System;

using AppKit;
using Foundation;

using TXM.Core;

namespace TXM.Mac
{
	public partial class ViewController : NSViewController
	{
        private TournamentController tournamentController;

		public static AppDelegate App
		{
			get { return (AppDelegate)NSApplication.SharedApplication.Delegate; }
		}

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            // Do any additional setup after loading the view.

            tournamentController = new TournamentController(new IO(new MacFile(), new MacMessage()));

            App.MainViewController = this;
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		public void Open(bool autosave = false)
		{
			//tournamentController.Load(new AutosaveDialog(), autosave);
            tournamentController.Load(null, autosave);
			if (tournamentController.ActiveTournament != null)
			{
				/*ComboBoxRounds.Items.Clear();
				if (tournamentController.ActiveTournament.Rounds != null)
				{
					for (int i = 1; i <= tournamentController.ActiveTournament.Rounds.Count; i++)
						AddRoundButton(i);
					if (tournamentController.ActiveTournament.Rounds != null && tournamentController.ActiveTournament.Rounds.Count > 0)
						DataGridPairing.ItemsSource = tournamentController.ActiveTournament.Rounds[tournamentController.ActiveTournament.Rounds.Count - 1].Pairings;
					if (tournamentController.ActiveTournament.FirstRound && (tournamentController.ActiveTournament.Rounds == null || tournamentController.ActiveTournament.Rounds.Count == 0))
					{
						SetGUIState(true);
					}
					else
					{
						SetGUIState(false, true);
					}
					ButtonGetResults.Content = tournamentController.ActiveTournament.ButtonGetResultsText;
					ButtonGetResults.IsEnabled = true;
					ButtonCut.IsEnabled = tournamentController.ActiveTournament.ButtonCutState == true;
					tournamentController.ActiveTournament.Sort();
					RefreshDataGridPlayer(tournamentController.ActiveTournament.Participants);
					if (tournamentController.ActiveTournament.Pairings != null)
						RefreshDataGridPairings(tournamentController.ActiveTournament.Pairings);

					InitDataGridPlayer();
					InitDataGridPairing();
					ButtonGetResults.ToolTip = ButtonGetResults.Content.ToString();
				}*/
			}
		}

	}
}
