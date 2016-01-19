using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public class FactionPoints
    {
        public Faction PointsFaction { get; set; }
        public int Count { get; set; }
        public FactionPoints(Faction faction)
        {
            PointsFaction = faction;
            Count = 1;
        }

    }

    [Serializable]
    public class Wave
    {
        public string Name { get; private set; }
        public int Count { get; set; }

        public Wave(string name)
        {
            Name = name;
            Count = 1;
        }
    }

    [Serializable]
    public class Statistic
    {
       
        public List<Ship> Ships { get; set; }
        public List<Pilot> Pilots { get; set; }
        public List<Modification> Modifications { get; set; }
        public List<Title> Titles { get; set; }
        public List<Upgrade> Upgrades { get; set; }
        public List<IUpgrade> IUpgrades { get; set; }
        public List<Ship> IShips { get; set; }
        public List<Pilot> IPilots { get; set; }
        public string Path { get; set; }
        public List<Wave> Waves { get; set; }
        public List<int[]> Points { get; set; }
        public List<FactionPoints> FPoints { get; set; }
        public List<string> Squadlists { get; private set; }

        private List<int> shipsPerFaction;
        public List<int> ShipsPerFaction
        {
            get
            {
                if (shipsPerFaction == null)
                {
                    shipsPerFaction = new List<int>();
                    for (int i = 0; i < Player.GetFactions().Length; i++)
                        shipsPerFaction.Add(0);
                }
                return shipsPerFaction;
            }
        }

        public Statistic()
        {
            Path = "";
            Reset();
        }

        public void Reset()
        {
            Ships = new List<Ship>();
            Pilots = new List<Pilot>();
            Modifications = new List<Modification>();
            Titles = new List<Title>();
            Upgrades = new List<Upgrade>();
            IUpgrades = new List<IUpgrade>();
            IShips = new List<Ship>();
            IPilots = new List<Pilot>();
            Waves = new List<Wave>();
            Points = new List<int[]>();
            FPoints = new List<FactionPoints>();
            Squadlists = new List<string>();
        }

        public string Parse(string url, bool add = true, bool overview = false)
        {
            List<Pilot> pilots = new List<Pilot>();

            if (!add && !Squadlists.Contains(url))
            {
                throw new ArgumentException();
            }

            string data = url.Remove(0, url.LastIndexOf("!") + 1);
            string[] ships = data.Split(';');
            int points = 0;
            bool factionAdd = true;
            foreach (string s in ships)
            {
                points += UpdateStatistic(s, add, pilots, factionAdd);
                factionAdd = false;
            }
            UpdatePoints(points, add);
            if (Squadlists == null)
                Squadlists = new List<string>();
            if (add)
                Squadlists.Add(url);
            else
                Squadlists.Remove(url);
            if (overview)
                return CreateString(pilots, points);
            else
                return null;
        }

        private string CreateString(List<Pilot> pilots, int points)
        {
            string list, temp;
            int count;
            List<string> upgrades;
            list = Player.FactionToString(pilots[0].PilotsFaction) + ";" + points + ";";
            while (pilots.Count != 0)
            {
                count = 0;
                temp = pilots[0].Name;
                upgrades = new List<string>();
                for (int i = 0; i < pilots.Count; i++)
                {
                    if (pilots[i].Name == temp)
                    {
                        count++;
                        for (int j = 0; j < pilots[i].Upgrades.Count; j++)
                        {
                            upgrades.Add(pilots[i].Upgrades[j].Gername);
                        }
                    }
                }
                if (count > 1)
                {
                    list += count + "x " + pilots[0].Gername;
                }
                else
                {
                    list += pilots[0].Gername;
                }
                if (upgrades.Count > 0)
                {
                    list += " (";
                    for (int i = 0; i < upgrades.Count; i++)
                        list += upgrades[i] + ", ";
                    list = list.Remove(list.Length - 2);
                    list += ")";
                }
                for (int i = pilots.Count - 1; i >= 0; i-- )
                {
                    if (pilots[i].Name == temp)
                        pilots.RemoveAt(i);
                }
                list += ", ";
            }
            list = list.Remove(list.Length - 2);

            return list;
        }

        private int UpdateStatistic(string ship, bool add, List<Pilot> pilots, bool factionAdd)
        {
            int points = 0;
            string[] parts = ship.Split(':');
            Pilot p = Pilots[Int32.Parse(parts[0])];
            pilots.Add(p.Copy());
            pilots[pilots.Count - 1].Upgrades = new List<IUpgrade>();
            if (add)
            {
                p.Count++;
                p.PilotsShip.Count++;
                if (p.Count == 1)
                    IPilots.Add(p);
                if (p.PilotsShip.Count == 1)
                    IShips.Add(p.PilotsShip);
            }
            else
            {
                p.Count--;
                p.PilotsShip.Count--;
                if (p.Count == 0)
                    IPilots.Remove(p);
                if (p.PilotsShip.Count == 0)
                    IShips.Remove(p.PilotsShip);
            }
            points += p.Points;
            string[] upgrades = parts[1].Split(',');
            int i;
            foreach (string s in upgrades)
            {
                if (s != "")
                {
                    i = Int32.Parse(s);
                    if (i == -1)
                        continue;
                    if (add)
                        Upgrades[i].Count++;
                    else
                        Upgrades[i].Count--;
                    points += Upgrades[i].Points;
                    UpdateIUpgrades(Upgrades[i], add);
                    pilots[pilots.Count - 1].Upgrades.Add(Upgrades[i]);
                }
            }
            i = Int32.Parse(parts[2]);
            //if (p.Faction == Faction.Rebels)
            //{
            //    if (p.PilotsShip.Name == "B-Wing")
            //    {
            //        if (i != -1)
            //        {
            //            if (add)
            //                Titles[i].Count++;
            //            else
            //                Titles[i].Count--;
            //            points += Titles[i].Points;
            //            UpdateIUpgrades(Titles[i], add);
            //        }
            //        i = Int32.Parse(parts[3]);
            //        if (i != -1)
            //        {
            //            if (add)
            //                Modifications[i].Count++;
            //            else
            //                Modifications[i].Count--;
            //            points += Modifications[i].Points;
            //            UpdateIUpgrades(Modifications[i], add);
            //        }
            //    }
            //    else
            //    {
            //        if (i != -1)
            //        {
            //            if (add)
            //                Modifications[i].Count++;
            //            else
            //                Modifications[i].Count--;
            //            points += Modifications[i].Points;
            //            UpdateIUpgrades(Modifications[i], add);
            //        }
            //        i = Int32.Parse(parts[3]);
            //        if (i != -1)
            //        {
            //            if (add)
            //                Titles[i].Count++;
            //            else
            //                Titles[i].Count--;
            //            points += Titles[i].Points;
            //            UpdateIUpgrades(Titles[i], add);
            //        }
            //    }
            //    if (parts.Length == 5)
            //    {
            //        if (parts[4] != "")
            //        {
            //            i = Int32.Parse(parts[4].Remove(0, 2));
            //            if (i != -1)
            //            {
            //                if (add)
            //                    Upgrades[i].Count++;
            //                else
            //                    Upgrades[i].Count--;
            //                points += Upgrades[i].Points;
            //                UpdateIUpgrades(Upgrades[i], add);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            if (i != -1)
            {
                if (add)
                    Titles[i].Count++;
                else
                    Titles[i].Count--;
                points += Titles[i].Points;
                UpdateIUpgrades(Titles[i], add);
                pilots[pilots.Count - 1].Upgrades.Add(Titles[i]);
            }
            i = Int32.Parse(parts[3]);
            if (i != -1)
            {
                if (add)
                    Modifications[i].Count++;
                else
                    Modifications[i].Count--;
                points += Modifications[i].Points;
                UpdateIUpgrades(Modifications[i], add);
                pilots[pilots.Count - 1].Upgrades.Add(Modifications[i]);
            }
            if (parts.Length == 5)
            {
                if (parts[4] != "")
                {
                    //U.-1  M.-1
                    string [] moreParts = parts[4].Split(',');
                    foreach (var part in moreParts)
                    {
                        i = Int32.Parse(part.Remove(0, 2));
                        if (part.StartsWith("U"))
                        {
                            if (i != -1)
                            {
                                if (add)
                                    Upgrades[i].Count++;
                                else
                                    Upgrades[i].Count--;
                                points += Upgrades[i].Points;
                                UpdateIUpgrades(Upgrades[i], add);
                                pilots[pilots.Count - 1].Upgrades.Add(Upgrades[i]);
                            }
                        }
                        else if (part.StartsWith("M"))
                        {
                            if (i != -1)
                            {
                                if (add)
                                    Modifications[i].Count++;
                                else
                                    Modifications[i].Count--;
                                points += Modifications[i].Points;
                                UpdateIUpgrades(Modifications[i], add);
                                pilots[pilots.Count - 1].Upgrades.Add(Modifications[i]);
                            }
                        }
                    }
                }
            }
            //}
            if (factionAdd)
                UpdateFpoints(p.PilotsFaction, add);
            int f = -1;
            if (p.PilotsFaction == Faction.Imperium)
                f = 0;
            else if (p.PilotsFaction == Faction.Rebels)
                f = 1;
            else if (p.PilotsFaction == Faction.Scum)
                f = 2;
            if (add)
                ShipsPerFaction[f]++;
            else
                ShipsPerFaction[f]--;

            UpdateWave(p, add);
            return points;
        }

        private void UpdateFpoints(Faction faction, bool add)
        {
            int a = -1;
            for (int i = 0; i < FPoints.Count; i++)
            {
                if (FPoints[i].PointsFaction == faction)
                {
                    a = i;
                    break;
                }
            }
            if (a == -1)
            {
                FactionPoints fp = new FactionPoints(faction);
                if (add)
                    FPoints.Add(fp);
            }
            else
            {
                if (add)
                    FPoints[a].Count++;
                else
                {
                    FPoints[a].Count--;
                    if (FPoints[a].Count == 0)
                        FPoints.RemoveAt(a);
                }
            }
        }

        private void UpdateIUpgrades(IUpgrade iu, bool add)
        {
            if (iu.Count == 1 && add)
                IUpgrades.Add(iu);
            if (iu.Count == 0 && !add)
                IUpgrades.Remove(iu);
        }

        private void UpdateWave(Pilot pilot, bool add)
        {
            int a = -1;
            for (int i = 0; i < Waves.Count; i++)
            {
                if (Waves[i].Name == pilot.Wave)
                {
                    a = i;
                    break;
                }
            }
            if (a == -1 && add)
            {
                Waves.Add(new Wave(pilot.Wave));
            }
            else
            {
                if (add)
                    Waves[a].Count++;
                else
                {
                    Waves[a].Count--;
                    if (Waves[a].Count == 0)
                        Waves.RemoveAt(a);
                }
            }
        }

        private void UpdatePoints(int points, bool add)
        {
            int a = -1;
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i][0] == points)
                {
                    a = i;
                    break;
                }
            }
            if (a == -1)
            {
                int[] n = { points, 1 };
                if (add)
                    Points.Add(n);
            }
            else
            {
                if (add)
                    Points[a][1]++;
                else
                {
                    Points[a][1]--;
                    if (Points[a][1] == 0)
                        Points.RemoveAt(a);
                }
            }
        }

        public void Sort()
        {
            List<IUpgrade> t = IUpgrades.OrderByDescending(x => x.Count).ThenBy(x => x.Gername).ToList<IUpgrade>();
            IUpgrades = new List<IUpgrade>();
            foreach (IUpgrade u in t)
                IUpgrades.Add(u);

            List<Ship> ta = IShips.OrderByDescending(x => x.Count).ThenBy(x => x.Gername).ToList<Ship>();
            IShips = new List<Ship>();
            foreach (Ship u in ta)
                IShips.Add(u);

            List<Pilot> tb = IPilots.OrderByDescending(x => x.Count).ThenBy(x => x.Gername).ToList<Pilot>();
            IPilots = new List<Pilot>();
            foreach (Pilot u in tb)
                IPilots.Add(u);

            List<FactionPoints> tc = FPoints.OrderByDescending(x => x.Count).ToList<FactionPoints>();
            FPoints = new List<FactionPoints>();
            foreach (FactionPoints u in tc)
                FPoints.Add(u);
        }
    }
}
