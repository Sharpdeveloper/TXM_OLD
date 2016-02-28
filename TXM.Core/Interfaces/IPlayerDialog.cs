using System;

namespace TXM.Core
{
    public interface IPlayerDialog
    {
        bool OK { get; set; }

        string LastName { get; set; }

        string ForeName { get; set; }

        string NickName { get; set; }

        int TableNr { get; set; }

        string Team { get; set; }

        string City { get; set; }

        bool Disqualified { get; set; }

        bool Present { get; set; }

        string Faction { get; set; }

        bool WonBye { get; set; }

        string T3ID { get; set; }

        bool Paid { get; set; }

        bool SquadlistGiven { get; set; }

        bool NewPlayer { get; set; }

        void Delete();

        void ShowDialog();

        void AddFaction(string faction);

        Language DisplayedLanguage { get; set; }

    }
}

