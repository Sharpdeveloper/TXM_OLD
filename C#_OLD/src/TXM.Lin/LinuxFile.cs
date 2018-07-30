using System;
using System.Collections.Generic;
using Gtk;

using TXM.Core;

namespace TXM.Lin
{
	[Serializable]
	public class LinuxFile : IFile
	{
		public string FileName { get; set; }
		private List<string> filter;
		private List<string> filterName;

		public void AddFilter(string _filter, string _filtername, bool add = false)
		{
			if(!add)
			{
				filter = new List<string>();
				filterName = new List<string>();
			}
			filter.Add(_filter);
			filterName.Add(_filtername);
		}

		public bool Open()
		{
			FileChooserDialog dlg = new FileChooserDialog("Öffnen",null,FileChooserAction.Open,"Abbrechen",ResponseType.Cancel,
				"Öffnen",ResponseType.Ok);
			FileFilter fileFilter = new FileFilter ();
			for (int i = 0; i < filter.Count; i++) {
				fileFilter.Name = filterName [i];
				fileFilter.AddPattern(filter [i]);
				dlg.AddFilter (fileFilter);
			}
			bool result = dlg.Run() == -5;
			if(result)
				FileName = dlg.Filename;
			dlg.Destroy ();
			return result;
		}

		public bool Save()
		{
			FileChooserDialog dlg = new FileChooserDialog("Speichern",null,FileChooserAction.Save,"Abbrechen",ResponseType.Cancel,
				"Speichern",ResponseType.Ok);
			FileFilter fileFilter = new FileFilter ();
			for (int i = 0; i < filter.Count; i++) {
				fileFilter.Name = filterName [i];
				fileFilter.AddPattern(filter [i]);
				dlg.AddFilter (fileFilter);
			}
			bool result = dlg.Run() == -5;
			if(result)
				FileName = dlg.Filename;
			dlg.Destroy ();
			return result;
		}
	}
}

