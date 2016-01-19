using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
	[Serializable]
	public class Result
	{
		public Player Enemy { get; set; }

		public int Destroyed { get; set; }

		public int Lost { get; set; }

		public bool FirstRound { get; set; }

		public int MaxSquadPoints { get; set; }

		public Result (int destroyed, int lost, Player enemy, bool firstRound, int maxSquadPoints)
		{
			Enemy = enemy;
			Destroyed = destroyed;
			Lost = lost;
			FirstRound = firstRound;
			MaxSquadPoints = maxSquadPoints;
		}
	}
}
