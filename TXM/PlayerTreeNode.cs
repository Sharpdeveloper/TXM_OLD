using System;

using TXM.Core;

namespace TXM
{
    public class PlayerTreeNode: Gtk.TreeNode
    {
        [Gtk.TreeNodeValue(Column = 0)]
        public int Rank { get; private set; }

        [Gtk.TreeNodeValue(Column = 1)]
        public string Nickname { get; private set; }

        [Gtk.TreeNodeValue(Column = 2)]
        public string Present { get; private set; }

        [Gtk.TreeNodeValue(Column = 3)]
        public string Team { get; private set; }

        [Gtk.TreeNodeValue(Column = 4)]
        public string Faction { get; private set; }

        [Gtk.TreeNodeValue(Column = 5)]
        public int Points { get; private set; }

        [Gtk.TreeNodeValue(Column = 6)]
        public int Wins { get; private set; }

        [Gtk.TreeNodeValue(Column = 7)]
        public int ModifiedWins { get; private set; }

        [Gtk.TreeNodeValue(Column = 8)]
        public int Draws { get; private set; }

        [Gtk.TreeNodeValue(Column = 9)]
        public int Looses { get; private set; }

        [Gtk.TreeNodeValue(Column = 10)]
        public int MoV { get; private set; }

        [Gtk.TreeNodeValue(Column = 11)]
        public int SoS { get; private set; }

        public PlayerTreeNode(Player p)
        {
            Rank = p.Rank;
            Nickname = p.DisplayName;
            Present = p.Present ? "X" : "";
            Team = p.Team;
            Faction = p.Faction;
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

