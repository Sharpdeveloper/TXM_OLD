using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public enum XWingAction
    {
        Focus,
        TargetLock,
        Boost,
        Evade,
        BarrelRoll,
        Recover,
        Reinforce,
        Coordinate,
        Jam,
        Cloak
    }
    [Serializable]
    public class Ship
    {
        public string Name { get; private set; }
        public string Gername { get;  set; }
        public List<Faction> Factions { get; private set; }
        public int Attack { get; private set; }
        public int Agility { get; private set; }
        public int Hull { get; private set; }
        public int Shields { get; private set; }
        public List<XWingAction> Actions { get; private set; }
        public List<int[]> Maneuvers { get; private set; }
        private int count;
        public string MERCode { get; private set; }

        public Ship(string name, List<Faction> factions, int attack, int agility, int hull, int shields, List<XWingAction> actions, List<int[]> maneuvers)
        {
            Name = name;
            Gername = name;
            Factions = factions;
            Attack = attack;
            Agility = agility;
            Hull = hull;
            Shields = shields;
            Actions = actions;
            Maneuvers = maneuvers;
            Count = 0;
            switch(name)
            {
                case "X-Wing":
                    MERCode = ":XWing2:";
                    break;
                case "Y-Wing":
                    MERCode = ":Y-Wing:";
                    break;
                case "TIE Fighter":
                    MERCode = ":Tiefighter:";
                    break;
                case "TIE Advanced": 
                    MERCode = ":TieAdvanced:";
                    break;
                case "A-Wing":
                    MERCode = ":A-Wing:";
                    break;
                case "YT-1300":
                    MERCode = ":Falke:";
                    break;
                case "TIE Interceptor":
                    MERCode = ":TieInterceptor";
                    break;
                case "Firespray-31":
                    MERCode = ":SlaveI:";
                    break;
                case "HWK-290":
                    MERCode = ":HWK:";
                    break;
                case "Lambda-Class Shuttle":
                    MERCode = ":Imperial Shutt";
                    break;
                case "B-Wing":
                    MERCode = ":B-Wing:";
                    break;
                case "TIE Bomber":
                    MERCode = ":T-Bomber:";
                    break;
                case "GR-75 Medium Transporter":
                    MERCode = ":RebelTranspor";
                    break;
                case "CR90 Corvette (Fore)":
                case "CR90 Corvette (Aft)":
                    MERCode = ":TantiveIV:";
                    break;
                case "Z-95 Headhunter":
                    MERCode = ":Z95:";
                    break;
                case "TIE Defender":
                    MERCode = ":Tie Defender:";
                    break;
                case "E-Wing":
                    MERCode = ":E-Wing:";
                    break;
                case "TIE Phantom":
                    MERCode = ":Tie-Phantom:";
                    break;
                case "YT-2400":
                    MERCode = ":Outrider:";
                    break;
                case "VT-49 Decimator":
                    MERCode = ":VT49Decimator:";
                    break;
                case "StarViper":
                    MERCode = ":StarViper:";
                    break;
                case "M3-A Interceptor":
                    MERCode = ":M3-A:";
                    break;
                case "Aggressor":
                    MERCode = ":IG-2000:";
                    break;
                default:
                    MERCode = "";
                    break;
            }

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
    }
}
