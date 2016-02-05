using System;

using TXM.Core;

namespace TXM
{
    public class PairingTreeNode: Gtk.TreeNode
    {
        [Gtk.TreeNodeValue(Column = 0)]
        public int Nr { get; private set; }

        [Gtk.TreeNodeValue(Column = 1)]
        public string Player1 { get; private set; }

        [Gtk.TreeNodeValue(Column = 2)]
        public string Player2 { get; private set; }

        [Gtk.TreeNodeValue(Column = 3)]
        public int Player1Score
        { 
            get
            {
                return GetPairing.Player1Score;
            }
            set
            {
                GetPairing.Player1Score = value;
            }
        }

        [Gtk.TreeNodeValue(Column = 4)]
        public int Player2Score
        { 
            get
            {
                return GetPairing.Player2Score;
            }
            set
            {
                GetPairing.Player1Score = value;
            }
        }

        [Gtk.TreeNodeValue(Column = 5)]
        public string ResultEdited
        { 
            get
            {
                //return "X";
                return GetPairing.ResultEdited ? "X" : " ";
            }
            set
            {
                GetPairing.ResultEdited = value.ToUpper().StartsWith("X");
            }
        }

        public Pairing GetPairing { get; private set; }

        public PairingTreeNode(Pairing p)
        {
            GetPairing = p;
            Nr = p.TableNr;
            Player1 = p.Player1Name;
            Player2 = p.Player2Name;
        }
    }
}

