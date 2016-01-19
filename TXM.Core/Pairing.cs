using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXM.Core
{
	[Serializable]
	public class Pairing
	{
		#region Static Fields

		private static int tableNr = 0;

		#endregion

		#region Properties

		public int TableNr { get; private set; }

		public Player Player1 { get; set; }

		public Player Player2 { get; set; }

		public int Player1Score { get; set; }

		public int Player2Score { get; set; }

		public bool ResultEdited { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="TXM.Core.Pairing"/> class.
		/// </summary>
		public Pairing ()
		{
			TableNr = ++tableNr;
			ResultEdited = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TXM.Core.Pairing"/> class.
		/// </summary>
		/// <param name="tableNr">Table nr.</param>
		public Pairing (int tableNr)
		{
			TableNr = tableNr;
			ResultEdited = false;
		}

		/// <summary>
		/// Copies an exisiting Pairings
		/// </summary>
		/// <param name="p">Pairing.</param>
		public Pairing (Pairing pairing)
		{
			this.TableNr = pairing.TableNr;
			this.Player1 = pairing.Player1;
			this.Player2 = pairing.Player2;
			this.Player1Score = pairing.Player1Score;
			this.Player2Score = pairing.Player2Score;
			this.ResultEdited = pairing.ResultEdited;
		}

		#endregion

		#region Indirect Properties

		public string Player1Name {
			get {
				return Player1.DisplayName;
			}
		}

		public string Player2Name {
			get {
				return Player2.DisplayName;
			}
		}

		#endregion

		#region Static Functions

		public static void ResetTableNr ()
		{
			tableNr = 0;
		}

		#endregion


		public void GetTableNr ()
		{
			TableNr = ++tableNr;
		}
	}
}
