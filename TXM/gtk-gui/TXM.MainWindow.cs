
// This file has been generated by the GUI designer. Do not modify.
namespace TXM
{
	public partial class MainWindow
	{
		private global::Gtk.UIManager UIManager;
		
		private global::Gtk.Action MenuFile;
		
		private global::Gtk.Action MenuNew;
		
		private global::Gtk.Action MenuNewTournament;
		
		private global::Gtk.Action MenuNewPlayer;
		
		private global::Gtk.Action MenuLoad;
		
		private global::Gtk.Action MenuSaveTournament;
		
		private global::Gtk.Action MenuLoadTournament;
		
		private global::Gtk.Action LoadAutosaveAction;
		
		private global::Gtk.Action LoadAutosavefileAction;
		
		private global::Gtk.Action ShowAutosaveAction;
		
		private global::Gtk.Action MenuAutosavefile;
		
		private global::Gtk.Action MenuImportExport;
		
		private global::Gtk.Action MenuImportT3;
		
		private global::Gtk.Action MenuExportT3;
		
		private global::Gtk.Action MenuExportBBCode;
		
		private global::Gtk.Action MenuPrint;
		
		private global::Gtk.Action MenuPrintTable;
		
		private global::Gtk.Action MenuPrintPairings;
		
		private global::Gtk.Action PrintPairingsWithResultsAction;
		
		private global::Gtk.Action MenuQuit;
		
		private global::Gtk.Action MenuPrintScoresheet;
		
		private global::Gtk.Action MenuTournament;
		
		private global::Gtk.Action MenuChangePairings;
		
		private global::Gtk.Action MenuResetLastResults;
		
		private global::Gtk.Action MenuTournamentSettings;
		
		private global::Gtk.Action MenuChangePlayer;
		
		private global::Gtk.Action MenuRemovePlayer;
		
		private global::Gtk.Action MenuTools;
		
		private global::Gtk.Action MenuTimer;
		
		private global::Gtk.Action MenuRandomizer;
		
		private global::Gtk.Action MenuShow;
		
		private global::Gtk.Action MenuLanguage;
		
		private global::Gtk.Action MenuLoadLanguages;
		
		private global::Gtk.Action MenuDefaultLanguage;
		
		private global::Gtk.Action MenuHelp;
		
		private global::Gtk.Action MenuAbout;
		
		private global::Gtk.Action MenuShowTime;
		
		private global::Gtk.Action MenuStartTimer;
		
		private global::Gtk.Action MenuPauseTimer;
		
		private global::Gtk.Action MenuResetTimer;
		
		private global::Gtk.Action MenuSetTime;
		
		private global::Gtk.Action Menu30Minutes;
		
		private global::Gtk.Action Menu45Minutes;
		
		private global::Gtk.Action Menu60Minutes;
		
		private global::Gtk.Action Menu75Minutes;
		
		private global::Gtk.Action Menu90Minutes;
		
		private global::Gtk.Action MenuVariable;
		
		private global::Gtk.Action saveAction;
		
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.MenuBar menubar1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Button buttonSave;
		
		private global::Gtk.Button buttonNewPlayer;
		
		private global::Gtk.Button buttonChangePairings;
		
		private global::Gtk.Button buttonChangePlayer;
		
		private global::Gtk.Button buttonReset;
		
		private global::Gtk.HPaned hpaned1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.Button buttonTournament;
		
		private global::Gtk.VButtonBox vbuttonbox1;
		
		private global::Gtk.VPaned vpaned1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.NodeView dataGridPlayer;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gtk.NodeView dataGridPairings;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget TXM.MainWindow
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.MenuFile = new global::Gtk.Action ("MenuFile", global::Mono.Unix.Catalog.GetString ("File"), null, null);
			this.MenuFile.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
			w1.Add (this.MenuFile, null);
			this.MenuNew = new global::Gtk.Action ("MenuNew", global::Mono.Unix.Catalog.GetString ("New"), null, null);
			this.MenuNew.ShortLabel = global::Mono.Unix.Catalog.GetString ("New");
			w1.Add (this.MenuNew, null);
			this.MenuNewTournament = new global::Gtk.Action ("MenuNewTournament", global::Mono.Unix.Catalog.GetString ("New Tournament"), null, null);
			this.MenuNewTournament.ShortLabel = global::Mono.Unix.Catalog.GetString ("New Tournament");
			w1.Add (this.MenuNewTournament, null);
			this.MenuNewPlayer = new global::Gtk.Action ("MenuNewPlayer", global::Mono.Unix.Catalog.GetString ("New Player"), null, null);
			this.MenuNewPlayer.ShortLabel = global::Mono.Unix.Catalog.GetString ("New Player");
			w1.Add (this.MenuNewPlayer, null);
			this.MenuLoad = new global::Gtk.Action ("MenuLoad", global::Mono.Unix.Catalog.GetString ("Load"), null, null);
			this.MenuLoad.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load");
			w1.Add (this.MenuLoad, null);
			this.MenuSaveTournament = new global::Gtk.Action ("MenuSaveTournament", global::Mono.Unix.Catalog.GetString ("Save Tournament"), null, null);
			this.MenuSaveTournament.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save Tournament");
			w1.Add (this.MenuSaveTournament, null);
			this.MenuLoadTournament = new global::Gtk.Action ("MenuLoadTournament", global::Mono.Unix.Catalog.GetString ("Load Tournament"), null, null);
			this.MenuLoadTournament.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Tournament");
			w1.Add (this.MenuLoadTournament, null);
			this.LoadAutosaveAction = new global::Gtk.Action ("LoadAutosaveAction", global::Mono.Unix.Catalog.GetString ("Load Autosave"), null, null);
			this.LoadAutosaveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Autosave");
			w1.Add (this.LoadAutosaveAction, null);
			this.LoadAutosavefileAction = new global::Gtk.Action ("LoadAutosavefileAction", global::Mono.Unix.Catalog.GetString ("Load Autosavefile"), null, null);
			this.LoadAutosavefileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Autosavefile");
			w1.Add (this.LoadAutosavefileAction, null);
			this.ShowAutosaveAction = new global::Gtk.Action ("ShowAutosaveAction", global::Mono.Unix.Catalog.GetString ("Show Autosave"), null, null);
			this.ShowAutosaveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Show Autosave");
			w1.Add (this.ShowAutosaveAction, null);
			this.MenuAutosavefile = new global::Gtk.Action ("MenuAutosavefile", global::Mono.Unix.Catalog.GetString ("Load Autosavefile"), null, null);
			this.MenuAutosavefile.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Autosavefile");
			w1.Add (this.MenuAutosavefile, null);
			this.MenuImportExport = new global::Gtk.Action ("MenuImportExport", global::Mono.Unix.Catalog.GetString ("Import / Export"), null, null);
			this.MenuImportExport.ShortLabel = global::Mono.Unix.Catalog.GetString ("Import / Export");
			w1.Add (this.MenuImportExport, null);
			this.MenuImportT3 = new global::Gtk.Action ("MenuImportT3", global::Mono.Unix.Catalog.GetString ("Import from T3"), null, null);
			this.MenuImportT3.ShortLabel = global::Mono.Unix.Catalog.GetString ("Import from T3");
			w1.Add (this.MenuImportT3, null);
			this.MenuExportT3 = new global::Gtk.Action ("MenuExportT3", global::Mono.Unix.Catalog.GetString ("Export to T3"), null, null);
			this.MenuExportT3.ShortLabel = global::Mono.Unix.Catalog.GetString ("Export to T3");
			w1.Add (this.MenuExportT3, null);
			this.MenuExportBBCode = new global::Gtk.Action ("MenuExportBBCode", global::Mono.Unix.Catalog.GetString ("Export to BBCode"), null, null);
			this.MenuExportBBCode.ShortLabel = global::Mono.Unix.Catalog.GetString ("Export to BBCode");
			w1.Add (this.MenuExportBBCode, null);
			this.MenuPrint = new global::Gtk.Action ("MenuPrint", global::Mono.Unix.Catalog.GetString ("Print"), null, null);
			this.MenuPrint.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print");
			w1.Add (this.MenuPrint, null);
			this.MenuPrintTable = new global::Gtk.Action ("MenuPrintTable", global::Mono.Unix.Catalog.GetString ("Print Table"), null, null);
			this.MenuPrintTable.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print Table");
			w1.Add (this.MenuPrintTable, null);
			this.MenuPrintPairings = new global::Gtk.Action ("MenuPrintPairings", global::Mono.Unix.Catalog.GetString ("Print Pairings"), null, null);
			this.MenuPrintPairings.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print Pairings");
			w1.Add (this.MenuPrintPairings, null);
			this.PrintPairingsWithResultsAction = new global::Gtk.Action ("PrintPairingsWithResultsAction", global::Mono.Unix.Catalog.GetString ("Print Pairings with Results"), null, null);
			this.PrintPairingsWithResultsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print Pairings with results");
			w1.Add (this.PrintPairingsWithResultsAction, null);
			this.MenuQuit = new global::Gtk.Action ("MenuQuit", global::Mono.Unix.Catalog.GetString ("Quit"), null, null);
			this.MenuQuit.ShortLabel = global::Mono.Unix.Catalog.GetString ("Quit");
			w1.Add (this.MenuQuit, null);
			this.MenuPrintScoresheet = new global::Gtk.Action ("MenuPrintScoresheet", global::Mono.Unix.Catalog.GetString ("Print Scoresheet"), null, null);
			this.MenuPrintScoresheet.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print Scoresheet");
			w1.Add (this.MenuPrintScoresheet, null);
			this.MenuTournament = new global::Gtk.Action ("MenuTournament", global::Mono.Unix.Catalog.GetString ("Tournament"), null, null);
			this.MenuTournament.ShortLabel = global::Mono.Unix.Catalog.GetString ("Tournament");
			w1.Add (this.MenuTournament, null);
			this.MenuChangePairings = new global::Gtk.Action ("MenuChangePairings", global::Mono.Unix.Catalog.GetString ("Change Pairings"), null, null);
			this.MenuChangePairings.ShortLabel = global::Mono.Unix.Catalog.GetString ("Change Pairings");
			w1.Add (this.MenuChangePairings, null);
			this.MenuResetLastResults = new global::Gtk.Action ("MenuResetLastResults", global::Mono.Unix.Catalog.GetString ("Reset Last Results"), null, null);
			this.MenuResetLastResults.ShortLabel = global::Mono.Unix.Catalog.GetString ("Reset Last Results");
			w1.Add (this.MenuResetLastResults, null);
			this.MenuTournamentSettings = new global::Gtk.Action ("MenuTournamentSettings", global::Mono.Unix.Catalog.GetString ("Tournament Settings"), null, null);
			this.MenuTournamentSettings.ShortLabel = global::Mono.Unix.Catalog.GetString ("Tournament Settings");
			w1.Add (this.MenuTournamentSettings, null);
			this.MenuChangePlayer = new global::Gtk.Action ("MenuChangePlayer", global::Mono.Unix.Catalog.GetString ("Change Player"), null, null);
			this.MenuChangePlayer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Change Player");
			w1.Add (this.MenuChangePlayer, null);
			this.MenuRemovePlayer = new global::Gtk.Action ("MenuRemovePlayer", global::Mono.Unix.Catalog.GetString ("Remove Player"), null, null);
			this.MenuRemovePlayer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove Player");
			w1.Add (this.MenuRemovePlayer, null);
			this.MenuTools = new global::Gtk.Action ("MenuTools", global::Mono.Unix.Catalog.GetString ("Tools"), null, null);
			this.MenuTools.ShortLabel = global::Mono.Unix.Catalog.GetString ("Tools");
			w1.Add (this.MenuTools, null);
			this.MenuTimer = new global::Gtk.Action ("MenuTimer", global::Mono.Unix.Catalog.GetString ("Timer"), null, null);
			this.MenuTimer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Timer");
			w1.Add (this.MenuTimer, null);
			this.MenuRandomizer = new global::Gtk.Action ("MenuRandomizer", global::Mono.Unix.Catalog.GetString ("Randomizer"), null, null);
			this.MenuRandomizer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Randomizer");
			w1.Add (this.MenuRandomizer, null);
			this.MenuShow = new global::Gtk.Action ("MenuShow", global::Mono.Unix.Catalog.GetString ("Show Table & Pairings"), null, null);
			this.MenuShow.ShortLabel = global::Mono.Unix.Catalog.GetString ("Show Table & Pairings");
			w1.Add (this.MenuShow, null);
			this.MenuLanguage = new global::Gtk.Action ("MenuLanguage", global::Mono.Unix.Catalog.GetString ("Language"), null, null);
			this.MenuLanguage.ShortLabel = global::Mono.Unix.Catalog.GetString ("Language");
			w1.Add (this.MenuLanguage, null);
			this.MenuLoadLanguages = new global::Gtk.Action ("MenuLoadLanguages", global::Mono.Unix.Catalog.GetString ("Load Languages"), null, null);
			this.MenuLoadLanguages.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Languages");
			w1.Add (this.MenuLoadLanguages, null);
			this.MenuDefaultLanguage = new global::Gtk.Action ("MenuDefaultLanguage", global::Mono.Unix.Catalog.GetString ("Default Language (english)"), null, null);
			this.MenuDefaultLanguage.ShortLabel = global::Mono.Unix.Catalog.GetString ("Default Language (english)");
			w1.Add (this.MenuDefaultLanguage, null);
			this.MenuHelp = new global::Gtk.Action ("MenuHelp", global::Mono.Unix.Catalog.GetString ("Help"), null, null);
			this.MenuHelp.ShortLabel = global::Mono.Unix.Catalog.GetString ("Help");
			w1.Add (this.MenuHelp, null);
			this.MenuAbout = new global::Gtk.Action ("MenuAbout", global::Mono.Unix.Catalog.GetString ("About TXM"), null, null);
			this.MenuAbout.ShortLabel = global::Mono.Unix.Catalog.GetString ("About TXM");
			w1.Add (this.MenuAbout, null);
			this.MenuShowTime = new global::Gtk.Action ("MenuShowTime", global::Mono.Unix.Catalog.GetString ("Shows Time for TO"), null, null);
			this.MenuShowTime.ShortLabel = global::Mono.Unix.Catalog.GetString ("Shows Time for TO");
			w1.Add (this.MenuShowTime, null);
			this.MenuStartTimer = new global::Gtk.Action ("MenuStartTimer", global::Mono.Unix.Catalog.GetString ("Start Timer"), null, null);
			this.MenuStartTimer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Start Timer");
			w1.Add (this.MenuStartTimer, null);
			this.MenuPauseTimer = new global::Gtk.Action ("MenuPauseTimer", global::Mono.Unix.Catalog.GetString ("Pause Timer"), null, null);
			this.MenuPauseTimer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Pause Timer");
			w1.Add (this.MenuPauseTimer, null);
			this.MenuResetTimer = new global::Gtk.Action ("MenuResetTimer", global::Mono.Unix.Catalog.GetString ("Reset Timer"), null, null);
			this.MenuResetTimer.ShortLabel = global::Mono.Unix.Catalog.GetString ("Reset Timer");
			w1.Add (this.MenuResetTimer, null);
			this.MenuSetTime = new global::Gtk.Action ("MenuSetTime", global::Mono.Unix.Catalog.GetString ("Set Time"), null, null);
			this.MenuSetTime.ShortLabel = global::Mono.Unix.Catalog.GetString ("Set Time");
			w1.Add (this.MenuSetTime, null);
			this.Menu30Minutes = new global::Gtk.Action ("Menu30Minutes", global::Mono.Unix.Catalog.GetString ("30 Minutes"), null, null);
			this.Menu30Minutes.ShortLabel = global::Mono.Unix.Catalog.GetString ("30 Minutes");
			w1.Add (this.Menu30Minutes, null);
			this.Menu45Minutes = new global::Gtk.Action ("Menu45Minutes", global::Mono.Unix.Catalog.GetString ("45 Minutes"), null, null);
			this.Menu45Minutes.ShortLabel = global::Mono.Unix.Catalog.GetString ("45 Minutes");
			w1.Add (this.Menu45Minutes, null);
			this.Menu60Minutes = new global::Gtk.Action ("Menu60Minutes", global::Mono.Unix.Catalog.GetString ("60 Minutes"), null, null);
			this.Menu60Minutes.ShortLabel = global::Mono.Unix.Catalog.GetString ("60 Minutes");
			w1.Add (this.Menu60Minutes, null);
			this.Menu75Minutes = new global::Gtk.Action ("Menu75Minutes", global::Mono.Unix.Catalog.GetString ("75 Minutes"), null, null);
			this.Menu75Minutes.ShortLabel = global::Mono.Unix.Catalog.GetString ("75 Minutes");
			w1.Add (this.Menu75Minutes, null);
			this.Menu90Minutes = new global::Gtk.Action ("Menu90Minutes", global::Mono.Unix.Catalog.GetString ("90 Minutes"), null, null);
			this.Menu90Minutes.ShortLabel = global::Mono.Unix.Catalog.GetString ("90 Minutes");
			w1.Add (this.Menu90Minutes, null);
			this.MenuVariable = new global::Gtk.Action ("MenuVariable", global::Mono.Unix.Catalog.GetString ("Variable"), null, null);
			this.MenuVariable.ShortLabel = global::Mono.Unix.Catalog.GetString ("Variable");
			w1.Add (this.MenuVariable, null);
			this.saveAction = new global::Gtk.Action ("saveAction", null, null, "gtk-save");
			w1.Add (this.saveAction, null);
			this.UIManager.InsertActionGroup (w1, 0);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.Name = "TXM.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child TXM.MainWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			// Container child vbox1.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='MenuFile' action='MenuFile'><menu name='MenuNew' action='MenuNew'><menuitem name='MenuNewTournament' action='MenuNewTournament'/><menuitem name='MenuNewPlayer' action='MenuNewPlayer'/></menu><menu name='MenuLoad' action='MenuLoad'><menuitem name='MenuLoadTournament' action='MenuLoadTournament'/><menuitem name='MenuAutosavefile' action='MenuAutosavefile'/></menu><menuitem name='MenuSaveTournament' action='MenuSaveTournament'/><menu name='MenuImportExport' action='MenuImportExport'><menuitem name='MenuImportT3' action='MenuImportT3'/><menuitem name='MenuExportT3' action='MenuExportT3'/><menuitem name='MenuExportBBCode' action='MenuExportBBCode'/></menu><menu name='MenuPrint' action='MenuPrint'><menuitem name='MenuPrintTable' action='MenuPrintTable'/><menuitem name='MenuPrintPairings' action='MenuPrintPairings'/><menuitem name='PrintPairingsWithResultsAction' action='PrintPairingsWithResultsAction'/><menuitem name='MenuPrintScoresheet' action='MenuPrintScoresheet'/></menu><menuitem name='MenuQuit' action='MenuQuit'/></menu><menu name='MenuTournament' action='MenuTournament'><menuitem name='MenuChangePairings' action='MenuChangePairings'/><menuitem name='MenuResetLastResults' action='MenuResetLastResults'/><menuitem name='MenuTournamentSettings' action='MenuTournamentSettings'/><menuitem name='MenuChangePlayer' action='MenuChangePlayer'/><menuitem name='MenuRemovePlayer' action='MenuRemovePlayer'/><menuitem name='MenuShow' action='MenuShow'/></menu><menu name='MenuTools' action='MenuTools'><menuitem name='MenuTimer' action='MenuTimer'/><menuitem name='MenuRandomizer' action='MenuRandomizer'/><menu name='MenuLanguage' action='MenuLanguage'><menuitem name='MenuLoadLanguages' action='MenuLoadLanguages'/><menuitem name='MenuDefaultLanguage' action='MenuDefaultLanguage'/></menu></menu><menu name='MenuHelp' action='MenuHelp'><menuitem name='MenuAbout' action='MenuAbout'/></menu><menu name='MenuShowTime' action='MenuShowTime'><menuitem name='MenuStartTimer' action='MenuStartTimer'/><menuitem name='MenuPauseTimer' action='MenuPauseTimer'/><menuitem name='MenuResetTimer' action='MenuResetTimer'/><menu name='MenuSetTime' action='MenuSetTime'><menuitem name='Menu30Minutes' action='Menu30Minutes'/><menuitem name='Menu45Minutes' action='Menu45Minutes'/><menuitem name='Menu60Minutes' action='Menu60Minutes'/><menuitem name='Menu75Minutes' action='Menu75Minutes'/><menuitem name='Menu90Minutes' action='Menu90Minutes'/><menuitem name='MenuVariable' action='MenuVariable'/></menu></menu></menubar></ui>");
			this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
			this.menubar1.Name = "menubar1";
			this.vbox1.Add (this.menubar1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString ("Save");
			this.hbox1.Add (this.buttonSave);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonSave]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonNewPlayer = new global::Gtk.Button ();
			this.buttonNewPlayer.CanFocus = true;
			this.buttonNewPlayer.Name = "buttonNewPlayer";
			this.buttonNewPlayer.UseUnderline = true;
			this.buttonNewPlayer.Label = global::Mono.Unix.Catalog.GetString ("New Player");
			this.hbox1.Add (this.buttonNewPlayer);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonNewPlayer]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonChangePairings = new global::Gtk.Button ();
			this.buttonChangePairings.CanFocus = true;
			this.buttonChangePairings.Name = "buttonChangePairings";
			this.buttonChangePairings.UseUnderline = true;
			this.buttonChangePairings.Label = global::Mono.Unix.Catalog.GetString ("Change Pairings");
			this.hbox1.Add (this.buttonChangePairings);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonChangePairings]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonChangePlayer = new global::Gtk.Button ();
			this.buttonChangePlayer.CanFocus = true;
			this.buttonChangePlayer.Name = "buttonChangePlayer";
			this.buttonChangePlayer.UseUnderline = true;
			this.buttonChangePlayer.Label = global::Mono.Unix.Catalog.GetString ("Change Player");
			this.hbox1.Add (this.buttonChangePlayer);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonChangePlayer]));
			w6.Position = 3;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonReset = new global::Gtk.Button ();
			this.buttonReset.CanFocus = true;
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.UseUnderline = true;
			this.buttonReset.Label = global::Mono.Unix.Catalog.GetString ("Reset Last Results");
			this.hbox1.Add (this.buttonReset);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonReset]));
			w7.Position = 4;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hpaned1 = new global::Gtk.HPaned ();
			this.hpaned1.CanFocus = true;
			this.hpaned1.Name = "hpaned1";
			this.hpaned1.Position = 127;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonTournament = new global::Gtk.Button ();
			this.buttonTournament.CanFocus = true;
			this.buttonTournament.Name = "buttonTournament";
			this.buttonTournament.UseUnderline = true;
			this.buttonTournament.Label = global::Mono.Unix.Catalog.GetString ("Start Tournement");
			this.vbox2.Add (this.buttonTournament);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.buttonTournament]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.vbuttonbox1 = new global::Gtk.VButtonBox ();
			this.vbuttonbox1.Name = "vbuttonbox1";
			this.vbox2.Add (this.vbuttonbox1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.vbuttonbox1]));
			w10.Position = 1;
			this.hpaned1.Add (this.vbox2);
			global::Gtk.Paned.PanedChild w11 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.vbox2]));
			w11.Resize = false;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.vpaned1 = new global::Gtk.VPaned ();
			this.vpaned1.CanFocus = true;
			this.vpaned1.Name = "vpaned1";
			this.vpaned1.Position = 281;
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.dataGridPlayer = new global::Gtk.NodeView ();
			this.dataGridPlayer.CanFocus = true;
			this.dataGridPlayer.Name = "dataGridPlayer";
			this.GtkScrolledWindow.Add (this.dataGridPlayer);
			this.vpaned1.Add (this.GtkScrolledWindow);
			global::Gtk.Paned.PanedChild w13 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.GtkScrolledWindow]));
			w13.Resize = false;
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.dataGridPairings = new global::Gtk.NodeView ();
			this.dataGridPairings.CanFocus = true;
			this.dataGridPairings.Name = "dataGridPairings";
			this.GtkScrolledWindow1.Add (this.dataGridPairings);
			this.vpaned1.Add (this.GtkScrolledWindow1);
			this.hpaned1.Add (this.vpaned1);
			this.vbox1.Add (this.hpaned1);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hpaned1]));
			w17.Position = 2;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 785;
			this.DefaultHeight = 526;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.MenuNewTournament.Activated += new global::System.EventHandler (this.NewTournament_Click);
			this.MenuNewPlayer.Activated += new global::System.EventHandler (this.NewPlayer_Click);
			this.MenuSaveTournament.Activated += new global::System.EventHandler (this.SaveTournament_Click);
			this.MenuLoadTournament.Activated += new global::System.EventHandler (this.LoadTournament_Click);
			this.MenuAutosavefile.Activated += new global::System.EventHandler (this.LoadAutosavefile_Click);
			this.MenuImportT3.Activated += new global::System.EventHandler (this.ImportT3_Click);
			this.MenuExportT3.Activated += new global::System.EventHandler (this.ExportT3_Click);
			this.MenuExportBBCode.Activated += new global::System.EventHandler (this.ExportBBCode_Click);
			this.MenuPrintTable.Activated += new global::System.EventHandler (this.PrintTable_Click);
			this.MenuPrintPairings.Activated += new global::System.EventHandler (this.PrintPairings_Click);
			this.PrintPairingsWithResultsAction.Activated += new global::System.EventHandler (this.PrintResults_Click);
			this.MenuQuit.Activated += new global::System.EventHandler (this.Quit_Click);
			this.MenuPrintScoresheet.Activated += new global::System.EventHandler (this.PrintScoresheet_Click);
			this.MenuChangePairings.Activated += new global::System.EventHandler (this.ChangePairings_Click);
			this.MenuResetLastResults.Activated += new global::System.EventHandler (this.ResetLastResults_Click);
			this.MenuTournamentSettings.Activated += new global::System.EventHandler (this.TournamentSettings_Click);
			this.MenuChangePlayer.Activated += new global::System.EventHandler (this.ChangePlayer_Click);
			this.MenuRemovePlayer.Activated += new global::System.EventHandler (this.RemovePlayer_Click);
			this.MenuTimer.Activated += new global::System.EventHandler (this.Timer_Click);
			this.MenuRandomizer.Activated += new global::System.EventHandler (this.Randomizer_Click);
			this.MenuShow.Activated += new global::System.EventHandler (this.Show_Click);
			this.MenuLoadLanguages.Activated += new global::System.EventHandler (this.LoadLanguage_Click);
			this.MenuDefaultLanguage.Activated += new global::System.EventHandler (this.DefaultLanguage_Click);
			this.MenuAbout.Activated += new global::System.EventHandler (this.About_Click);
			this.MenuStartTimer.Activated += new global::System.EventHandler (this.StartTimer_Click);
			this.MenuPauseTimer.Activated += new global::System.EventHandler (this.PauseTimer_Click);
			this.MenuResetTimer.Activated += new global::System.EventHandler (this.ResetTimer_Click);
			this.Menu30Minutes.Activated += new global::System.EventHandler (this.M30_Click);
			this.Menu45Minutes.Activated += new global::System.EventHandler (this.M45_Click);
			this.Menu60Minutes.Activated += new global::System.EventHandler (this.M60_Click);
			this.Menu75Minutes.Activated += new global::System.EventHandler (this.M75_Click);
			this.Menu90Minutes.Activated += new global::System.EventHandler (this.M90_Click);
			this.MenuVariable.Activated += new global::System.EventHandler (this.Variable_Click);
			this.buttonSave.Clicked += new global::System.EventHandler (this.SaveTournament_Click);
			this.buttonNewPlayer.Clicked += new global::System.EventHandler (this.NewPlayer_Click);
			this.buttonChangePairings.Clicked += new global::System.EventHandler (this.ChangePairings_Click);
			this.buttonChangePlayer.Clicked += new global::System.EventHandler (this.ChangePlayer_Click);
			this.buttonReset.Clicked += new global::System.EventHandler (this.ResetLastResults_Click);
		}
	}
}