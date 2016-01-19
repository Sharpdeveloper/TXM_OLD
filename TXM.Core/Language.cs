using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXM.Core
{
	[Serializable]
	public class Language
	{
		private Dictionary<string, string> translation;

		/// <summary>
		/// Initializes a new instance of the <see cref="TXM.Core.Language"/> class.
		/// </summary>
		public Language ()
		{
			translation = new Dictionary<string, string> ();
		}

		/// <summary>
		/// Gets the translation of a word or phrase
		/// </summary>
		/// <returns>The translation.</returns>
		/// <param name="text">Text.</param>
		public string GetTranslation (string text)
		{
			if (translation.ContainsKey (text))
				return translation [text];
			else
				return text;
		}

		/// <summary>
		/// transforms the translation in an easy access directory
		/// </summary>
		/// <param name="fileData">File data.</param>
		public void SetTranslations (List<string> fileData)
		{
			string[] s;
			translation = new Dictionary<string, string> ();
			foreach (var a in fileData) {
				s = a.Split (':');
				translation.Add (s [0], s [1]);
			}
		}
	}
}
