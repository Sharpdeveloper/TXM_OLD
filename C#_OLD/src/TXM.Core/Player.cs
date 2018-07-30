using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TXM.Core
{
    [Serializable]
    public class Player
    {
        private static int currentID = 0;
        private int version = 0;

        #region Player Informations
        public string Name { get; set; }
        public string Forename { get; set; }
        public string Nickname { get; set; }
        public int TableNo { get; set; }
        public string DisplayName
        {
            get
            {
                if (Nickname != null && Nickname != "")
                    return Forename + " \"" + Nickname + "\"";
                else
                    return Forename + " " + Name.ToCharArray()[0] + "."; 
            }
        }
        public string Team { get; set; }
        public string City { get; private set; }
        public string SquadList { get; set; }
        public bool Disqualified { get; set; }              
        public bool Dropped { get; set; }
        public bool Present { get; set; }
        public int Order
        {
            get
            {
                return new Random().Next(0, 99999);
            }
        }
        #endregion

        #region Game Infomations
        public int ID { get; set; }
        public int Wins { get;  set; }
        public int ModifiedWins { get;  set; }
        public int Losses { get;  set; }
        public int ModifiedLosses { get; set; }
        public int Draws { get;  set; }
        public int TournamentPoints { get;  set; }
        public int DestroyedPoints { get;  set; }
        public int LostPoints { get;  set; }
        public double StrengthOfSchedule { get;  set; }
        public double ExtendedStrengthOfSchedule { get; set; }
        public int MarginOfVictory { get;  set; }
        public string Faction { get; set; }
        public List<Result> Results { get; set; }
        #endregion

        #region Tournament Informations
        public List<Player> Enemies { get; set; }
        public bool Bye { get; set; }
        public bool Paired { get; set; }
        public bool WonBye { get; set; }
        #endregion

        #region T3 Informations
        public int T3ID { get; private set; }
        public int Rank { get; set; }
        public int ArmyRank { get; set; }
        public bool Paid { get; set; }
        public bool ListGiven { get; set; }
        #endregion

        #region Constructors
        public Player(Player p):this(p.Name, p.Forename, p.Nickname, p.Team,p.City,p.Wins,p.ModifiedWins,p.Losses,p.Draws,p.TournamentPoints,p.DestroyedPoints,p.LostPoints,p.StrengthOfSchedule,p.MarginOfVictory,p.Faction,p.Bye,p.Paired,p.WonBye,p.T3ID,p.Rank,p.ArmyRank,p.Paid,p.ListGiven,p.ID)
        {

        }

        public Player(string name, string forename, string nickname, string team, string city, int wins, int modifiedWins, int looses, int draws, int points, int pointsDestroyed, int pointsLost, double pointsOfEnemies, int marginOfVictory, string playersFaction, bool freeticket, bool paired, bool wonFreeticket, int t3ID, int rank, int armyRank, bool payed, bool squadListGiven, int nr = -1)
        {
            Name = name;
            Forename = forename;
            Nickname = nickname;
            Team = team;
            City = city;
            Wins = wins;
            ModifiedWins = modifiedWins;
            Losses = looses;
            Draws = draws;
            TournamentPoints = points;
            DestroyedPoints = pointsDestroyed;
            LostPoints = pointsLost;
            StrengthOfSchedule = pointsOfEnemies;
            MarginOfVictory = marginOfVictory;
            Faction = playersFaction;
            Bye = freeticket;
            Paired = paired;
            WonBye = wonFreeticket;
            T3ID = t3ID;
            Rank = rank;
            ArmyRank = armyRank;
            Paid = payed;
            ListGiven = squadListGiven;
            if (nr == -1)
                ID = ++currentID;
            else
                ID = nr;
            Enemies = new List<Player>();
            Results = new List<Result>();
        }

        public Player(int t3ID, string forename, string name, string nickname, string faction, string city, string team, bool payed, bool armylistgiven)
            : this(name, forename, nickname, team, city, 0, 0, 0, 0, 0, 0, 0, 0, 0, faction, false, false, false, t3ID, 0, 0, payed, armylistgiven)
        { }

        public Player(string nickname, string playersFaction)
            : this(0, "", "", nickname, playersFaction, "", "", false, false)
        { }

        public Player(string nickname)
            : this(nickname, "Imperial")
        { }
        #endregion

        #region Public Functions
        public static Player GetWonBye()
        {
            Player p = new Player("WonBye")
            {
                ID = -2
            };
            return p;
        }

        public static Player GetBye()
        {
            Player p = new Player("Bye")
            {
                ID = -1
            };
            return p;
        }

        public static Player GetBonus()
        {
            Player p = new Player("Bonus")
            {
                ID = -3
            };
            return p;
        }

        public bool HasPlayedVS(Player enemy)
        {
            if (Enemies == null)
                return false;
            foreach (Player _enemy in Enemies)
            {
                if (_enemy.ID == enemy.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPlayedAndWonVS(Player enemy)
        {
            if (Enemies == null)
                return false;
            for(int i = 0; i < Enemies.Count; i++)
            {
                var _enemy = Enemies[i];
                if (_enemy.ID == enemy.ID)
                {
                    return Results[i].Destroyed - Results[i].Lost > 0;
                }
            }
            return false;
        }

        public void SumStrengthOfSchedule()
        {
            StrengthOfSchedule = 0;
            for (int i = 0; i < Enemies.Count; i++)
            {
                StrengthOfSchedule += GetEnemyTournamentPoints(i);
            }
            StrengthOfSchedule = StrengthOfSchedule / Results.Count;
        }

        public void SumExtendedStrengthOfSchedule()
        {
            ExtendedStrengthOfSchedule = 0;
            for (int i = 0; i < Enemies.Count; i++)
            {
                ExtendedStrengthOfSchedule += Enemies[i].StrengthOfSchedule;
            }
            ExtendedStrengthOfSchedule = ExtendedStrengthOfSchedule / Results.Count;
        }

        public void AddLastEnemy(Player enemy)
        {
            Enemies.Add(enemy);
            SumStrengthOfSchedule();
        }

        public void RemoveLastResult()
        {
            RemoveRound(Results.Count - 1);
            Results.RemoveAt(Results.Count - 1);
        }

        public void RemoveRound(int round)
        {           
            Result result = Results[round];
            int mov = result.MaxPoints;// + CalcuateMarginOfVictory(result.Destroyed, result.Lost, result.Enemy.PointOfSquad, result.MaxPoints);
            DestroyedPoints -= result.Destroyed;
            LostPoints -= result.Lost;
            MarginOfVictory -= mov;

            if (result.Destroyed <= result.Lost)
            {
                if (result.Destroyed == result.Lost && result.WinnerID == this.ID)
                {
                    Wins--;
                    TournamentPoints--;
                }
                else
                    Losses--;
            }
            else
            {
                    TournamentPoints --;
                    Wins--;
            }
            Enemies.RemoveAt(Enemies.Count - 1);
        }

        public void Disqualify()
        {
            Disqualified = true;
            if (Nickname != null)
                Nickname += " <disqualified>";
            else
                Name += " <disqualified>";
        }

        public static void ResetID()
        {
            currentID = 0;
        }
        #endregion

        #region Properties
        public int EnemyCount
        {
            get
            {
                return Enemies.Count;
            }
        }
        #endregion

        #region Static Functions
        public bool Equals(Player other)
        {
            return ID == other.ID;
        }


        #endregion

        #region private Functions
        private double GetEnemyTournamentPoints(int enemyNr)
        {
            if (enemyNr < 0 || Enemies[enemyNr].TournamentPoints == 0)
                return 0;
            else
            {
                return ((double) Enemies[enemyNr].TournamentPoints) / Enemies[enemyNr].Results.Count;
            }
        }

        public void GetResults(Player p)
        {
            Draws = p.Draws;
            Losses = p.Losses;
            Wins = p.Wins;
            TournamentPoints = p.TournamentPoints;
            DestroyedPoints = p.DestroyedPoints;
            LostPoints = p.LostPoints;
            StrengthOfSchedule = p.StrengthOfSchedule;
            ModifiedWins = p.ModifiedWins;
            MarginOfVictory = p.MarginOfVictory;
        }
        #endregion

        public void Drop()
        {
            Dropped = true;
            if (Nickname != null)
                Nickname += " <dropped>";
            else
                Name += " <dropped>";
        }

		public Player(SerializationInfo info, StreamingContext context)
		{
			version = (int)info.GetValue("Player_Version", typeof(int));
			if (version == 0)
			{
				currentID = (int)info.GetValue("Player_currentID", typeof(int));
                Name = (string)info.GetValue("Player_Name", typeof(string));
                Forename = (string)info.GetValue("Player_Forename", typeof(string));
                Nickname = (string)info.GetValue("Player_Nickname", typeof(string));
                TableNo = (int)info.GetValue("Player_TableNo", typeof(int));
                Team = (string)info.GetValue("Player_Team", typeof(string));
                City = (string)info.GetValue("Player_City", typeof(string));
                SquadList = (string)info.GetValue("Player_SquadList", typeof(string));
                Disqualified = (bool)info.GetValue("Player_Disqualified", typeof(bool));
                Dropped = (bool)info.GetValue("Player_Dropped", typeof(bool));
                Present = (bool)info.GetValue("Player_Present", typeof(bool));
                ID = (int)info.GetValue("Player_ID", typeof(int));
                Wins = (int)info.GetValue("Player_Wins", typeof(int));
                ModifiedWins = (int)info.GetValue("Player_ModifiedWins", typeof(int));
                Losses = (int)info.GetValue("Player_Losses", typeof(int));
                ModifiedLosses = (int)info.GetValue("Player_ModifiedLosses", typeof(int));
                Draws = (int)info.GetValue("Player_Draws", typeof(int));
                TournamentPoints = (int)info.GetValue("Player_TournamentPoints", typeof(int));
                DestroyedPoints = (int)info.GetValue("Player_DestroyedPoints", typeof(int));
                LostPoints = (int)info.GetValue("Player_LostPoints", typeof(int));
                StrengthOfSchedule = (double)info.GetValue("Player_StrengthOfSchedule", typeof(double));
                ExtendedStrengthOfSchedule = (double)info.GetValue("Player_ExtendedStrengthOfSchedule", typeof(double));
                MarginOfVictory = (int)info.GetValue("Player_MarginOfVictory", typeof(int));
                Faction = (string)info.GetValue("Player_Faction", typeof(string));
                Results = (List<Result>)info.GetValue("Player_Results", typeof(List<Result>));
                Enemies = (List<Player>)info.GetValue("Player_Enemies", typeof(List<Player>));
                Bye = (bool)info.GetValue("Player_Bye", typeof(bool));
                Paired = (bool)info.GetValue("Player_Paired", typeof(bool));
                WonBye = (bool)info.GetValue("Player_WonBye", typeof(bool));
                T3ID = (int)info.GetValue("Player_T3ID", typeof(int));
                Rank = (int)info.GetValue("Player_Rank", typeof(int));
                ArmyRank = (int)info.GetValue("Player_ArmyRank", typeof(int));
                Paid = (bool)info.GetValue("Player_Paid", typeof(bool));
                ListGiven = (bool)info.GetValue("Player_ListGiven", typeof(bool));
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Player_Version", version, typeof(int));
			info.AddValue("Player_currentID", currentID, typeof(int));
			info.AddValue("Player_Name", Name, typeof(string));
			info.AddValue("Player_Forename", Forename, typeof(string));
			info.AddValue("Player_Nickname", Nickname, typeof(string));
			info.AddValue("Player_TableNo", TableNo, typeof(int));
			info.AddValue("Player_Team", Team, typeof(string));
			info.AddValue("Player_City", City, typeof(string));
			info.AddValue("Player_SquadList", SquadList, typeof(string));
			info.AddValue("Player_Disqualified", Disqualified, typeof(bool));
			info.AddValue("Player_Dropped", Dropped, typeof(bool));
			info.AddValue("Player_Present", Present, typeof(bool));
			info.AddValue("Player_ID", ID, typeof(int));
			info.AddValue("Player_Wins", Wins, typeof(int));
			info.AddValue("Player_ModifiedWins", ModifiedWins, typeof(int));
			info.AddValue("Player_Losses", Losses, typeof(int));
			info.AddValue("Player_ModifiedLosses", ModifiedLosses, typeof(int));
			info.AddValue("Player_Draws", Draws, typeof(int));
			info.AddValue("Player_TournamentPoints", TournamentPoints, typeof(int));
			info.AddValue("Player_DestroyedPoints", DestroyedPoints, typeof(int));
			info.AddValue("Player_LostPoints", LostPoints, typeof(int));
			info.AddValue("Player_StrengthOfSchedule", StrengthOfSchedule, typeof(double));
			info.AddValue("Player_ExtendedStrengthOfSchedule", ExtendedStrengthOfSchedule, typeof(double));
			info.AddValue("Player_MarginOfVictory", MarginOfVictory, typeof(int));
			info.AddValue("Player_Faction", Faction, typeof(string));
			info.AddValue("Player_Results", Results, typeof(List<Result>));
			info.AddValue("Player_Enemies", Enemies, typeof(List<Player>));
			info.AddValue("Player_Bye", Bye, typeof(bool));
			info.AddValue("Player_Paired", Paired, typeof(bool));
			info.AddValue("Player_WonBye", WonBye, typeof(bool));
			info.AddValue("Player_T3ID", T3ID, typeof(int));
			info.AddValue("Player_Rank", Rank, typeof(int));
			info.AddValue("Player_ArmyRank", ArmyRank, typeof(int));
			info.AddValue("Player_Paid", Paid, typeof(bool));
			info.AddValue("Player_ListGiven", ListGiven, typeof(bool));
		}
    }
}
