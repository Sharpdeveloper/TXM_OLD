using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public class Tournament
    {

        #region Tournament Information

        public List<Player> Participants { get; set; }

        public bool FirstRound { get; set; }

        public List<Pairing> PrePaired { get; set; }

        public string Name { get; set; }

        public List<string> Nicknames { get; internal set; }

        public int MaxSquadPoints { get; set; }

        public List<Round> Rounds { get; internal set; }

        public string FilePath { get; internal set; }

        public string AutoSavePath { get; internal set; }

        public int DisplayedRound { get; set; }

        public bool Cut { get; set; }

        public int CutTo { get; set; }

        public bool CutStarted { get; internal set; }

        public bool WonFreeticketsCalculated { get; internal set; }

        public List<Pairing> Pairings { get; set; }

        public int ActiveRound { get; set; }

        #endregion

        #region GUI_State

        public bool ButtonGetResultState { get; set; }

        public bool ButtonNextRoundState { get; set; }

        public bool ButtonCutState { get; set; }

        #endregion

        #region T3 Information

        public int T3ID { get; set; }

        public string GOEPPVersion { get; internal set; }

        #endregion

        #region internal Fields

        internal int currentStartNr;
        internal static List<int> givenStartNr = new List<int>();
        private Player WonBye;
        private Player Bye;
        internal List<Player> ListOfPlayers;
        private List<Player>[] PointGroup;
        internal int WonFreetickets;
        internal int currentCountOfPlayer;
        internal List<Player> WinnerLastRound;
        internal bool freeticket;

        #endregion

        #region Constructors

        public Tournament(string imperiumText, string byeText, string wonByeText)
        {
            Pairings = new List<Pairing>();
            Rounds = new List<Round>();
            Participants = new List<Player>();
            Nicknames = new List<string>();
            givenStartNr = new List<int>();
            Bye = new Player(byeText, imperiumText);
            WonBye = new Player(wonByeText, imperiumText);
            ActiveRound = 0;
        }

        public Tournament(string name, int t3ID, string GOEPPversion, bool firstround, int maxSquadPoints, string byeText, string wonByeText, string imperiumText)
        {
            Name = name;
            T3ID = t3ID;
            GOEPPVersion = GOEPPversion;
            currentStartNr = 0;
            Participants = new List<Player>();
            Nicknames = new List<string>();
            givenStartNr = new List<int>();
            FirstRound = firstround;
            Bye = new Player(byeText, imperiumText);
            WonBye = new Player(wonByeText, imperiumText);
            MaxSquadPoints = maxSquadPoints;
            CutStarted = false;
            WonFreeticketsCalculated = false;
            Pairings = new List<Pairing>();
            Rounds = new List<Round>();
            ActiveRound = 0;
        }

        public Tournament(int t3ID, string name, string byeText, string wonByeText, string imperiumText, string GOEPPversion = "")
            : this(name, t3ID, GOEPPversion, true, 100, byeText, wonByeText, imperiumText)
        {
        }

        public Tournament(string name, string byeText, string wonByeText, string imperiumText)
            : this(0, name, byeText, wonByeText, imperiumText)
        {
        }

        public Tournament(string name, int maxSquadPoints, string byeText, string wonByeText, string imperiumText)
            : this(name, 0, "", true, maxSquadPoints, byeText, wonByeText, imperiumText)
        {
        }

        #endregion

        #region public functions

        public Player GetPlayerByNr(int nr)
        {
            foreach (var p in Participants)
            {
                if (p.Nr == nr)
                    return p;
            }
            return null;
        }

        public void AddPlayer(Player player)
        {
            player.Nr = ++currentStartNr;
            Participants.Add(player);
            Nicknames.Add(player.Nickname);
            if (Pairings.Count > 0)
            {
                if (Pairings[Pairings.Count - 1].Player2 == Bye)
                {
                    Pairings[Pairings.Count - 1].Player2 = player;
                    Pairings[Pairings.Count - 1].ResultEdited = false;
                }
                else
                    Pairings.Add(new Pairing() { Player1 = player, ResultEdited = true, Player2 = Bye });
            }
        }

        public void AddInfos(string playerName, int points, string squadlist)
        {
            int playerNr = -1;
            for (int i = 0; i < Participants.Count; i++)
            {
                if (Participants[i].Nickname == playerName)
                {
                    playerNr = i;
                    break;
                }
            }
            if (playerNr >= 0)
            {
                Participants[playerNr].PointOfSquad = points;
                Participants[playerNr].SquadList = squadlist;
            }
        }

        public void Sort()
        {
            SetDeafetedValues();
            List<Player> t = Participants.OrderByDescending(x => x.Points).ThenByDescending(x => x.DefeatedAllInGroup).ThenByDescending(x => x.MarginOfVictory).ThenByDescending(x => x.PointsOfEnemies).ThenBy(x => x.Order).ToList<Player>();
            Participants = new List<Player>();
            foreach (Player p in t)
                Participants.Add(p);
            for (int i = 0; i < Participants.Count; i++)
                Participants[i].Rank = i + 1;
        }

        private void SetDeafetedValues()
        {
            CountGroups();
            foreach (var pl in PointGroup)
            {
                for (int i = 0; i < pl.Count; i++)
                {
                    var p = pl[i];
                    if (p.Enemies.Count < pl.Count - 1)
                        p.DefeatedAllInGroup = false;
                    else
                    {
                        bool defeated = true;
                        for (int j = 0; j < pl.Count; j++)
                        {
                            if (j == i)
                                continue;
                            var enemy = pl[j];
                            if (!p.HasPlayedAndWonVS(enemy))
                                defeated = false;
                            if (defeated == false)
                                break;
                        }
                        p.DefeatedAllInGroup = defeated;
                    }
                }
            }
        }

        public List<Pairing> GetSeed(bool start, bool cut)
        {
            Pairing.ResetTableNr();
            int temp, pos = 0;

            #region Cut
            if (Cut && (cut || CutStarted))
            {
                CalculateWonFreeticket();
                if (cut)
                {
                    Sort();
                    CutStarted = true;
                    currentCountOfPlayer = CutTo;

                    ListOfPlayers = new List<Player>();

                    for (int i = 0; i < currentCountOfPlayer; i++)
                        ListOfPlayers.Add(Participants[i]);

                    Pairings = new List<Pairing>();

                    while (ListOfPlayers.Count > 0)
                    {
                        Pairings.Add(new Pairing());
                        Pairings[pos].Player1 = ListOfPlayers[0];
                        Pairings[pos].Player2 = ListOfPlayers[ListOfPlayers.Count - 1];
                        ListOfPlayers.Remove(Pairings[pos].Player1);
                        ListOfPlayers.Remove(Pairings[pos].Player2);
                        pos++;
                    }
                }
                else
                {
                    Pairings = new List<Pairing>();

                    for (int i = 0; i < WinnerLastRound.Count / 2; i++)
                    {
                        Pairings.Add(new Pairing());
                        Pairings[i].Player1 = WinnerLastRound[i];
                        Pairings[i].Player2 = WinnerLastRound[WinnerLastRound.Count - 1 - i];
                    }
                }
            }
            #endregion
            else
            {
                if (start)
                    Start();
                else
                {
                    Sort();
                    Rounds[Rounds.Count - 1].Participants = new List<Player>();
                    foreach (Player p in Participants)
                        Rounds[Rounds.Count - 1].Participants.Add(new Player(p));

                }

                ListOfPlayers = new List<Player>();
                Random random = new Random();

                foreach (Player p in Participants)
                {
                    if (!p.Disqualified)
                        ListOfPlayers.Add(p);
                }

                Pairings = new List<Pairing>();

                if (start)
                {
                    if (PrePaired != null)
                    {
                        foreach (Pairing p in PrePaired)
                        {
                            Pairings.Add(p);
                            ListOfPlayers.Remove(p.Player1);
                            ListOfPlayers.Remove(p.Player2);
                            if (p.Player1.Bye)
                                p.ResultEdited = true;
                            pos++;
                        }
                    }
                    List<Player> wonFreeTickets = GetWonFreetickets();
                    for (int i = 0; i < wonFreeTickets.Count; i++)
                    {
                        Pairings.Add(new Pairing());
                        Pairings[pos].Player1 = wonFreeTickets[i];
                        Pairings[pos].Player2 = WonBye;
                        Pairings[pos].ResultEdited = true;
                        pos++;
                    }
                    if (ListOfPlayers.Count % 2 == 0)
                        freeticket = false;
                    else
                        freeticket = true;
                }
                else
                {
                    if (Participants.Count % 2 == 0)
                        freeticket = false;
                    else
                        freeticket = true;
                }
                if (start)
                {
                    while (ListOfPlayers.Count > 0)
                    {
                        Pairings.Add(new Pairing());
                        if (ListOfPlayers.Count == 1)
                        {
                            Pairings[pos].Player1 = ListOfPlayers[0];
                            Pairings[pos].Player2 = Bye;
                            Pairings[pos].Player1.Bye = true;
                            Pairings[pos].ResultEdited = true;
                            ListOfPlayers.Remove(Pairings[pos].Player1);
                            pos++;
                            break;
                        }
                        Pairings[pos].Player1 = ListOfPlayers[0];
                        for (int i = 0; i < ListOfPlayers.Count; i++)
                        {
                            temp = random.Next(1, ListOfPlayers.Count);
                            if (Pairings[pos].Player1.Team != ListOfPlayers[temp].Team || Pairings[pos].Player1.Team == "")
                            {
                                Pairings[pos].Player2 = ListOfPlayers[temp];
                                break;
                            }
                        }
                        if (Pairings[pos].Player2 == null)
                            Pairings[pos].Player2 = ListOfPlayers[1];

                        ListOfPlayers.Remove(Pairings[pos].Player1);
                        ListOfPlayers.Remove(Pairings[pos].Player2);
                        pos++;
                    }
                }
                else
                {
                    CountGroups();

                    int group;
                    for (int i = 0; i < PointGroup.Length; i++)
                    {
                        while (PointGroup[i].Count >= 2)
                        {
                            Pairings.Add(new Pairing());
                            temp = random.Next(0, PointGroup[i].Count);
                            Pairings[pos].Player1 = PointGroup[i][temp];
                            PointGroup[i].RemoveAt(temp);
                            temp = random.Next(0, PointGroup[i].Count);
                            group = -1;
                            for (int j = i; j < PointGroup.Length; j++)
                            {
                                if (!HasPlayedVsWholePointGroup((Player)Pairings[pos].Player1, j))
                                {
                                    group = j;
                                    break;
                                }
                            }
                            if (group != -1)
                            {
                                temp = random.Next(0, PointGroup[i].Count);
                                while (Pairings[pos].Player1.HasPlayedVS(PointGroup[group][temp]))
                                {
                                    temp = random.Next(0, PointGroup[i].Count);
                                }
                            }
                            else
                            {
                                //Was passiert wenn ein Spieler schon gegen alle gespielt hat?
                                group = i;
                            }
                            Pairings[pos].Player2 = PointGroup[group][temp];
                            PointGroup[group].RemoveAt(temp);
                            pos++;
                        }
                        if (PointGroup[i].Count == 1)
                        {
                            if (PointGroup.Length == i + 1 && freeticket)
                            {
                                Pairings.Add(new Pairing());
                                Pairings[pos].Player1 = PointGroup[i][0];
                                Pairings[pos].Player2 = Bye;
                                Pairings[pos].Player1.Bye = true;
                                Pairings[pos].ResultEdited = true;
                            }
                            else
                                PointGroup[i + 1].Add(PointGroup[i][0]);
                        }
                    }
                }

                //Prüfen, ob die letzten beiden schon gegeneinander gespielt haben
                temp = Pairings.Count - 1;

                if (Pairings[temp] == null)
                    temp--;

                if (Pairings[temp].Player1.HasPlayedVS(Pairings[temp].Player2) && Pairings[temp].Player1.Enemies.Count < Participants.Count)
                {
                    for (int i = temp - 1; i >= 0; i--)
                    {
                        if (!Pairings[temp].Player1.HasPlayedVS(Pairings[i].Player1) && !Pairings[temp].Player2.HasPlayedVS(Pairings[i].Player2))
                        {
                            ChangePairing(temp, 0, i, 0);
                            break;
                        }
                        if (!Pairings[temp].Player1.HasPlayedVS(Pairings[i].Player2) && !Pairings[temp].Player2.HasPlayedVS(Pairings[i].Player1))
                        {
                            ChangePairing(temp, 0, i, 1);
                            break;
                        }
                    }
                }
            }

            if (Rounds == null)
                Rounds = new List<Round>();
            Rounds.Add(new Round(Pairings, Participants));
            DisplayedRound = Rounds.Count;
            CheckTableNr();

            return Pairings;
        }

        
        private void CheckTableNr()
        {
            foreach (Pairing p in Pairings)
            {
                if (p.Player1.TableNr != 0 && p.Player1.Bye == false && p.TableNr != p.Player1.TableNr)
                {
                    if ((p.Player2.TableNr != 0 && p.Player1.TableNr < p.Player2.TableNr) || p.Player2.TableNr == 0)
                    {
                        ChangePairings(p.Player1.TableNr, p.TableNr);
                    }
                    else if (p.Player2.TableNr != 0 && p.Player1.TableNr > p.Player2.TableNr && p.Player2.TableNr != p.TableNr)
                    {
                        ChangePairings(p.Player2.TableNr, p.TableNr);
                    }
                }
                else if (p.Player2.TableNr != 0)
                {
                    ChangePairings(p.Player2.TableNr, p.TableNr);
                }
            }
        }

        private void ChangePairings(int a, int b)
        {
            a--;
            b--;
            Player a1 = Pairings[a].Player1;
            Player a2 = Pairings[a].Player2;
            bool result = Pairings[a].ResultEdited;
            Pairings[a].Player1 = Pairings[b].Player1;
            Pairings[a].Player2 = Pairings[b].Player2;
            Pairings[a].ResultEdited = Pairings[b].ResultEdited;
            Pairings[b].Player1 = a1;
            Pairings[b].Player2 = a2;
            Pairings[b].ResultEdited = result;
        }

        public void RemoveLastRound()
        {
            foreach (var p in Participants)
                p.RemoveLastResult();
            foreach (var p in Pairings)
            {
                p.Player1Score = 0;
                p.Player2Score = 0;
                if (p.Player1.Bye || (p.Player1.WonBye && FirstRound))
                    p.ResultEdited = true;
                else
                    p.ResultEdited = false;
            }
        }

        public void GetResults(List<Pairing> results, bool update = false)
        {
            Result r;
            bool winner;
            WinnerLastRound = new List<Player>();
            if (!update)
            {
                foreach (Pairing pairing in results)
                {
                    r = new Result(pairing.Player1Score, pairing.Player2Score, pairing.Player2, FirstRound, MaxSquadPoints);
                    winner = pairing.Player1.AddResult(r);
                    if (winner)
                        WinnerLastRound.Add(pairing.Player1);
                    if (pairing.Player2 != WonBye && pairing.Player2 != Bye)
                    {
                        r = new Result(pairing.Player2Score, pairing.Player1Score, pairing.Player1, FirstRound, MaxSquadPoints);
                        winner = pairing.Player2.AddResult(r);
                        if (winner)
                            WinnerLastRound.Add(pairing.Player2);
                    }
                }
                FirstRound = false;
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].ResultEdited)
                    {
                        r = new Result(results[i].Player1Score, results[i].Player2Score, results[i].Player2, FirstRound, MaxSquadPoints);
                        results[i].Player1.Update(r, DisplayedRound);
                        r = new Result(results[i].Player2Score, results[i].Player1Score, results[i].Player1, FirstRound, MaxSquadPoints);
                        results[i].Player2.Update(r, DisplayedRound);
                    }
                }
            }
            foreach (Player player in Participants)
                player.SumEnemiesStrength();
            for (int i = 0; i < results.Count; i++)
                results[i].ResultEdited = false;
            if (update)
                Rounds[DisplayedRound - 1].Pairings = results;
            else
                Rounds[Rounds.Count - 1].Pairings = results;
            PrePaired = new List<Pairing>();
        }

        public void ChangePlayer(Player player)
        {
            Participants[GetIndexOf(player)] = (Player)player;
        }

        public Player GetStrongestUnplayedEnemy(Player player)
        {
            for (int i = Participants.Count - 1; i >= 0; i--)
            {
                if (!player.HasPlayedVS(Participants[i]))
                    return Participants[i];
            }
            return null;
        }

        #endregion

        #region private functions

        private int GetIndexOf(Player player)
        {
            for (int i = 0; i < Participants.Count; i++)
            {
                if (Participants[i].Equals(player))
                    return i;
            }
            return -1;
        }

        private void CountGroups()
        {
            List<int> v = new List<int>();
            foreach (Player player in Participants)
            {
                if (!v.Contains(player.Points))
                {
                    v.Add(player.Points);
                }
            }
            PointGroup = new List<Player>[v.Count];
            v.OrderBy(x => x);
            for (int i = 0; i < v.Count; i++)
            {
                PointGroup[i] = new List<Player>();
                foreach (Player player in Participants)
                {
                    if (v[i] == player.Points)
                    {
                        PointGroup[i].Add(player);
                    }
                }
            }
        }

        private bool HasPlayedVsWholePointGroup(Player Player, int groupNr)
        {
            Player enemy;
            for (int i = 0; i < PointGroup[groupNr].Count; i++)
            {
                enemy = PointGroup[groupNr][i];
                if (!Player.HasPlayedVS(enemy) && !Player.Equals(enemy))
                    return false;
            }
            return true;
        }

        private void Start()
        {
            WonFreetickets = 0;
            foreach (Player p in Participants)
            {
                if (p.WonBye)
                    WonFreetickets++;
            }
        }

        private List<Player> GetWonFreetickets()
        {
            List<Player> result = new List<Player>();
            for (int i = 0; i < ListOfPlayers.Count; i++)
            {
                if (ListOfPlayers[i].WonBye)
                {
                    result.Add(ListOfPlayers[i]);
                    ListOfPlayers.RemoveAt(i);
                    //i--;
                }
            }
            return result;
        }

        private void ChangePairing(int player1Game, int player1Pos, int player2Game, int player2Pos)
        {
            int player1EnemyPos = (player1Pos == 0) ? 1 : 0;
            if (player1Game == player2Game)
                return;
            Player player2;
            if (player2Pos == 0)
            {
                player2 = Pairings[player2Game].Player1;
                if (player1EnemyPos == 0)
                {
                    Pairings[player2Game].Player1 = Pairings[player1Game].Player1;
                    Pairings[player1Game].Player1 = player2;
                }
                else
                {
                    Pairings[player2Game].Player1 = Pairings[player1Game].Player2;
                    Pairings[player1Game].Player2 = player2;
                }
            }
            else
            {
                player2 = Pairings[player2Game].Player2;
                if (player1EnemyPos == 0)
                {
                    Pairings[player2Game].Player2 = Pairings[player1Game].Player1;
                    Pairings[player1Game].Player1 = player2;
                }
                else
                {
                    Pairings[player2Game].Player2 = Pairings[player1Game].Player2;
                    Pairings[player1Game].Player2 = player2;
                }
            }
            Pairings[player1Game].Player1.Bye = false;
            Pairings[player1Game].Player2.Bye = false;
            Pairings[player2Game].Player1.Bye = false;
            Pairings[player2Game].Player2.Bye = false;
            Pairings[player1Game].ResultEdited = false;
            Pairings[player2Game].ResultEdited = false;
            if (Pairings[player1Game].Player1 == Bye)
            {
                Pairings[player1Game].Player2.Bye = true;
                Pairings[player1Game].ResultEdited = true;
            }
            else if (Pairings[player1Game].Player2 == Bye)
            {
                Pairings[player1Game].Player1.Bye = true;
                Pairings[player1Game].ResultEdited = true;
            }
            else if (Pairings[player2Game].Player1 == Bye)
            {
                Pairings[player2Game].Player2.Bye = true;
                Pairings[player2Game].ResultEdited = true;
            }
            else if (Pairings[player2Game].Player2 == Bye)
            {
                Pairings[player2Game].Player1.Bye = true;
                Pairings[player2Game].ResultEdited = true;
            }
        }

        #endregion

        public void RemovePlayer(Player player)
        {
            Participants.Remove(player);
        }

        public void CalculateWonFreeticket()
        {
            if (!WonFreeticketsCalculated)
            {
                foreach (var player in (Participants))
                {
                    if (player.WonBye)
                    {
                        player.AddLastEnemy(GetStrongestUnplayedEnemy(player));
                    }
                }
            }
            WonFreeticketsCalculated = true;
        }

        public void DisqualifyPlayer(Player player, string disqualifiedText)
        {
            for (int i = 0; i < player.Enemies.Count; i++)
            {
                Player enemy = player.Enemies[i];
                player.Update(new Result(0, MaxSquadPoints, enemy, false, MaxSquadPoints), i + 1);
                enemy.Update(new Result(MaxSquadPoints, 0, player, false, MaxSquadPoints), i + 1);
            }
            for (int i = 0; i < player.Enemies.Count; i++)
            {
                player.Enemies[i].SumEnemiesStrength();
            }
            player.SumEnemiesStrength();
            player.Disqualify(disqualifiedText);
        }

        public int GetIndexOfPlayer(Player p)
        {
            for (int i = 0; i < Participants.Count; i++)
            {
                if (Participants[i].Nr == p.Nr)
                    return i;
            }
            return 0;
        }
    }
}
