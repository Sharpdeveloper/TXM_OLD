using System;

namespace TXM.Core
{
    public interface ITournamentDialog
    {
        bool OK { get; set; }

        string TournamentName { get; set; }

        int MaxPoints { get; set; }

        bool Cut { get; set; }

        int CutTo { get; set; }

        bool NewTournament { get; set; }

        Language DisplayedLanguage { get; set; }

        void ShowDialog();
    }
}

