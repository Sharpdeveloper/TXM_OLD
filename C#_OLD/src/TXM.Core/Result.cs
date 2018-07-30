using System;
using System.Runtime.Serialization;

namespace TXM.Core
{
    [Serializable]
    public class Result : ISerializable
    {
        private int version = 1;

        public Player Enemy { get; set; }
        public int Destroyed { get; set; }
        public int Lost { get; set; }
        public int MaxPoints { get; set; }
        public int WinnerID { get; set; }
        public int TournamentPoints { get; set; }

        public Result(int destroyed, int lost, Player enemy, int maxPoints, int winnerID, int torurnamentPoints = 0)
        {
            Enemy = enemy;
            Destroyed = destroyed;
            Lost = lost;
            MaxPoints = maxPoints;
            WinnerID = winnerID;
            TournamentPoints = torurnamentPoints;
        }

		public Result(SerializationInfo info, StreamingContext context)
		{
			version = (int)info.GetValue("Result_Version", typeof(int));
			if (version == 0)
			{
                Enemy = (Player)info.GetValue("Result_Enemy", typeof(Player));
                Destroyed = (int)info.GetValue("Result_Destroyed", typeof(int));
                Lost = (int)info.GetValue("Result_Lost", typeof(int));
                MaxPoints = (int)info.GetValue("Result_MaxPoints", typeof(int));
                WinnerID = (int)info.GetValue("Result_WinnerID", typeof(int));
                TournamentPoints = 0;
                version = 1;
			}
            else if (version == 1)
            {
                Enemy = (Player)info.GetValue("Result_Enemy", typeof(Player));
                Destroyed = (int)info.GetValue("Result_Destroyed", typeof(int));
                Lost = (int)info.GetValue("Result_Lost", typeof(int));
                MaxPoints = (int)info.GetValue("Result_MaxPoints", typeof(int));
                WinnerID = (int)info.GetValue("Result_WinnerID", typeof(int));
                TournamentPoints = (int)info.GetValue("Result_TournamentPoints", typeof(int));
            }
        }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Result_Version", version, typeof(int));
			info.AddValue("Result_Enemy", Enemy, typeof(Player));
			info.AddValue("Result_Destroyed", Destroyed, typeof(int));
			info.AddValue("Result_Lost", Lost, typeof(int));
			info.AddValue("Result_MaxPoints", MaxPoints, typeof(int));
			info.AddValue("Result_WinnerID", WinnerID, typeof(int));
            info.AddValue("Result_TournamentPoints", TournamentPoints, typeof(int));
        }
    }
}
