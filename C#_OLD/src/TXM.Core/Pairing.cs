using System;
using System.Runtime.Serialization;

namespace TXM.Core
{
    [Serializable]
    public class Pairing : ISerializable
    {
        #region Static Fields
        private static int tableNr = 0;
        private int version = 2;
        #endregion

        #region Properties
        public int TableNr { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public string Winner { get; set; }
        public bool ResultEdited { get; set; }
        public int Player1Points { get; set; }
        public int Player2Points { get; set; }
        public bool Locked { get; set; }
        public bool Hidden
        {
            get
            {
                return (Player1Score != 0 && Player2Score != 0 && (Player1Score != Player2Score || Winner != "Automatic")) || ResultEdited || Locked;
            }
        }
        #endregion

        #region Constructor
        public Pairing()
        {
            Winner = "Automatic";
            TableNr = ++tableNr;
            ResultEdited = false;
            Locked = false;
        }

        public Pairing(int tableNr)
        {
            Winner = "Automatic";
            TableNr = tableNr;
            ResultEdited = false;
            Locked = false;
        }

        public Pairing(Pairing p)
        {
            TableNr = p.TableNr;
            Player1 = p.Player1;
            Player2 = p.Player2;
            Player1Score = p.Player1Score;
            Player2Score = p.Player2Score;
            ResultEdited = p.ResultEdited;
            Winner = p.Winner;
            Player1Points = p.Player1Points;
            Player2Points = p.Player2Points;
            Locked = p.Locked;
        }
        #endregion

        #region Indirect Properties
        public string Player1Name
        {
            get
            {
                return Player1.DisplayName;
            }
        }

        public string Player2Name
        {
            get
            {
                return Player2.DisplayName;
            }
        }
        #endregion

        #region Static Functions
        public static void ResetTableNr()
        {
            tableNr = 0;
        }
        #endregion


        public void GetTableNr()
        {
            TableNr = ++tableNr;
        }

		public Pairing(SerializationInfo info, StreamingContext context)
		{
			version = (int)info.GetValue("Pairing_Version", typeof(int));
			if (version == 0)
			{
				tableNr = (int)info.GetValue("Pairing_tableNr", typeof(int));
                TableNr = (int)info.GetValue("Pairing_TableNr", typeof(int));
                Player1 = (Player)info.GetValue("Pairing_Player1", typeof(Player));
                Player2 = (Player)info.GetValue("Pairing_Player2", typeof(Player));
                Player1Score = (int)info.GetValue("Pairing_Player1Score", typeof(int));
                Player2Score = (int)info.GetValue("Pairing_Player2Score", typeof(int));
                Winner = (string)info.GetValue("Pairing_Winner", typeof(string));
                ResultEdited = (bool)info.GetValue("Pairing_ResultEdited", typeof(bool));
                Player1Points = 0;
                Player2Points = 0;
                Locked = false;
                version = 2;
			}
            else if (version == 1)
            {
                tableNr = (int)info.GetValue("Pairing_tableNr", typeof(int));
                TableNr = (int)info.GetValue("Pairing_TableNr", typeof(int));
                Player1 = (Player)info.GetValue("Pairing_Player1", typeof(Player));
                Player2 = (Player)info.GetValue("Pairing_Player2", typeof(Player));
                Player1Score = (int)info.GetValue("Pairing_Player1Score", typeof(int));
                Player2Score = (int)info.GetValue("Pairing_Player2Score", typeof(int));
                Winner = (string)info.GetValue("Pairing_Winner", typeof(string));
                ResultEdited = (bool)info.GetValue("Pairing_ResultEdited", typeof(bool));
                Player1Points = (int)info.GetValue("Pairing_Player1Points", typeof(int));
                Player2Points = (int)info.GetValue("Pairing_Player2Points", typeof(int));
                Locked = false;
                version = 2;
            }
            else if (version == 2)
            {
                tableNr = (int)info.GetValue("Pairing_tableNr", typeof(int));
                TableNr = (int)info.GetValue("Pairing_TableNr", typeof(int));
                Player1 = (Player)info.GetValue("Pairing_Player1", typeof(Player));
                Player2 = (Player)info.GetValue("Pairing_Player2", typeof(Player));
                Player1Score = (int)info.GetValue("Pairing_Player1Score", typeof(int));
                Player2Score = (int)info.GetValue("Pairing_Player2Score", typeof(int));
                Winner = (string)info.GetValue("Pairing_Winner", typeof(string));
                ResultEdited = (bool)info.GetValue("Pairing_ResultEdited", typeof(bool));
                Player1Points = (int)info.GetValue("Pairing_Player1Points", typeof(int));
                Player2Points = (int)info.GetValue("Pairing_Player2Points", typeof(int));
                Locked = (bool)info.GetValue("Pairing_Locked", typeof(bool));
            }
        }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Pairing_Version", version, typeof(int));
			info.AddValue("Pairing_tableNr", tableNr, typeof(int));
			info.AddValue("Pairing_TableNr", TableNr, typeof(int));
			info.AddValue("Pairing_Player1", Player1, typeof(Player));
			info.AddValue("Pairing_Player2", Player2, typeof(Player));
			info.AddValue("Pairing_Player1Score", Player1Score, typeof(int));
			info.AddValue("Pairing_Player2Score", Player2Score, typeof(int));
			info.AddValue("Pairing_Winner", Winner, typeof(string));
			info.AddValue("Pairing_ResultEdited", ResultEdited, typeof(bool));
            info.AddValue("Pairing_Player1Points", Player1Points, typeof(int));
            info.AddValue("Pairing_Player2Points", Player2Points, typeof(int));
            info.AddValue("Pairing_Locked", ResultEdited, typeof(bool));
        }
    }
}
