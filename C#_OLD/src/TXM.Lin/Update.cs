using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net;

//using WPF_Dialogs.Dialogs;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TXM.Core;

namespace TXM.Lin
{
    public static class Update
    {
        private static string tempPath;
        private static string cards;
        private static string translations;
        private static Statistic stats;
        public static void LoadFiles()
        {
            SetTempPath();
			try
			{
	            WebClient wc = new WebClient();
	            wc.DownloadFile(Settings.SQUADFILECARDS, cards);
	            wc.DownloadFile(Settings.SQUADFILETRANSLATION, translations);
			}
			catch(Exception e)
			{
				MessageBox.Show ("Es fehlen Leider Zertifikate um diese Funktion nutzen zu können. Bitte gegeben Sie folgendes in der Konsole/im Terminal ein: mozroots --import --ask-remove");
			}

        }
        public static Statistic LoadContents(bool update = false, bool silence = false)
        {
            SetTempPath();
            if (!File.Exists(cards))
            {
                if (silence)
                    LoadFiles();
                else
                {
                    MessageBox.Show("Bitte erst die Daten aktualisieren.");
                    return null;
                }
            }
            if (File.Exists(Path.Combine(tempPath, Settings.BINFILE)) && !update && new FileInfo(Path.Combine(tempPath, Settings.BINFILE)).Length != 0)
                return Deserialize();
            else
            {
                string text;
                int posStart = 0;
                int posEnd;
                stats = new Statistic();

                using (StreamReader sr = new StreamReader(cards, Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }

                posStart = text.IndexOf("ships:", posStart);
                posEnd = text.IndexOf("pilotsById:", posStart);
                stats.Ships = GetShips(text.Substring(posStart, posEnd - posStart));

                posStart = posEnd;
                posEnd = text.IndexOf("upgradesById:", posStart);
                stats.Pilots = GetPilots(text.Substring(posStart, posEnd - posStart));

                posStart = posEnd;
                posEnd = text.IndexOf("modificationsById:", posStart);
                stats.Upgrades = GetUpgrades(text.Substring(posStart, posEnd - posStart));

                posStart = posEnd;
                posEnd = text.IndexOf("titlesById:", posStart);
                stats.Modifications = GetModifications(text.Substring(posStart, posEnd - posStart));

                posStart = posEnd;
                posEnd = text.IndexOf("exportObj.setupCardData = (basic_cards", posStart);
                stats.Titles = GetTitles(text.Substring(posStart, posEnd - posStart));

                text = "";
                posStart = 0;
                posEnd = 0;

                using (StreamReader sr = new StreamReader(translations))
                {
                    text = sr.ReadToEnd();
                }

                posStart = text.IndexOf("pilot_translations", posStart);
                posEnd = text.IndexOf("upgrade_translations", posStart);
                Translate(text.Substring(posStart, posEnd - posStart), 'p');

                posStart = posEnd;
                posEnd = text.IndexOf("modification_translations", posStart);
                Translate(text.Substring(posStart, posEnd - posStart), 'u');

                posStart = posEnd;
                posEnd = text.IndexOf("title_translations", posStart);
                Translate(text.Substring(posStart, posEnd - posStart), 'm');

                posStart = posEnd;
                posEnd = text.IndexOf("exportObj.setupCardData", posStart);
                Translate(text.Substring(posStart, posEnd - posStart), 't');

                Serialize(stats);
                return stats;
            }
        }
        private static void Translate(string data, char what)
        {
            int s = 0;
            int e = data.IndexOf("\n", s); ;
            List<string> lines = new List<string>();
            do
            {
                lines.Add(data.Substring(s, e - s));
                s = e + 2;
                e = data.IndexOf("\n", s);
            } while (e > 0);
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("name: "))
                {
                    int a = lines[i - 1].Contains("ship") ? i - 2 : i - 1;
                    TranslatePilot(lines[a], lines[i], what);
                }
            }
        }
        private static void TranslatePilot(string uk, string de, char what)
        {
            int s = uk.IndexOf("\"", 0) + 1;
            int e = uk.IndexOf("\"", s);
            uk = uk.Substring(s, e - s);
            s = de.IndexOf("\"", 0) + 1;
            e = de.IndexOf("\"", s);
            de = de.Substring(s, e - s);
            if (what == 'p')
            {
                foreach (Pilot p in stats.Pilots)
                {
                    if (p.Name == uk)
                    {
                        p.Gername = de;
                        break;
                    }
                }
            }
            else if (what == 'u')
            {
                foreach (Upgrade p in stats.Upgrades)
                {
                    if (p.Name == uk)
                    {
                        p.Gername = de;
                        break;
                    }
                }
            }
            else if (what == 'm')
            {
                foreach (Modification p in stats.Modifications)
                {
                    if (p.Name == uk)
                    {
                        p.Gername = de;
                        break;
                    }
                }
            }
            else if (what == 't')
            {
                foreach (Title p in stats.Titles)
                {
                    if (p.Name == uk)
                    {
                        p.Gername = de;
                        break;
                    }
                }
            }
        }
        private static List<Ship> GetShips(string data)
        {
            List<Ship> ships = new List<Ship>();
            int start = data.IndexOf(" name:", 0);
            int end;

            do
            {
                end = data.IndexOf(" name:", start + 1);
                if (end <= 0)
                    end = data.Length;
                ships.Add(ConvertToShip(data.Substring(start, end - start)));
                start = end;
            } while (start != data.Length);

            return ships;
        }
        private static Ship ConvertToShip(string data)
        {
            int s = 0;
            int e;
            s = data.IndexOf("\"", s) + 1;
            e = data.IndexOf("\"", s);
            string name = data.Substring(s, e - s);

            s = data.IndexOf("[", s) + 1;
            e = data.IndexOf("]", s);
            List<Faction> factions = GetFactions(data.Substring(s, e - s));

            s = data.IndexOf(":", s) + 2;
            int attack = Int32.Parse(data.Substring(s, 1));

            s = data.IndexOf(":", s) + 2;
            int agility = Int32.Parse(data.Substring(s, 1));

            s = data.IndexOf(":", s) + 2;
            int hull = Int32.Parse(data.Substring(s, 1));

            s = data.IndexOf(":", s) + 2;
            int shields = Int32.Parse(data.Substring(s, 1));

            s = data.IndexOf("[", s);
            e = data.IndexOf("]", s);
            List<XWingAction> actions = GetActions(data.Substring(s, e - s));

            s = data.IndexOf("[", e);
            e = s + 1;
            for (int i = 0; i < 5; i++)
                e = data.IndexOf("]", e) + 2;
            List<int[]> maneuvers;
            if (s > 0)
                maneuvers = GetManeuvers(data.Substring(s, e - s));
            else
            {
                maneuvers = new List<int[]>();
                maneuvers.Add(new int[6]);
            }
            return new Ship(name, factions, attack, agility, hull, shields, actions, maneuvers);
        }
        private static List<int[]> GetManeuvers(string data)
        {
            List<int[]> maneuvers = new List<int[]>();
            int start = data.IndexOf("[", 0) + 1;
            start = data.IndexOf("[", start);
            int end;
			while (start > 0)
            {
                end = data.IndexOf("]", start);
                int[] row = GetSpeeds(data.Substring(start, end - start));
                maneuvers.Add(row);
                start = data.IndexOf("[", end + 1) + 1;
            } 
            return maneuvers;
        }
        private static int[] GetSpeeds(string data)
        {
            int[] speeds = new int[6];
            int pos = 0;
            int oldpos = 0;
            for (int i = 0; i < 5; i++)
            {
                pos = data.IndexOf(",", pos + 1);
                try
                {
                    speeds[i] = Int32.Parse(data.Substring(pos - 1, 1));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    speeds[i] = Int32.Parse(data.Substring(oldpos + 2, 1));
                }
                oldpos = pos;
            }
            if (pos != -1)
                speeds[5] = Int32.Parse(data.Substring(pos + 2, 1));
            else
                speeds[5] = 0;
            return speeds;
        }
        private static List<XWingAction> GetActions(string data)
        {
            List<XWingAction> actions = new List<XWingAction>();
            int start = data.IndexOf("\"", 0) + 1;
            int end;
            string f;
            do
            {
                end = data.IndexOf("\"", start);
                f = data.Substring(start, end - start);
                if (f == "Target Lock")
                    actions.Add(XWingAction.TargetLock);
                else if (f == "Boost")
                    actions.Add(XWingAction.Boost);
                else if (f == "Evade")
                    actions.Add(XWingAction.Evade);
                else if (f == "Barrel Roll")
                    actions.Add(XWingAction.BarrelRoll);
                else if (f == "Recover")
                    actions.Add(XWingAction.Recover);
                else if (f == "Reinforce")
                    actions.Add(XWingAction.Reinforce);
                else if (f == "Coordinate")
                    actions.Add(XWingAction.Coordinate);
                else if (f == "Jam")
                    actions.Add(XWingAction.Jam);
                else if (f == "Cloak")
                    actions.Add(XWingAction.Cloak);
                else
                    actions.Add(XWingAction.Focus);
                start = data.IndexOf("\"", end + 1) + 1;
            } while (start > 0);

            return actions;
        }
        private static List<Faction> GetFactions(string data)
        {
            List<Faction> factions = new List<Faction>();
            int start = data.IndexOf("\"", 0) + 1;
            int end;
            string f;
            do
            {
                end = data.IndexOf("\"", start);
                f = data.Substring(start, end - start);
                if (f == "Rebel Alliance")
                    factions.Add(Faction.Rebels);
                else if (f == "Galactic Empire")
                    factions.Add(Faction.Imperium);
                else
                    factions.Add(Faction.Scum);
                start = data.IndexOf("\"", end + 1) + 1;
            } while (start > 0);

            return factions;
        }
        private static List<Pilot> GetPilots(string data)
        {
            List<Pilot> pilots = new List<Pilot>();
            int start = data.IndexOf(" name:", 0);
            int end;

            do
            {
                end = data.IndexOf(" name:", start + 1);
                if (end <= 0)
                    end = data.Length;
                pilots.Add(ConvertToPilot(data.Substring(start, end - start)));
                start = end;
            } while (start != data.Length);

            return pilots;
        }
        private static Pilot ConvertToPilot(string data)
        {
            int s = 0;
            int e = data.IndexOf("\n", s); ;
            List<string> pilotvalues = new List<string>();
            do
            {
                pilotvalues.Add(data.Substring(s, e - s));
                s = e + 2;
                e = data.IndexOf("\n", s);
            } while (e > 0);

            string name = "";
            Faction faction = Faction.Scum;
            int id = -1;
            List<string> sources = new List<string>();
            bool unique = false;
            Ship ship = null;
            int skill = -1;
            int points = -1;
            List<Slot> slots = new List<Slot>();

            for (int i = 0; i < pilotvalues.Count; i++)
            {
                string str = pilotvalues[i];
                if (str.Contains(" name") && !(str.Contains("canonical")))
                {
					if (str.Contains ("\""))
					{
						s = str.IndexOf ("\"", 0) + 1;
						e = str.IndexOf ("\"", s);
					}
					else
					{
						s = str.IndexOf ("\'", 0) + 1;
						e = str.IndexOf ("\'", s);
					}

                    name = str.Substring(s, e - s);
                }
                else if (str.Contains("faction"))
                {
                    s = str.IndexOf("\"", 0);
                    e = str.IndexOf("\"", s + 1) + 1;
                    faction = GetFactions(str.Substring(s, e - s))[0];
                }
                else if (str.Contains(" id"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        id = Int32.Parse(str.Substring(s, 3));
                    }
                    catch (ArgumentOutOfRangeException exce)
                    {
                        try
                        {
                            id = Int32.Parse(str.Substring(s, 2));
                        }
                        catch (ArgumentOutOfRangeException excep)
                        {
                            id = Int32.Parse(str.Substring(s, 1));
                        }
                    }
                }
                else if (str.Contains("sources"))
                {
                    //s = str.IndexOf("[", 0);
                    //e = str.IndexOf("]", s);
                    //sources = GetSources(str.Substring(s, e - s));
                }
                else if (str.Contains("unique"))
                {
                    unique = true;
                }
                else if (str.Contains("ship"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    ship = GetShip(str.Substring(s, e - s));
                }
                else if (str.Contains("skill"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    skill = Int32.Parse(str.Substring(s, 1));
                }
                else if (str.Contains("points"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        points = Int32.Parse(str.Substring(s, 2));
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        points = Int32.Parse(str.Substring(s, 1));
                    }
                }
                else if (str.Contains("slots"))
                {
                    for (int j = i; j < pilotvalues.Count; j++)
                        str += pilotvalues[j];
                    slots = GetSlots(str);
                    break;
                }
            }

            return new Pilot(name, faction, id, sources, unique, ship, skill, points, slots);
        }
        private static List<string> GetSources(string data)
        {
            List<string> sources = new List<string>();
            int start = data.IndexOf("\"", 0) + 1;
            int end;
            string f;
            do
            {
                end = data.IndexOf("\"", start);
                sources.Add(data.Substring(start, end - start));
                start = data.IndexOf("\"", end + 1) + 1;
            } while (start > 0);

            return sources;
        }
        private static List<Slot> GetSlots(string data)
        {
            List<Slot> slots = new List<Slot>();
            int start = data.IndexOf("\"", 0) + 1;
            if (start <= 0)
            {
                slots.Add(Slot.None);
                return slots;
            }
            int end;
            string f;
            do
            {
                end = data.IndexOf("\"", start);
                f = data.Substring(start, end - start);
                if (f == "Elite")
                    slots.Add(Slot.Elite);
                else if (f == "Torpedo")
                    slots.Add(Slot.Torpedo);
                else if (f == "Astromech")
                    slots.Add(Slot.Astromech);
                else if (f == "Turret")
                    slots.Add(Slot.Turret);
                else if (f == "Missile")
                    slots.Add(Slot.Missile);
                else if (f == "Crew")
                    slots.Add(Slot.Crew);
                else if (f == "Cannon")
                    slots.Add(Slot.Cannon);
                else if (f == "Bomb")
                    slots.Add(Slot.Bomb);
                else if (f == "System")
                    slots.Add(Slot.System);
                else if (f == "Hardpoint")
                    slots.Add(Slot.Hardpoint);
                else if (f == "Team")
                    slots.Add(Slot.Team);
                else if (f == "Illicit")
                    slots.Add(Slot.Illicit);
                else if (f == "Salvaged Astromech")
                    slots.Add(Slot.SalvagedAstromech);
                else
                    slots.Add(Slot.None);
                start = data.IndexOf("\"", end + 1) + 1;
            } while (start > 0);

            return slots;
        }
        private static Ship GetShip(string shipName)
        {
            foreach (Ship s in stats.Ships)
            {
                if (s.Name == shipName)
                    return s;
            }
            return null;
        }
        private static List<Upgrade> GetUpgrades(string data)
        {
            List<Upgrade> upgrades = new List<Upgrade>();
            int start = data.IndexOf(" name:", 0);
            int end;

            do
            {
                end = data.IndexOf(" name:", start + 1);
                if (end <= 0)
                    end = data.Length;
                upgrades.Add(ConvertToUpgrade(data.Substring(start, end - start)));
                start = end;
            } while (start != data.Length);

            return upgrades;
        }
        private static Upgrade ConvertToUpgrade(string data)
        {
            int s = 0;
            int e = data.IndexOf("\n", s); ;
            List<string> pilotvalues = new List<string>();
            do
            {
                pilotvalues.Add(data.Substring(s, e - s));
                s = e + 2;
                e = data.IndexOf("\n", s);
            } while (e > 0);

            string name = "";
            int id = -1;
            Slot slot = Slot.None;
            List<string> sources = new List<string>();
            int attack = -1;
            string range = "";
            bool unique = false;
            int points = -1;

            for (int i = 0; i < pilotvalues.Count; i++)
            {
                string str = pilotvalues[i];
                if (str.Contains(" name"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    if (s == 0 && e == -1)
                    {
                        s = str.IndexOf("\'", 0) + 1;
                        e = str.IndexOf("\'", s);
                    }
                    name = str.Substring(s, e - s);
                }
                else if (str.Contains(" id"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        id = Int32.Parse(str.Substring(s, 3));
                    }
                    catch (ArgumentOutOfRangeException exce)
                    {
                        try
                        {
                            id = Int32.Parse(str.Substring(s, 2));
                        }
                        catch (ArgumentOutOfRangeException excep)
                        {
                            id = Int32.Parse(str.Substring(s, 1));
                        }
                    }
                }
                else if (str.Contains("attack"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    attack = Int32.Parse(str.Substring(s, 1));
                }
                else if (str.Contains("range"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    range = str.Substring(s, e - s);
                }
                else if (str.Contains("sources"))
                {
                    //s = str.IndexOf("[", 0);
                    //e = str.IndexOf("]", s);
                    //sources = GetSources(str.Substring(s, e - s));
                }
                else if (str.Contains("unique"))
                {
                    unique = true;
                }
                else if (str.Contains("points"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        points = Int32.Parse(str.Substring(s, 2));
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        points = Int32.Parse(str.Substring(s, 1));
                    }
                }
                else if (str.Contains("slot"))
                {
                    for (int j = i; j < pilotvalues.Count; j++)
                        str += pilotvalues[j];
                    slot = GetSlots(str)[0];
                }
            }

            return new Upgrade(name, id, slot, sources, attack, range, unique, points);
        }
        private static List<Modification> GetModifications(string data)
        {
            List<Modification> modifications = new List<Modification>();
            int start = data.IndexOf(" name:", 0);
            int end;

            do
            {
                end = data.IndexOf(" name:", start + 1);
                if (end <= 0)
                    end = data.Length;
                modifications.Add(ConvertToModification(data.Substring(start, end - start)));
                start = end;
            } while (start != data.Length);

            return modifications;
        }
        private static Modification ConvertToModification(string data)
        {
            int s = 0;
            int e = data.IndexOf("\n", s); ;
            List<string> pilotvalues = new List<string>();
            do
            {
                pilotvalues.Add(data.Substring(s, e - s));
                s = e + 2;
                e = data.IndexOf("\n", s);
            } while (e > 0);

            string name = "";
            int id = -1;
            List<string> sources = new List<string>();
            int points = -1;
            Ship onlyFor = null;

            for (int i = 0; i < pilotvalues.Count; i++)
            {
                string str = pilotvalues[i];
                if (str.Contains(" name"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    name = str.Substring(s, e - s);
                }
                else if (str.Contains(" id"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        id = Int32.Parse(str.Substring(s, 3));
                    }
                    catch (ArgumentOutOfRangeException exce)
                    {
                        try
                        {
                            id = Int32.Parse(str.Substring(s, 2));
                        }
                        catch (ArgumentOutOfRangeException excep)
                        {
                            id = Int32.Parse(str.Substring(s, 1));
                        }
                    }
                }
                else if (str.Contains("sources"))
                {
                    //s = str.IndexOf("[", 0);
                    //e = str.IndexOf("]", s);
                    //sources = GetSources(str.Substring(s, e - s));
                }
                else if (str.Contains("ship") && !str.Contains("ship.data.") && !str.Contains("restriction_func"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    onlyFor = GetShip(str.Substring(s, e - s));
                }
                else if (str.Contains("points"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        points = Int32.Parse(str.Substring(s, 2));
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        points = Int32.Parse(str.Substring(s, 1));
                    }
                }
            }

            return new Modification(name, id, points, sources, onlyFor);
        }
        private static List<Title> GetTitles(string data)
        {
            List<Title> titles = new List<Title>();
            int start = data.IndexOf(" name:", 0);
            int end;

            do
            {
                end = data.IndexOf(" name:", start + 1);
                if (end <= 0)
                    end = data.Length;
                titles.Add(ConvertToTitles(data.Substring(start, end - start)));
                start = end;
            } while (start != data.Length);

            return titles;
        }
        private static Title ConvertToTitles(string data)
        {
            int s = 0;
            int e = data.IndexOf("\n", s); ;
            List<string> pilotvalues = new List<string>();
            do
            {
                pilotvalues.Add(data.Substring(s, e - s));
                s = e + 2;
                e = data.IndexOf("\n", s);
            } while (e > 0);

            string name = "";
            int id = -1;
            List<string> sources = new List<string>();
            int points = -1;
            Ship onlyFor = null;
            bool unique = false;

            for (int i = 0; i < pilotvalues.Count; i++)
            {
                string str = pilotvalues[i];
                if (str.Contains(" name"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    try
                    {
                        name = str.Substring(s, e - s);
                    }
                    catch (ArgumentOutOfRangeException exc)
                    {
                        s = str.IndexOf("\'", 0) + 1;
                        e = str.IndexOf("\'", s);
                        name = str.Substring(s, e - s);
                    }
                }
                else if (str.Contains(" id"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        id = Int32.Parse(str.Substring(s, 3));
                    }
                    catch (ArgumentOutOfRangeException exce)
                    {
                        try
                        {
                            id = Int32.Parse(str.Substring(s, 2));
                        }
                        catch (ArgumentOutOfRangeException excep)
                        {
                            id = Int32.Parse(str.Substring(s, 1));
                        }
                    }
                }
                else if (str.Contains("sources"))
                {
                    //s = str.IndexOf("[", 0);
                    //e = str.IndexOf("]", s);
                    //sources = GetSources(str.Substring(s, e - s));
                }
                else if (str.Contains("ship") && !str.Contains("ship.") && !str.Contains("restriction_func") && !str.Contains("validation_func"))
                {
                    s = str.IndexOf("\"", 0) + 1;
                    e = str.IndexOf("\"", s);
                    onlyFor = GetShip(str.Substring(s, e - s));
                }
                else if (str.Contains("unique"))
                {
                    unique = true;
                }
                else if (str.Contains(" points"))
                {
                    s = str.IndexOf(":", 0) + 2;
                    try
                    {
                        points = Int32.Parse(str.Substring(s, 2));
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        points = Int32.Parse(str.Substring(s, 1));
                    }
                }
            }

            return new Title(name, id, sources, unique, onlyFor, points);
        }
        private static void SetTempPath()
        {
            tempPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TXM", "Temp");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            cards = Path.Combine(tempPath, "cards.txt");
            translations = Path.Combine(tempPath, "gertrans.txt");
        }

        public static void Serialize(Statistic stats, bool save = false)
        {
            string path = "";
            if (save)
            {
                path = stats.Path;
                if (path == "" || path == null)
                {
					Gtk.FileChooserDialog slg = new Gtk.FileChooserDialog("Statistik speichern",null,Gtk.FileChooserAction.Save,"Abbrechen",Gtk.ResponseType.Cancel,
						"Speichern",Gtk.ResponseType.Ok);
					Gtk.FileFilter filter = new Gtk.FileFilter ();
					filter.Name = "TXM Statistik Datei";
					filter.AddPattern ("*.txmstats");
					slg.AddFilter(filter);
					int response = slg.Run ();

                    if (response == -5)
                    {
						path = slg.Filename;
                    }
                    else
                        return;
                }
            }
            else
                path = Path.Combine(tempPath, Settings.BINFILE);

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, stats);
            }
        }
        public static Statistic Deserialize(bool load = false)
        {
            string path = "";
            if (load)
            {
				Gtk.FileChooserDialog dlg = new Gtk.FileChooserDialog("Statistik laden",null,Gtk.FileChooserAction.Open,"Abbrechen",Gtk.ResponseType.Cancel,
					"Öffnen",Gtk.ResponseType.Ok);
				Gtk.FileFilter filter = new Gtk.FileFilter ();
				filter.Name = "TXM Statistik Datei";
				filter.AddPattern ("*.txmstats");
				int response = dlg.Run ();

				if (response == -5) {
					path = dlg.Filename;
					dlg.Destroy ();
				} else {
					dlg.Destroy ();
					return null;
				}
            }
            else
            {
                SetTempPath();
                path = Path.Combine(tempPath, Settings.BINFILE);
            }
            Statistic stats = null;
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                stats = (Statistic)formatter.Deserialize(stream);
            }
            return stats;
        }
        public static void Export(Statistic stats, bool csv, bool info = true, string path = "")
        {
            List<string> lines = new List<string>();
            stats.Sort();
            int length = stats.IPilots.Count > stats.IShips.Count ? stats.IPilots.Count : stats.IShips.Count;
            length = length > stats.IUpgrades.Count ? length : stats.IUpgrades.Count;
            if (csv)
            {
                if (path == "")
                {
					Gtk.FileChooserDialog slg = new Gtk.FileChooserDialog("Listen speichern",null,Gtk.FileChooserAction.Save,"Abbrechen",Gtk.ResponseType.Cancel,
						"Speichern",Gtk.ResponseType.Ok);
					Gtk.FileFilter filter = new Gtk.FileFilter ();
					filter.Name = "Comma Separated Values";
					filter.AddPattern ("*.csv");
					slg.AddFilter(filter);
					int response = slg.Run ();

					if (response == -5)
						path = slg.Filename;
                }
                if (path != "")
                {
                    string line = "=SUMME(A2:A" + length + ");Schiffe;MERCode;;=SUMME(E2:E" + length + ");Piloten;Welle;;=SUMME(I2:I" + length + ");Aufwertungskarten;;=SUMME(L2:L" + length + ");Punktzahlen;;;=SUMME(P2:P" + length + ");Fraktionen;Prozent;Schiffe pro Fraktion;Schiffepro Liste;;Schiffe pro Squad;=E1/P1;;";
                    lines.Add(line);
                    List<string> PointFormulas = new List<string>(); ;
                    List<string> FactionFormulas = new List<string>();
                    List<string> ShipsPerFactionFormulas = new List<string>();
                    List<string> WaveFormulas = new List<string>();
                    for (int i = 0; i <= stats.Points.Count; i++)
                    {
                        line = "=L" + (i + 2) + "/L1";
                        PointFormulas.Add(line);
                    }
                    for (int i = 0; i <= stats.FPoints.Count; i++)
                    {
                        line = "=P" + (i + 2) + "/P1";
                        FactionFormulas.Add(line);
                    }
                    for (int i = 0; i <= stats.FPoints.Count; i++)
                    {
                        line = "=S" + (i + 2) + "/P" + (i + 2);
                        ShipsPerFactionFormulas.Add(line);
                    }
                    for (int i = 0; i <= stats.Waves.Count; i++)
                    {
                        line = "=W" + (i + 2) + "/E1";
                        WaveFormulas.Add(line);
                    }
                    for (int i = 0; i < length; i++)
                    {
                        line = "";
                        if (i < stats.IShips.Count)
                            line += stats.IShips[i].Count + ";" + stats.IShips[i].Gername + ";" + stats.IShips[i].MERCode +  ";;";
                        else
                            line += ";;;;";
                        if (i < stats.IPilots.Count)
                            line += stats.IPilots[i].Count + ";" + stats.IPilots[i].Gername + ";" + stats.IPilots[i].Wave + ";;";
                        else
                            line += ";;;;";
                        if (i < stats.IUpgrades.Count)
                            line += stats.IUpgrades[i].Count + ";" + stats.IUpgrades[i].Gername + ";;";
                        else
                            line += ";;;";
                        if (i < stats.Points.Count)
                            line += stats.Points[i][1] + ";" + stats.Points[i][0] + ";" + PointFormulas[i] + ";;";
                        else
                            line += ";;;;";
                        if (i < stats.FPoints.Count)
                            line += stats.FPoints[i].Count + ";" + Player.FactionToString(stats.FPoints[i].PointsFaction) + ";" + FactionFormulas[i] + ";" + stats.ShipsPerFaction[i] + ";" + ShipsPerFactionFormulas[i] +  ";;";
                        else
                            line += ";;;;;;";
                        if (i < stats.Waves.Count)
                            line += "Welle " + stats.Waves[i].Name + ";" + stats.Waves[i].Count + ";" + WaveFormulas[i] + ";;";
                        else
                            line += ";;;;";
                        lines.Add(line);
                    }

                    using (System.IO.StreamWriter f = new System.IO.StreamWriter(path))
                    {
                        foreach (string s in lines)
                            f.WriteLine(s);
                    }

                    if (info)
                    {
                        MessageBox.Show("Datei erfolgreich exportiert.");
                    }
                }
            }
        }
        public static void LoadCSV(Tournament2 tournament)
        {
            LoadCSV(tournament.Statistics, true, tournament);
        }
        public static void LoadCSV(Statistic stats, bool overview = false, Tournament2 tournament = null)
        {
            string path, file, overviewList, line;
            string[] url, lines;
            int countOfLists;
            bool win = true;
            List<Statistic> statistics;
            List<string> lists, players, output;
            lists = new List<string>();
            players = new List<string>();
            statistics = new List<Statistic>();
			Gtk.FileChooserDialog dlg = new Gtk.FileChooserDialog("Statistik laden",null,Gtk.FileChooserAction.Open,"Abbrechen",Gtk.ResponseType.Cancel,
				"Öffnen",Gtk.ResponseType.Ok);
			Gtk.FileFilter filter = new Gtk.FileFilter ();
			filter.Name = "TXT-Datei";
			filter.AddPattern ("*.txt");
			int response = dlg.Run ();

			if (response == -5)
			{
				path = dlg.Filename;
            }
            else
                return;
            using (StreamReader sr = new StreamReader(path))
            {
                file = sr.ReadToEnd();
            }
            if (file.Contains('\n'))
            {
                lines = file.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "")
                    {
                        lines[i] = lines[i].Remove(lines[i].LastIndexOf('\r'));
                    }
                }
            }
            else
                lines = file.Split('\r');

            countOfLists = lines[1].Split('\t').Length - 1;

            statistics.Add(stats);
            for (int i = 1; i < countOfLists; i++)
                statistics.Add(Update.LoadContents());

            for (int j = 1; j < lines.Length; j++)
            {
                if (lines[j] != "")
                {
                    url = lines[j].Split('\t');
                    players.Add(url[0]);
                    for (int i = 1; i <= countOfLists; i++)
                    {
                        if (url[i] != "")
                        {
                            if (url[i].StartsWith("\"") && url[i].EndsWith("\""))
                                url[i] = url[i].Substring(url[i].IndexOf("\"") + 1, url[i].LastIndexOf("\"") - 1);
                            else if (url[i].StartsWith("\""))
                                url[i] = url[i].Substring(url[i].IndexOf("\"") + 1);
                            else if (url[i].EndsWith("\""))
                                url[i] = url[i].Substring(0, url[i].LastIndexOf("\"") - 2);
                        }
                        if (url[i] == "")
                            overviewList = "";
                        else
                            overviewList = statistics[i - 1].Parse(url[i], true, overview);
                        lists.Add(overviewList);
                        //Aktuell nur für die erste Liste, da die Turnierverwaltung noch nicht
                        //mit Escalation klar kommt.
                        if (tournament != null && overviewList != "" && i == 1)
                        {
                            int trenner = overviewList.IndexOf(';');
                            int trenner2 = overviewList.IndexOf(';', trenner+1);
                            tournament.AddInfos(url[0], Int32.Parse(overviewList.Substring(trenner+1, trenner2 - trenner-1)), url[i]);
                        }
                    }
                }
            }

            if (countOfLists > 1)
            {
				MessageBox.Show("Es wurden mehrere Listen pro Spieler ermittelt.\nWähle einen Speicherort aus.\nAnschließend wird die erste Liste angezeigt.");

				Gtk.FileChooserDialog slg = new Gtk.FileChooserDialog("Listen speichern",null,Gtk.FileChooserAction.Save,"Abbrechen",Gtk.ResponseType.Cancel,
					"Speichern",Gtk.ResponseType.Ok);
				filter = new Gtk.FileFilter ();
				filter.Name = "TXM Statistik Datei";
				filter.AddPattern ("*.txmstats");
				slg.AddFilter(filter);
				response = slg.Run ();

				if (response == -5)
				{
					path = slg.Filename;
                }
                else
                    return;
                for (int i = 0; i < countOfLists; i++)
                {
                    statistics[i].Path = path.Substring(0, path.LastIndexOf('.')) + " " + (i + 1) + path.Substring(path.LastIndexOf('.'), path.Length - path.LastIndexOf('.'));
                    Serialize(statistics[i], true);
                }
            }

            if (overview)
            {
                MessageBox.Show("Gib bitte den Speicherort für die\nÜbersichtsliste(n) an.");
				Gtk.FileChooserDialog slg = new Gtk.FileChooserDialog("Listen speichern",null,Gtk.FileChooserAction.Save,"Abbrechen",Gtk.ResponseType.Cancel,
					"Speichern",Gtk.ResponseType.Ok);
				filter = new Gtk.FileFilter ();
				filter.Name = "CSV-Datei";
				filter.AddPattern ("*.csv");
				slg.AddFilter(filter);
				response = slg.Run ();

				if (response == -5)
                {
					path = slg.Filename;
                }
                else
                    return;
                output = new List<string>();
                line = "Spieler;Fraktion;Punkte;Liste";
                output.Add(line);
                for (int i = 0; i < players.Count; i++)
                {
                    url = lines[i + 1].Split('\t');
                    line = players[i] + ";";
                    for (int j = 0; j < countOfLists; j++)
                    {
                        line += lists[i * countOfLists + j] + ";";
                    }
                    output.Add(line);
                }
                using (System.IO.StreamWriter f = new System.IO.StreamWriter(path))
                {
                    foreach (string s in output)
                        f.WriteLine(s);
                }
                if (countOfLists == 1)
                {
                    path = path.Substring(0, path.LastIndexOf('.')) + "_statistik.csv";
                    Export(statistics[0], true, false, path);
                }
                else
                {
                    for (int i = 0; i < countOfLists; i++)
                    {
                        string p = statistics[i].Path.Substring(0, path.LastIndexOf('.') + 2) + ".csv";
                        Export(statistics[i], true, false, p);
                    }
                }
            }
        }
    }
}
