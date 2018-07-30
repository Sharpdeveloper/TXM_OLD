using System;
using System.Collections.Generic;
using System.Linq;

namespace TXM.Core
{
    [Serializable]
	public class SWLCGRules : AbstractRules
	{
        protected new static string name = "Star Wars\u2122: The Card Game";

        public SWLCGRules()
        {
            IsDrawPossible = true;
            OptionalFields = new List<string>() { "eSoS" };
            IsDoubleElimination = true;
            IsRandomSeeding = true;
            IsWinnerDropDownNeeded = false;
            DefaultMaxPoints = 0;
            Factions = new string[] { "Light Side", "Dark Side" };
            DefaultTime = 70;
            base.name = name;
        }

        public static string GetRuleName()
        {
            return name;
        }

        protected override bool CalculateResult(Result result, Func<int, int, int> f)
		{
			Result newResult = result;

			//ID == -1 => Bye
			if (result.Enemy.ID == -1 || result.Enemy.ID == -2)
			{
				newResult = new Result(1, 0, result.Enemy, 1, result.WinnerID);
			}

            int tP = newResult.Destroyed;
            TtournamentPoints = f.Invoke(0, tP);
            switch (tP)
            {
                case 3:
                    Twins = f.Invoke(0, 1);
                    break;
                case 2:
                    TmodifiedWins = f.Invoke(0, 1);
                    break;
                case 1:
                    Tdraws = f.Invoke(0, 1);
                    break;
                case 0:
                    Tlosses = f.Invoke(0, 1);
                    break;
            }

            return tP == 1;
		}

        public override List<Player> SortTable(List<Player> unsorted)
        {
            List<Player> t = unsorted.OrderByDescending(x => x.TournamentPoints).ThenByDescending(x => x.StrengthOfSchedule).ThenByDescending(x => x.ExtendedStrengthOfSchedule).ThenBy(x => x.Order).ToList<Player>();
            for (int i = 0; i < t.Count; i++)
                t[i].Rank = i + 1;
            return t;
        }
    }
}
