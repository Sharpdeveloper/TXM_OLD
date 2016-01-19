using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
    [Serializable]
    public class Title:IUpgrade
    {
        public string Name{ get; set; }
        public string Gername{ get; set; }
        public int Id{ get; private set; }
        public List<string> Sources{ get; private set; }
        public bool Unique{ get; private set; }
        public Ship OnlyFor{ get; private set; }
        public int Points{ get; private set; }
        private int count;

        public Title(string name, int id, List<string> sources, bool uniqe, Ship onlyFor, int points)
        {
            Name = name;
            Gername = name;
            Id = id;
            Sources = sources;
            Unique = uniqe;
            OnlyFor = onlyFor;
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
