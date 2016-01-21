using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOWare.Core
{
    public class Controller
    {
        public Lang ActiveLanguage { get { return activeIO.ActiveLanguage; } }
        private Tournament activeTournament;
        private IO activeIO;
        private Settings activeSetting;

        public Controller(IFileManager fileManager, IMessageManager messageManager, Lang lang)
        {
            activeIO = new IO(fileManager, messageManager, lang);
            activeSetting = activeIO.ActiveSettings;
        }

        /// <summary>
        /// Creates a new Tournament
        /// </summary>
        /// <param name="td">A tournament dialog of the current OS which implentes the interface</param>
        /// <returns>true: New Tournament created; false: no new tournament</returns>
        public bool NewTournament(ITournamentDialog td)
        {
            if (activeTournament != null)
            {
                if (!activeIO.ShowMessageWithOKCancel(ActiveLanguage.ExistingTournament))
                {
                    return false;
                }
            }

            td.Title = ActiveLanguage.NewTournament;
            td.TabTitleBasic = ActiveLanguage.Basic;
            td.TabTitleAdvanced = ActiveLanguage.Advanced;
            td.Controll = new TournamentController(activeIO, ActiveLanguage);
            td.SetPresets();
            if (td.ShowDialog() == true)
            {
                activeTournament = new Tournament();
                return true;
            }
            return false;
        }
    }
}
