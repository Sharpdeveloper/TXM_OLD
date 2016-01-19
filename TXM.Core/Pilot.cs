using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public enum Slot
    {
        Elite,
        Torpedo,
        Astromech,
        Turret,
        Missile,
        Crew,
        Cannon,
        Bomb,
        System,
        Cargo,
        Hardpoint,
        Team,
        Illicit,
        SalvagedAstromech,
        None
    }
    [Serializable]
    public class Pilot
    {
        public string Name{ get; private set; }
        public string Gername{ get; set; }
        public Faction PilotsFaction{ get; private set; }
        public int Id{ get; private set; }
        public List<string> Sources{ get; private set; }
        public bool Unique{ get; private set; }
        public Ship PilotsShip{ get; private set; }
        public int Skill{ get; private set; }
        public int Points{ get; private set; }
        public List<Slot> Slots{ get; private set; }
        public List<IUpgrade> Upgrades { get; set; }
        public string Wave { get; private set; }
        private int count;

        public Pilot(string name, Faction faction, int id, List<string> sources, bool unique, Ship ship, int skill, int points, List<Slot> slots)
        {
            Name = name;
            Gername = name;
            PilotsFaction = faction;
            Id = id;
            Sources = sources;
            Unique = unique;
            PilotsShip = ship;
            Skill = skill;
            Points = points;
            Slots = slots;
            Count = 0;

            if (id <= 22)
                Wave = "1";
            else if (id <= 40)
                Wave = "2";
            else if (id <= 56)
                Wave = "3";
            else if (id <= 62)
                Wave = "3.5";
            else if (id == 63)
                Wave = "Epic 1";
            else if (id <= 79)
                Wave = "4";
            else if (id <= 81)
                Wave = "Epic 2";
            else if (id <= 85)
                Wave = "Epic 1";
            else if (id <= 89)
                Wave = "4.5";
            else if (id <= 91)
                Wave = "Epic 2";
            else if (id <= 99)
                Wave = "5";
            else if (id <= 127)
                Wave = "6";
            else if (id <= 130)
                Wave = "Epic 3";
            else
                Wave = "Unbekannt";
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                if (count < 0)
                    count = 0;
            }
        }

        public Pilot Copy()
        {
            return new Pilot(this.Name, this.PilotsFaction, this.Id, this.Sources, this.Unique, this.PilotsShip, this.Skill, this.Points, this.Slots);
        }
    }
}
