using System;

using TXM.Core;

namespace TXM
{
    public class PlayerTreeNode: Gtk.TreeNode
    {
        [Gtk.TreeNodeValue(Column = 0)]
        public int Rank { get; private set; }

        [Gtk.TreeNodeValue(Column = 1)]
        public int Nr { get; private set; }

        [Gtk.TreeNodeValue(Column = 2)]
        public string Forename { get; private set; }

        [Gtk.TreeNodeValue(Column = 3)]
        public string Nickname { get; private set; }

        [Gtk.TreeNodeValue(Column = 4)]
        public string WonFreeticket { get; private set; }

        [Gtk.TreeNodeValue(Column = 5)]
        public string Paid { get; private set; }

        [Gtk.TreeNodeValue(Column = 6)]
        public string ArmyListGiven { get; private set; }

        [Gtk.TreeNodeValue(Column = 7)]
        public string Team { get; private set; }

        [Gtk.TreeNodeValue(Column = 8)]
        public string Faction { get; private set; }

        [Gtk.TreeNodeValue(Column = 9)]
        public int Squad { get; private set; }

        [Gtk.TreeNodeValue(Column = 10)]
        public int Points { get; private set; }

        [Gtk.TreeNodeValue(Column = 11)]
        public int Wins { get; private set; }

        [Gtk.TreeNodeValue(Column = 12)]
        public int ModifiedWins { get; private set; }

        [Gtk.TreeNodeValue(Column = 13)]
        public int Draws { get; private set; }

        [Gtk.TreeNodeValue(Column = 14)]
        public int Looses { get; private set; }

        [Gtk.TreeNodeValue(Column = 15)]
        public int MoV { get; private set; }

        [Gtk.TreeNodeValue(Column = 16)]
        public int SoS { get; private set; }

        public PlayerTreeNode(Player p)
        {
            Rank = p.Rank;
            Nr = p.Nr;
            Forename = p.Forename;
            Nickname = p.Nickname;
            WonFreeticket = p.WonFreeticket ? "X" : "";
            Paid = p.Payed ? "X" : "";
            ArmyListGiven = p.SquadListGiven ? "X" : "";
            Team = p.Team;
            Faction = p.Faction;
            Squad = p.PointOfSquad;
            Points = p.Points;
            Wins = p.Wins;
            ModifiedWins = p.ModifiedWins;
            Draws = p.Draws;
            Looses = p.Looses;
            MoV = p.MarginOfVictory;
            SoS = p.PointsOfEnemies;
        }
    }
}

