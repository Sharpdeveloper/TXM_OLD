using System.Collections.Generic;
using AppKit;
using TXM.Core;
namespace TXM.Mac
{
	public class MacFile : IFile
	{
		private List<string> filter;
		private List<string> filterName;

		public string FileName
		{
			get;
			set;
		}

		public void AddFilter(string _filter, string _filtername, bool add = false)
		{
			if (!add)
			{
				filter = new List<string>();
				filterName = new List<string>();
			}
			filter.Add(_filter);
			filterName.Add(_filtername);
		}

		public bool Open()
		{
			var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.Title = "Open Tournament";

            List < string > filterTemp = new List<string>();    
            for (int i = 0; i < filter.Count; i++)
                filterTemp.Add(filter[0]);
            
            dlg.AllowedFileTypes = filter.ToArray();

			bool result = dlg.RunModal() == 1;
			if (result == true)
                FileName = dlg.Url.Path;
			return result;
		}

		public bool Save()
		{
			var dlg = new NSSavePanel();
			dlg.Title = "Save Tournament";

			List<string> filterTemp = new List<string>();
			for (int i = 0; i < filter.Count; i++)
				filterTemp.Add(filter[0]);

			dlg.AllowedFileTypes = filter.ToArray();

			bool result = dlg.RunModal() == 1;
			if (result == true)
                FileName = dlg.Url.Path;
			return result;
		}
    }
}
