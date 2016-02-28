using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public class Player
    {
        #region Player Informations

        public string Name { get; set; }

        public string Forename { get; set; }

        public string Nickname { get; set; }

        public int TableNr { get; set; }

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

        public string City { get; set; }

        public string SquadList { get; set; }

        public bool Disqualified { get; set; }

        public bool DefeatedAllInGroup { get; set; }

        public bool Present { get; set; }

        private static int currentStartNr = 0;

        public int Order
        {
            get
            {
                Random r = new Random();
                return r.Next(0, 99999);
            }
        }

        #endregion

        #region Game Infomations

        public int Nr { get; set; }

        public int Wins { get; set; }

        public int ModifiedWins { get; set; }

        public int Looses { get; set; }

        public int Draws { get; set; }

        public int Points { get; set; }

        public int PointsDestroyed { get; set; }

        public int PointsLost { get; set; }

        public int PointsOfEnemies { get; set; }

        public int PointOfSquad { get; set; }

        public int MarginOfVictory { get; set; }

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

        public string T3ID { get; set; }

        public int Rank { get; set; }

        public string ArmyRank { get; set; }

        public bool Paid { get; set; }

        public bool SquadListGiven { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Copies a player
        /// </summary>
        /// <param name="p">Player.</param>
        public Player(Player p)
            : this(p.Name, p.Forename, p.Nickname, p.Team, p.City, p.Wins, p.ModifiedWins, p.Looses, p.Draws, p.Points, p.PointsDestroyed, p.PointsLost, p.PointsOfEnemies, p.PointOfSquad, p.MarginOfVictory, p.Faction, p.Bye, p.Paired, p.WonBye, p.T3ID, p.Rank, p.ArmyRank, p.Paid, p.SquadListGiven, p.Nr)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXM.Core.Player"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="forename">Forename.</param>
        /// <param name="nickname">Nickname.</param>
        /// <param name="team">Team.</param>
        /// <param name="city">City.</param>
        /// <param name="wins">Wins.</param>
        /// <param name="modifiedWins">Modified wins.</param>
        /// <param name="looses">Looses.</param>
        /// <param name="draws">Draws.</param>
        /// <param name="points">Points.</param>
        /// <param name="pointsDestroyed">Points destroyed.</param>
        /// <param name="pointsLost">Points lost.</param>
        /// <param name="pointsOfEnemies">Points of enemies.</param>
        /// <param name="pointOfSquad">Point of squad.</param>
        /// <param name="marginOfVictory">Margin of victory.</param>
        /// <param name="playersFaction">Players faction.</param>
        /// <param name="freeticket">If set to <c>true</c> freeticket.</param>
        /// <param name="paired">If set to <c>true</c> paired.</param>
        /// <param name="wonFreeticket">If set to <c>true</c> won freeticket.</param>
        /// <param name="t3ID">T3 I.</param>
        /// <param name="rank">Rank.</param>
        /// <param name="armyRank">Army rank.</param>
        /// <param name="paid">If set to <c>true</c> paid.</param>
        /// <param name="squadListGiven">If set to <c>true</c> squad list given.</param>
        /// <param name="nr">Nr.</param>
        public Player(string name, string forename, string nickname, string team, string city, int wins, int modifiedWins, int looses, int draws, int points, int pointsDestroyed, int pointsLost, int pointsOfEnemies, int pointOfSquad, int marginOfVictory, string playersFaction, bool freeticket, bool paired, bool wonFreeticket, string t3ID, int rank, string armyRank, bool paid, bool squadListGiven, int nr = -1)
        {
            Name = name;
            Forename = forename;
            Nickname = nickname;
            Team = team;
            City = city;
            Wins = wins;
            ModifiedWins = modifiedWins;
            Looses = looses;
            Draws = draws;
            Points = points;
            PointsDestroyed = pointsDestroyed;
            PointsLost = pointsLost;
            PointsOfEnemies = pointsOfEnemies;
            PointOfSquad = pointOfSquad;
            MarginOfVictory = marginOfVictory;
            Faction = playersFaction;
            Bye = freeticket;
            Paired = paired;
            WonBye = wonFreeticket;
            T3ID = t3ID;
            Rank = rank;
            ArmyRank = armyRank;
            Paid = paid;
            SquadListGiven = squadListGiven;
            if (nr == -1)
                Nr = ++currentStartNr;
            else
                Nr = nr;
            Enemies = new List<Player>();
            Results = new List<Result>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXM.Core.Player"/> class.
        /// </summary>
        /// <param name="t3ID">T3 I.</param>
        /// <param name="forename">Forename.</param>
        /// <param name="name">Name.</param>
        /// <param name="nickname">Nickname.</param>
        /// <param name="faction">Faction.</param>
        /// <param name="city">City.</param>
        /// <param name="team">Team.</param>
        /// <param name="paid">If set to <c>true</c> paid.</param>
        /// <param name="armylistgiven">If set to <c>true</c> armylistgiven.</param>
        /// <param name="pointOfSquad">Point of squad.</param>
        public Player(string t3ID, string forename, string name, string nickname, string faction, string city, string team, bool paid, bool armylistgiven, int pointOfSquad = 100)
            : this(name, forename, nickname, team, city, 0, 0, 0, 0, 0, 0, 0, 0, pointOfSquad, 0, faction, false, false, false, t3ID, 0, "0", paid, armylistgiven)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXM.Core.Player"/> class.
        /// </summary>
        /// <param name="nickname">Nickname.</param>
        /// <param name="pointOfSquad">Point of squad.</param>
        /// <param name="playersFaction">Players faction.</param>
        public Player(string nickname, int pointOfSquad, string Faction)
            : this("0", "", "", nickname, Faction, "", "", false, false, pointOfSquad)
        {
            
        }

        public Player()
        {
            Nr = ++currentStartNr;
            Enemies = new List<Player>();
            Results = new List<Result>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXM.Core.Player"/> class.
        /// </summary>
        /// <param name="nickname">Nickname.</param>
        public Player(string nickname, string imperiumText)
            : this(nickname, 100, imperiumText)
        {
        }

        #endregion

        #region Public Functions

        public bool HasPlayedVS(Player enemy)
        {
            if (Enemies == null)
                return false;
            foreach (Player enemie in Enemies)
            {
                if (enemie.Nr == enemy.Nr)
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
            for (int i = 0; i < Enemies.Count; i++)
            {
                try
                {
                    var enemie = Enemies[i];
                    if (enemie.Nr == enemy.Nr)
                    {
                        return Results[i].Destroyed - Results[i].Lost > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public void SumEnemiesStrength()
        {
            PointsOfEnemies = 0;
            for (int i = 0; i < Enemies.Count; i++)
            {
                PointsOfEnemies += GetEnemyStrength(i);
            }
        }

        public bool AddResult(Result result, bool update = false)
        {
            bool winner = false;
            if (Results == null)
                Results = new List<Result>();
            if (Bye)
            {
                MarginOfVictory += (int)(1.5 * result.MaxSquadPoints);

                Wins++;
                winner = true;

                Points += 5;

                Enemies.Add(result.Enemy);

                Bye = false;
            }
            else if (result.FirstRound && WonBye)
            {
                MarginOfVictory += 2 * result.MaxSquadPoints;

                Wins++;
                winner = true;

                Points += 5;

            }
            else
            {
                int mov = result.MaxSquadPoints + CalcuateMarginOfVictory(result.Destroyed, result.Lost, result.Enemy.PointOfSquad, result.MaxSquadPoints);
                PointsDestroyed += result.Destroyed;
                PointsLost += result.Lost;
                MarginOfVictory += mov;

                if (result.Destroyed <= result.Lost)
                {
                    if (result.Destroyed == result.Lost)
                    {
                        Draws++;
                        Points++;
                    }
                    else
                        Looses++;
                }
                else
                {
                    if ((result.Destroyed - result.Lost) > 11)
                    {
                        Points += 5;
                        Wins++;
                    }
                    else
                    {
                        Points += 3;
                        ModifiedWins++;
                    }
                    winner = true;
                }
            }

            if (!update)
            {
                Results.Add(result);
                Enemies.Add(result.Enemy);
            }

            return winner;
        }

        public void AddLastEnemy(Player enemy)
        {
            Enemies.Add(enemy);

            SumEnemiesStrength();
        }

        public void Update(Result r, int round)
        {
            RemoveRound(round - 1);

            //Add new Result:
            AddResult(r, true);
            Results[round - 1] = r;
        }

        public void RemoveLastResult()
        {
            RemoveRound(Results.Count - 1);
            Results.RemoveAt(Results.Count - 1);
        }

        public void RemoveRound(int round)
        {           
            Result result = Results[round];
            int mov = result.MaxSquadPoints + CalcuateMarginOfVictory(result.Destroyed, result.Lost, result.Enemy.PointOfSquad, result.MaxSquadPoints);
            PointsDestroyed -= result.Destroyed;
            PointsLost -= result.Lost;
            MarginOfVictory -= mov;

            if (result.Destroyed <= result.Lost)
            {
                if (result.Destroyed == result.Lost)
                {
                    Draws--;
                    Points--;
                }
                else
                    Looses--;
            }
            else
            {
                if ((result.Destroyed - result.Lost) > 11)
                {
                    Points -= 5;
                    Wins--;
                }
                else
                {
                    Points -= 3;
                    ModifiedWins--;
                }
            }
            Enemies.RemoveAt(Enemies.Count - 1);
        }

        public void Disqualify(string disqualifiedText)
        {
            Disqualified = true;
            if (Nickname != null)
                Nickname += " <" + disqualifiedText + ">";
            else
                Name += " <" + disqualifiedText + ">"; 
        }

        #endregion

        public bool Equals(Player other)
        {
            return Nr == other.Nr;
        }

        #region private Functions

        private int GetEnemyStrength(int enemyNr)
        {
            if (enemyNr < 0)
                return 0;
            else
            {
                return Enemies[enemyNr].Points;
            }
        }

        private int CalcuateMarginOfVictory(int destroyed, int lost, int enemySquadPoints, int maxSquadPoints)
        {
            if (destroyed >= enemySquadPoints)
                return maxSquadPoints - lost;
            else
            {
                if (lost >= PointOfSquad)
                    lost = maxSquadPoints;
                return destroyed - lost;
            }
        }

        public void GetResults(Player p)
        {
            Draws = p.Draws;
            Looses = p.Looses;
            Wins = p.Wins;
            Points = p.Points;
            PointsDestroyed = p.PointsDestroyed;
            PointsLost = p.PointsLost;
            PointsOfEnemies = p.PointsOfEnemies;
            ModifiedWins = p.ModifiedWins;
            MarginOfVictory = p.MarginOfVictory;
        }

        #endregion


    }
}
