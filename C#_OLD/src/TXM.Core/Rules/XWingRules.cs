using System;
using System.Collections.Generic;
using System.Linq;

namespace TXM.Core
{
    [Serializable]
	public class XWingRules : AbstractRules
	{
        private new static string name = "Star Wars\u2122: X-Wing\u2122 Miniatures Game";

        public XWingRules()
        {
            IsDrawPossible = false;
            OptionalFields = new List<string>() { "MoV" };
            IsDoubleElimination = false;
            IsRandomSeeding = true;
            IsWinnerDropDownNeeded = true;
            DefaultMaxPoints = 100;
            Factions = new string[] { "Rebel", "Imperial", "Scum & Villainy" };
            DefaultTime = 75;
            base.name = name;
        }

        public static string GetRuleName()
		{
            return name;
		}

        protected override bool CalculateResult(Result result, Func<int, int, int> f)
		{
			Result newResult = result;

            if (newResult.MaxPoints == 0)
            {
                newResult.MaxPoints = DefaultMaxPoints;
            }

            //ID == -1 => Bye
            if (result.Enemy.ID == -1)
			{
				newResult = new Result((int)(0.5 * result.MaxPoints), 0, result.Enemy, result.MaxPoints, result.WinnerID);
			}
			//ID == -2 => WonBye
			else if (result.Enemy.ID == -2)
			{
				newResult = new Result((int) result.MaxPoints, 0, result.Enemy, result.MaxPoints, result.WinnerID);
			}

			int tP = newResult.Destroyed - newResult.Lost;
			if (tP > 0)
			{
				tP = 1;
			}
			else if (tP == 0 && newResult.WinnerID != newResult.Enemy.ID)
			{
				tP = 1;
			}
			else
			{
				tP = 0;
			}

			TmarginOfVictory = f.Invoke(0, (newResult.Destroyed - newResult.Lost + newResult.MaxPoints));
			TdestroyedPoints = f.Invoke(0, newResult.Destroyed);
			TlostPoints = f.Invoke(0, newResult.Lost);
			TtournamentPoints = f.Invoke(0, tP);
			switch (tP)
			{
				case 1:
					Twins = f.Invoke(0, 1);
					break;
				case 0:
					Tlosses = f.Invoke(0, 1);
					break;
			}

			return tP == 1;
		}

        public override List<Player> SortTable(List<Player> unsorted)
        {
            List<Player> t = unsorted.OrderByDescending(x => x.TournamentPoints).ThenByDescending(x => x.MarginOfVictory).ThenByDescending(x => x.StrengthOfSchedule).ThenBy(x => x.Order).ToList<Player>();
            for (int i = 0; i < t.Count; i++)
                t[i].Rank = i + 1;
            return t;
        }
    }
}
