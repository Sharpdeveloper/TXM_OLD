using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public class Upgrade: IUpgrade
    {
        public int Id{ get; private set; }
        public Slot UpgradeSlot{ get; private set; }
        public List<string> Sources{ get; private set; }
        public int Points{ get; private set; }
        public int Attack{ get; private set; }
        public string Range{ get; private set; }
        public bool Unique{ get; private set; }
        private int count;
        public string Name { get; set; }
        public string Gername { get; set; }

        public Upgrade(string name, int id, Slot slot, List<string> sources, int attack, string range, bool unique, int points)
        {
            Name = name;
            Gername = name;
            Id = id;
            UpgradeSlot = slot;
            Sources = sources;
            Attack = attack;
            Range = range;
            Unique = unique;
            Points = points;
            Count = 0;
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
