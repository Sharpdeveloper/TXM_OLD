using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TXM.Core
{
    [Serializable]
    public enum TournamentCut
    {
        NoCut,
        Top64,
        Top32,
        Top16,
        Top8,
        Top4
    }
    [Serializable]
    public class Tournament : ISerializable
    {
        private int version = 2;

        #region Tournament Information
        public List<Player> Participants { get; set; }
        public List<Player> Teamplayer { get; set; }
        public bool FirstRound { get; set; }
        public List<Pairing> PrePaired { get; set; }
        public string Name { get; set; }
        public List<string> Nicknames { get; internal set; }
        public int MaxPoints { get; set; }
        public List<Round> Rounds { get; internal set; }
        public string FilePath { get; internal set; }
        public string AutoSavePath { get; internal set; }
        public int DisplayedRound { get; set; }
        public TournamentCut Cut { get; set; }
        public bool CutStarted { get; internal set; }
        public bool WonByeCalculated { get; internal set; }
        public List<Pairing> Pairings { get; set; }
        public bool TeamProtection { get; set; }
        public bool Single { get; set; }
        public bool PrintDDGER { get; set; }
        public bool PrintDDENG { get; set; }
        public AbstractRules Rule { get; set; }
        public bool bonus;
        #endregion

        #region GUI_State
        public string ButtonGetResultsText { get; set; }
        public bool ButtonCutState { get; set; }
        #endregion

        #region T3 Information
        public int T3ID { get; set; }
        public string GOEPPVersion { get; internal set; }
        #endregion

        #region internal Fields
        internal static List<int> givenStartNo = new List<int>();
        public Player WonBye = Player.GetWonBye();
        public Player Bye = Player.GetBye();
        private Player Bonus = Player.GetBonus();
        internal List<Player> ListOfPlayers;
        private List<Player>[] PointGroup;
        internal int WonByes;
        internal int currentCountOfPlayer;
        internal List<Player> WinnerLastRound;
        internal bool bye;
        #endregion

        #region Constructors
        public Tournament(string name, int t3ID, string GOEPPversion, AbstractRules rules, bool firstround = true, int maxPoints = 100, TournamentCut cut = TournamentCut.NoCut)
        {
            Player.ResetID();
            Name = name;
            T3ID = t3ID;
            GOEPPVersion = GOEPPversion;
            Participants = new List<Player>();
            Nicknames = new List<string>();
            givenStartNo = new List<int>();
            FirstRound = firstround;
            MaxPoints = maxPoints;
            Cut = cut;
            CutStarted = false;
            WonByeCalculated = false;
            Pairings = new List<Pairing>();
            Rounds = new List<Round>();
            Rule = rules;
            Single = true;
            bonus = false;
        }
        public Tournament(int t3ID, string name, AbstractRules rules, string GOEPPversion = "")
            : this(name, t3ID, GOEPPversion, rules)
        { }
        public Tournament(string name, AbstractRules rules)
            : this(0, name, rules)
        { }

        public Tournament(string name, int maxPoints, TournamentCut cut, AbstractRules rules)
            : this(name, 0, "", rules, true, maxPoints, cut)
        { }
        #endregion

        #region public functions
        public Player GetPlayerByNr(int nr)
        {
            foreach (var p in Participants)
            {
                if (p.ID == nr)
                    return p;
            }
            return null;
        }

        public void AddPlayer(Player player)
        {
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

        public void Sort()
        {
            Participants = Rule.SortTable(Participants);
        }

        internal void SplitTeams()
        {
            foreach (var tp in Teamplayer)
            {
                int a = 0;
                for (int i = 0; i < Participants.Count; i++)
                {
                    if (Participants[i].Team == tp.Team)
                    {
                        a = i;
                        break;
                    }
                }
                tp.Wins = Participants[a].Wins;
                tp.Rank = Participants[a].Rank;
                tp.ModifiedWins = Participants[a].ModifiedWins;
                tp.Losses = Participants[a].Losses;
                tp.Draws = Participants[a].Draws;
                tp.TournamentPoints = Participants[a].TournamentPoints;
                tp.DestroyedPoints = Participants[a].DestroyedPoints;
                tp.LostPoints = Participants[a].LostPoints;
                tp.StrengthOfSchedule = Participants[a].StrengthOfSchedule;
                tp.MarginOfVictory = Participants[a].MarginOfVictory;
            }
            Participants = Teamplayer;
        }

        public List<Pairing> GetSeed(bool start, bool cut)
        {
            if (Rule.IsRandomSeeding)
            {
                return GetSeedRandom(start, cut);
            }
            else
            {
                return GetSeedNonRandom(start, cut);
            }
        }

        public List<Pairing> GetSeedRandom(bool start, bool cut)
        {
            Pairing.ResetTableNr();
            int temp, pos = 0;
            bool swappedGroup = false;

            #region Cut
            if (Cut != TournamentCut.NoCut && (cut || CutStarted))
            {
                CalculateWonBye();
                if (cut)
                {
                    Sort();
                    CutStarted = true;
                    if (Cut == TournamentCut.Top8)
                        currentCountOfPlayer = 8;
                    else if (Cut == TournamentCut.Top16)
                        currentCountOfPlayer = 16;
                    else if (Cut == TournamentCut.Top32)
                        currentCountOfPlayer = 32;
                    else if (Cut == TournamentCut.Top64)
                        currentCountOfPlayer = 64;
                    else
                        currentCountOfPlayer = 4;

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
                    if (!(p.Disqualified || p.Dropped))
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
                    List<Player> wonByes = GetWonByes();
                    for (int i = 0; i < wonByes.Count; i++)
                    {
                        Pairings.Add(new Pairing());
                        Pairings[pos].Player1 = wonByes[i];
                        Pairings[pos].Player2 = WonBye;
                        Pairings[pos].ResultEdited = true;
                        pos++;
                    }
                    if (ListOfPlayers.Count % 2 == 0)
                        bye = false;
                    else
                        bye = true;
                }
                else
                {
                    if (ListOfPlayers.Count % 2 == 0)
                        bye = false;
                    else
                        bye = true;
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
                            if (TeamProtection && (Pairings[pos].Player1.Team != ListOfPlayers[temp].Team || Pairings[pos].Player1.Team == ""))
                            {
                            Pairings[pos].Player2 = ListOfPlayers[temp];
                            break;
                            }
                            else
                                Pairings[pos].Player2 = ListOfPlayers[temp];
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

                    int group = 0;
                    for (int i = 0; i < PointGroup.Length; i++)
                    {
                        while (PointGroup[i].Count >= 2)
                        {
                            Pairings.Add(new Pairing());
                            if (swappedGroup)
                            {
                                temp = PointGroup[i].Count - 1;
                                swappedGroup = false;
                            }
                            else
                            {
                                temp = random.Next(0, PointGroup[i].Count);
                            }
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
                            if (PointGroup.Length == i + 1 && bye)
                            {
                                Pairings.Add(new Pairing());
                                Pairings[pos].Player1 = PointGroup[i][0];
                                Pairings[pos].Player2 = Bye;
                                Pairings[pos].Player1.Bye = true;
                                Pairings[pos].ResultEdited = true;
                            }
                            else
                            {
                                PointGroup[i + 1].Add(PointGroup[i][0]);
                                swappedGroup = true;
                            }
                        }
                    }
                }

                //Prüfen, ob die letzten beiden schon gegeneinander gespielt haben
                temp = Pairings.Count - 1;

                if (Pairings[temp] == null)
                    temp--;

                if (Pairings[temp].Player1.HasPlayedVS(Pairings[temp].Player2) && Pairings[temp].Player1.EnemyCount < Participants.Count)
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

        public List<Pairing> GetSeedNonRandom(bool start, bool cut)
        {
            Pairing.ResetTableNr();
            int temp, pos = 0;

            if (Cut != TournamentCut.NoCut && (cut || CutStarted))
            {
                CalculateWonBye();
                if (cut)
                {
                    Sort();
                    CutStarted = true;
                    if (Cut == TournamentCut.Top8)
                        currentCountOfPlayer = 8;
                    else if (Cut == TournamentCut.Top16)
                        currentCountOfPlayer = 16;
                    else
                        currentCountOfPlayer = 4;

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
                    List<Player> wonFreeTickets = GetWonByes();
                    for (int i = 0; i < wonFreeTickets.Count; i++)
                    {
                        Pairings.Add(new Pairing());
                        Pairings[pos].Player1 = wonFreeTickets[i];
                        Pairings[pos].Player2 = WonBye;
                        Pairings[pos].ResultEdited = true;
                        pos++;
                    }
                    if (ListOfPlayers.Count % 2 == 0)
                        bye = false;
                    else
                        bye = true;
                }
                else
                {
                    if (Participants.Count % 2 == 0)
                        bye = false;
                    else
                        bye = true;
                }
                if (start)
                {
                    Random random = new Random();
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
                            if (TeamProtection && (Pairings[pos].Player1.Team != ListOfPlayers[temp].Team || Pairings[pos].Player1.Team == ""))
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
                        for (int i = 1; i < ListOfPlayers.Count; i++)
                        {
                            if (!Pairings[pos].Player1.HasPlayedVS(ListOfPlayers[i]))
                            {
                                Pairings[pos].Player2 = ListOfPlayers[i];
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

                //Prüfen, ob die letzten beiden schon gegeneinander gespielt haben
                temp = Pairings.Count - 1;

                if (Pairings[temp] == null)
                    temp--;

                if (Pairings[temp].Player1.HasPlayedVS(Pairings[temp].Player2) && Pairings[temp].Player1.EnemyCount < Participants.Count)
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

            return Pairings;
        }

        private void CheckTableNr()
        {
            foreach (Pairing p in Pairings)
            {
                if (p.Player1.TableNo != 0 && p.Player1.Bye == false && p.TableNr != p.Player1.TableNo)
                {
                    if ((p.Player2.TableNo != 0 && p.Player1.TableNo < p.Player2.TableNo) || p.Player2.TableNo == 0)
                    {
                        ChangePairings(p.Player1.TableNo, p.TableNr);
                    }
                    else if (p.Player2.TableNo != 0 && p.Player1.TableNo > p.Player2.TableNo && p.Player2.TableNo != p.TableNr)
                    {
                        ChangePairings(p.Player2.TableNo, p.TableNr);
                    }
                }
                else if (p.Player2.TableNo != 0)
                {
                    ChangePairings(p.Player2.TableNo, p.TableNr);
                }
            }
        }

        private void ChangePairings(int a, int b)
        {
            int tablenra = a;
            int tablenrb = b;
            a--;
            b--;
            if(a >= Pairings.Count)
            {
                a = Pairings.Count-1;
            }
            if(b >= Pairings.Count)
            {
                b = Pairings.Count - 1;
            }

            Player a1 = Pairings[a].Player1;
            Player a2 = Pairings[a].Player2;
            bool result = Pairings[a].ResultEdited;
            Pairings[a].Player1 = Pairings[b].Player1;
            Pairings[a].Player2 = Pairings[b].Player2;
            Pairings[a].ResultEdited = Pairings[b].ResultEdited;
            Pairings[b].Player1 = a1;
            Pairings[b].Player2 = a2;
            Pairings[b].ResultEdited = result;
            Pairings[a].TableNr = tablenra;
            Pairings[b].TableNr = tablenrb;
        }

        //TODO: Ändern auf Rules
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
            int winnerID = 0;
            bool winner;
            WinnerLastRound = new List<Player>();
            if(bonus)
            {
                foreach (Pairing pairing in results)
                {
                    r = new Result(0, 0, Bonus, MaxPoints, 1, pairing.Player1Score);
                    Rule.AddBonus(pairing.Player1, r);
                }
                return;
            }
            if (!update)
            {
                foreach (Pairing pairing in results)
                {
                    if (pairing.Winner == "Player 1")
                        winnerID = pairing.Player1.ID;
                    else if(pairing.Winner == "Player 2")
                        winnerID = pairing.Player2.ID;
                    else
                        winnerID = (pairing.Player1Score > pairing.Player2Score) ? pairing.Player1.ID : pairing.Player2.ID;
                    r = new Result(pairing.Player1Score, pairing.Player2Score, pairing.Player2, MaxPoints, winnerID, pairing.Player1Points);
                    winner = Rule.AddResult(pairing.Player1, r);
                    if (winner)
                        WinnerLastRound.Add(pairing.Player1);
                    if (pairing.Player2 != WonBye && pairing.Player2 != Bye)
                    {
                        r = new Result(pairing.Player2Score, pairing.Player1Score, pairing.Player1, MaxPoints, winnerID, pairing.Player2Points);
                        winner = Rule.AddResult(pairing.Player2, r);
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
                        if (results[i].Winner == "Player 1")
                            winnerID = results[i].Player1.ID;
                        else if (results[i].Winner == "Player 2")
                            winnerID = results[i].Player2.ID;
                        else
                            winnerID = (results[i].Player1Score > results[i].Player2Score) ? results[i].Player1.ID : results[i].Player2.ID;
                        r = new Result(results[i].Player1Score, results[i].Player2Score, results[i].Player2, MaxPoints, winnerID, results[i].Player1Points);
                        Rule.Update(results[i].Player1, r, DisplayedRound);
                        r = new Result(results[i].Player2Score, results[i].Player1Score, results[i].Player1, MaxPoints, winnerID, results[i].Player2Points);
                        Rule.Update(results[i].Player2, r, DisplayedRound);
                    }
                }
            }
            foreach (Player player in Participants)
                player.SumStrengthOfSchedule();
            foreach (Player player in Participants)
                player.SumExtendedStrengthOfSchedule();
            for (int i = 0; i < results.Count; i++)
                results[i].ResultEdited = false;
            if (update)
                Rounds[DisplayedRound - 1].Pairings = results;
            else
                Rounds[Rounds.Count - 1].Pairings = results;
            PrePaired = new List<Pairing>();
			Sort();
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

        private int GetIndexOfInRound(Player player, int round)
        {
            for (int i = 0; i < Rounds[round].Participants.Count; i++)
            {
                if (Rounds[round].Participants[i].Equals(player))
                    return i;
            }
            return -1;
        }

        private void CountGroups()
        {
            List<int> v = new List<int>();
            foreach (Player player in ListOfPlayers)
            {
                if (!v.Contains(player.TournamentPoints))
                {
                    v.Add(player.TournamentPoints);
                }
            }
            PointGroup = new List<Player>[v.Count];
            v.OrderBy(x => x);
            for (int i = 0; i < v.Count; i++)
            {
                PointGroup[i] = new List<Player>();
                foreach (Player player in ListOfPlayers)
                {
                    if (v[i] == player.TournamentPoints)
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
            if(!Single)
            {
                Teamplayer = Participants;
                Participants = new List<Player>();
                foreach( var tp in Teamplayer)
                {
                    int a = -1;
                    for(int i = 0; i < Participants.Count; i++)
                    {
                        if(Participants[i].Team == tp.Team)
                        {
                            a = i;
                            break;
                        }
                    }
                    if (a == -1)
                    {
                        Participants.Add(new Player(tp.Team) { Team = tp.Team, Forename = tp.Forename, Faction = tp.Faction });
                    }
                    else
                        Participants[a].Forename += ", " + tp.Forename;
                }
            }
            WonByes = 0;
            foreach (Player p in Participants)
            {
                if (p.WonBye)
                    WonByes++;
            }
        }

        private List<Player> GetWonByes()
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

        public void CalculateWonBye()
        {
            if (!WonByeCalculated)
            {
                foreach (var player in (Participants))
                {
                    if (player.WonBye)
                    {
                        player.AddLastEnemy(GetStrongestUnplayedEnemy(player));
                    }
                }
            }
            WonByeCalculated = true;
        }

        public void DisqualifyPlayer(Player player)
        {
            for (int i = 0; i < player.Enemies.Count; i++)
            {
                Player enemy = player.Enemies[i];
                Rule.Update(player, new Result(0, MaxPoints, enemy, MaxPoints, enemy.ID), i + 1);
                Rule.Update(enemy, new Result(MaxPoints, 0, player, MaxPoints, enemy.ID), i + 1);
            }
            for (int i = 0; i < player.Enemies.Count; i++)
            {
                player.Enemies[i].SumStrengthOfSchedule();
                player.Enemies[i].SumExtendedStrengthOfSchedule();
            }
            player.SumStrengthOfSchedule();
            player.SumExtendedStrengthOfSchedule();
            player.Disqualify();
        }

        public void DropPlayer(Player player)
        {
            player.Drop();
        }

        public int GetIndexOfPlayer(Player p)
        {
            for (int i = 0; i < Participants.Count; i++)
            {
                if (Participants[i].ID == p.ID)
                    return i;
            }
            return 0;
        }

        public List<Pairing> GetBonusSeed()
        {
            Pairings = new List<Pairing>();

            foreach(var p in Participants)
            {
                Pairings.Add(new Pairing() { Player1 = p, Player2 = Bonus });
            }

            if (Rounds == null)
                Rounds = new List<Round>();
            Rounds.Add(new Round(Pairings, Participants));
            DisplayedRound = Rounds.Count;
            bonus = true;
            return Pairings;
        }

        public Tournament(SerializationInfo info, StreamingContext context)
        {
            version = (int)info.GetValue("Tournament_Version", typeof(int));
            WonBye = Player.GetWonBye();
            Bye = Player.GetBye();
            Bonus = Player.GetBonus();
            if (version == 0)
            {
                Participants = (List<Player>)info.GetValue("Tournament_Participants", typeof(List<Player>));
                Teamplayer = (List<Player>)info.GetValue("Tournament_Teamplayer", typeof(List<Player>));
                FirstRound = (bool)info.GetValue("Tournament_FirstRound", typeof(bool));
                PrePaired = (List<Pairing>)info.GetValue("Tournament_PrePaired", typeof(List<Pairing>));
                Name = (string)info.GetValue("Tournament_Name", typeof(string));
                Nicknames = (List<string>)info.GetValue("Tournament_Nicknames", typeof(List<string>));
                MaxPoints = (int)info.GetValue("Tournament_MaxPoints", typeof(int));
                Rounds = (List<Round>)info.GetValue("Tournament_Rounds", typeof(List<Round>));
                FilePath = (string)info.GetValue("Tournament_FilePath", typeof(string));
                AutoSavePath = (string)info.GetValue("Tournament_AutoSavePath", typeof(string));
                DisplayedRound = (int)info.GetValue("Tournament_DisplayedRound", typeof(int));
                Cut = (TournamentCut)info.GetValue("Tournament_Cut", typeof(TournamentCut));
                CutStarted = (bool)info.GetValue("Tournament_CutStarted", typeof(bool));
                WonByeCalculated = (bool)info.GetValue("Tournament_WonByeCalculated", typeof(bool));
                Pairings = (List<Pairing>)info.GetValue("Tournament_Pairings", typeof(List<Pairing>));
                TeamProtection = (bool)info.GetValue("Tournament_TeamProtection", typeof(bool));
                Single = (bool)info.GetValue("Tournament_Single", typeof(bool));
                PrintDDGER = (bool)info.GetValue("Tournament_PrintDDGER", typeof(bool));
                PrintDDENG = (bool)info.GetValue("Tournament_PrintDDENG", typeof(bool));
                Rule = (AbstractRules)info.GetValue("Tournament_Rule", typeof(AbstractRules));
                bool ButtonGetResultState = (bool)info.GetValue("Tournament_ButtonGetResultState", typeof(bool));
                bool ButtonNextRoundState = (bool)info.GetValue("Tournament_ButtonNextRoundState", typeof(bool));
                ButtonCutState = (bool)info.GetValue("Tournament_ButtonCutState", typeof(bool));
                T3ID = (int)info.GetValue("Tournament_T3ID", typeof(int));
                GOEPPVersion = (string)info.GetValue("Tournament_GOEPPVersion", typeof(string));
                givenStartNo = (List<int>)info.GetValue("Tournament_givenStartNo", typeof(List<int>));
                ListOfPlayers = (List<Player>)info.GetValue("Tournament_ListOfPlayers", typeof(List<Player>));
                PointGroup = (List<Player>[])info.GetValue("Tournament_PointGroup", typeof(List<Player>[]));
                WonByes = (int)info.GetValue("Tournament_WonByes", typeof(int));
                currentCountOfPlayer = (int)info.GetValue("Tournament_currentCountOfPlayer", typeof(int));
                WinnerLastRound = (List<Player>)info.GetValue("Tournament_WinnerLastRound", typeof(List<Player>));
                bye = (bool)info.GetValue("Tournament_bye", typeof(bool));
                if(ButtonNextRoundState == true)
                {
                    ButtonGetResultsText = "Next Round";
                }
                else if(ButtonGetResultState == true)
                {
                    ButtonGetResultsText = "Get Results";
                }
                else
                {
                    ButtonGetResultsText = "Start Tournament";
                }
                bonus = false;
                version = 2;
            }
            else if (version == 1)
            {
                Participants = (List<Player>)info.GetValue("Tournament_Participants", typeof(List<Player>));
                Teamplayer = (List<Player>)info.GetValue("Tournament_Teamplayer", typeof(List<Player>));
                FirstRound = (bool)info.GetValue("Tournament_FirstRound", typeof(bool));
                PrePaired = (List<Pairing>)info.GetValue("Tournament_PrePaired", typeof(List<Pairing>));
                Name = (string)info.GetValue("Tournament_Name", typeof(string));
                Nicknames = (List<string>)info.GetValue("Tournament_Nicknames", typeof(List<string>));
                MaxPoints = (int)info.GetValue("Tournament_MaxPoints", typeof(int));
                Rounds = (List<Round>)info.GetValue("Tournament_Rounds", typeof(List<Round>));
                FilePath = (string)info.GetValue("Tournament_FilePath", typeof(string));
                AutoSavePath = (string)info.GetValue("Tournament_AutoSavePath", typeof(string));
                DisplayedRound = (int)info.GetValue("Tournament_DisplayedRound", typeof(int));
                Cut = (TournamentCut)info.GetValue("Tournament_Cut", typeof(TournamentCut));
                CutStarted = (bool)info.GetValue("Tournament_CutStarted", typeof(bool));
                WonByeCalculated = (bool)info.GetValue("Tournament_WonByeCalculated", typeof(bool));
                Pairings = (List<Pairing>)info.GetValue("Tournament_Pairings", typeof(List<Pairing>));
                TeamProtection = (bool)info.GetValue("Tournament_TeamProtection", typeof(bool));
                Single = (bool)info.GetValue("Tournament_Single", typeof(bool));
                PrintDDGER = (bool)info.GetValue("Tournament_PrintDDGER", typeof(bool));
                PrintDDENG = (bool)info.GetValue("Tournament_PrintDDENG", typeof(bool));
                Rule = (AbstractRules)info.GetValue("Tournament_Rule", typeof(AbstractRules));
                ButtonGetResultsText = (string)info.GetValue("Tournament_ButtonGetResultsText", typeof(string));
                ButtonCutState = (bool)info.GetValue("Tournament_ButtonCutState", typeof(bool));
                T3ID = (int)info.GetValue("Tournament_T3ID", typeof(int));
                GOEPPVersion = (string)info.GetValue("Tournament_GOEPPVersion", typeof(string));
                givenStartNo = (List<int>)info.GetValue("Tournament_givenStartNo", typeof(List<int>));
                ListOfPlayers = (List<Player>)info.GetValue("Tournament_ListOfPlayers", typeof(List<Player>));
                PointGroup = (List<Player>[])info.GetValue("Tournament_PointGroup", typeof(List<Player>[]));
                WonByes = (int)info.GetValue("Tournament_WonByes", typeof(int));
                currentCountOfPlayer = (int)info.GetValue("Tournament_currentCountOfPlayer", typeof(int));
                WinnerLastRound = (List<Player>)info.GetValue("Tournament_WinnerLastRound", typeof(List<Player>));
                bye = (bool)info.GetValue("Tournament_bye", typeof(bool));
                bonus = false;
                version = 2;
            }
            else if (version == 2)
            {
                Participants = (List<Player>)info.GetValue("Tournament_Participants", typeof(List<Player>));
                Teamplayer = (List<Player>)info.GetValue("Tournament_Teamplayer", typeof(List<Player>));
                FirstRound = (bool)info.GetValue("Tournament_FirstRound", typeof(bool));
                PrePaired = (List<Pairing>)info.GetValue("Tournament_PrePaired", typeof(List<Pairing>));
                Name = (string)info.GetValue("Tournament_Name", typeof(string));
                Nicknames = (List<string>)info.GetValue("Tournament_Nicknames", typeof(List<string>));
                MaxPoints = (int)info.GetValue("Tournament_MaxPoints", typeof(int));
                Rounds = (List<Round>)info.GetValue("Tournament_Rounds", typeof(List<Round>));
                FilePath = (string)info.GetValue("Tournament_FilePath", typeof(string));
                AutoSavePath = (string)info.GetValue("Tournament_AutoSavePath", typeof(string));
                DisplayedRound = (int)info.GetValue("Tournament_DisplayedRound", typeof(int));
                Cut = (TournamentCut)info.GetValue("Tournament_Cut", typeof(TournamentCut));
                CutStarted = (bool)info.GetValue("Tournament_CutStarted", typeof(bool));
                WonByeCalculated = (bool)info.GetValue("Tournament_WonByeCalculated", typeof(bool));
                Pairings = (List<Pairing>)info.GetValue("Tournament_Pairings", typeof(List<Pairing>));
                TeamProtection = (bool)info.GetValue("Tournament_TeamProtection", typeof(bool));
                Single = (bool)info.GetValue("Tournament_Single", typeof(bool));
                PrintDDGER = (bool)info.GetValue("Tournament_PrintDDGER", typeof(bool));
                PrintDDENG = (bool)info.GetValue("Tournament_PrintDDENG", typeof(bool));
                Rule = (AbstractRules)info.GetValue("Tournament_Rule", typeof(AbstractRules));
                ButtonGetResultsText = (string)info.GetValue("Tournament_ButtonGetResultsText", typeof(string));
                ButtonCutState = (bool)info.GetValue("Tournament_ButtonCutState", typeof(bool));
                T3ID = (int)info.GetValue("Tournament_T3ID", typeof(int));
                GOEPPVersion = (string)info.GetValue("Tournament_GOEPPVersion", typeof(string));
                givenStartNo = (List<int>)info.GetValue("Tournament_givenStartNo", typeof(List<int>));
                ListOfPlayers = (List<Player>)info.GetValue("Tournament_ListOfPlayers", typeof(List<Player>));
                PointGroup = (List<Player>[])info.GetValue("Tournament_PointGroup", typeof(List<Player>[]));
                WonByes = (int)info.GetValue("Tournament_WonByes", typeof(int));
                currentCountOfPlayer = (int)info.GetValue("Tournament_currentCountOfPlayer", typeof(int));
                WinnerLastRound = (List<Player>)info.GetValue("Tournament_WinnerLastRound", typeof(List<Player>));
                bye = (bool)info.GetValue("Tournament_bye", typeof(bool));
                bonus = (bool)info.GetValue("Tournament_bonus", typeof(bool));
            }
            Rule = AbstractRules.GetRule(Rule.GetName());
        }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Tournament_Version", version, typeof(int));
			info.AddValue("Tournament_Participants", Participants, typeof(List<Player>));
			info.AddValue("Tournament_Teamplayer", Teamplayer, typeof(List<Player>));
			info.AddValue("Tournament_FirstRound", FirstRound, typeof(bool));
			info.AddValue("Tournament_PrePaired", PrePaired, typeof(List<Pairing>));
			info.AddValue("Tournament_Name", Name, typeof(string));
			info.AddValue("Tournament_Nicknames", Nicknames, typeof(List<string>));
			info.AddValue("Tournament_MaxPoints", MaxPoints, typeof(int));
			info.AddValue("Tournament_Rounds", Rounds, typeof(List<Round>));
			info.AddValue("Tournament_FilePath", FilePath, typeof(string));
			info.AddValue("Tournament_AutoSavePath", AutoSavePath, typeof(string));
			info.AddValue("Tournament_DisplayedRound", DisplayedRound, typeof(int));
			info.AddValue("Tournament_Cut", Cut, typeof(TournamentCut));
			info.AddValue("Tournament_CutStarted", CutStarted, typeof(bool));
			info.AddValue("Tournament_WonByeCalculated", WonByeCalculated, typeof(bool));
			info.AddValue("Tournament_Pairings", Pairings, typeof(List<Pairing>));
			info.AddValue("Tournament_TeamProtection", TeamProtection, typeof(bool));
			info.AddValue("Tournament_Single", Single, typeof(bool));
			info.AddValue("Tournament_PrintDDGER", PrintDDGER, typeof(bool));
			info.AddValue("Tournament_PrintDDENG", PrintDDENG, typeof(bool));
			info.AddValue("Tournament_Rule", Rule, typeof(AbstractRules));
			info.AddValue("Tournament_ButtonGetResultsText", ButtonGetResultsText, typeof(string));
			info.AddValue("Tournament_ButtonCutState", ButtonCutState, typeof(bool));
			info.AddValue("Tournament_T3ID", T3ID, typeof(int));
			info.AddValue("Tournament_GOEPPVersion", GOEPPVersion, typeof(string));
			info.AddValue("Tournament_givenStartNo", givenStartNo, typeof(List<int>));
			info.AddValue("Tournament_ListOfPlayers", ListOfPlayers, typeof(List<Player>));
			info.AddValue("Tournament_PointGroup", PointGroup, typeof(List<Player>[]));
			info.AddValue("Tournament_WonByes", WonByes, typeof(int));
			info.AddValue("Tournament_currentCountOfPlayer", currentCountOfPlayer, typeof(int));
			info.AddValue("Tournament_WinnerLastRound", WinnerLastRound, typeof(List<Player>));
			info.AddValue("Tournament_bye", bye, typeof(bool));
            info.AddValue("Tournament_bonus", bonus, typeof(bool));
        }
    }
}
