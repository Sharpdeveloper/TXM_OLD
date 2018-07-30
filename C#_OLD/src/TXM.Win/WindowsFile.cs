using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

using TXM.Core;

namespace TXM
{
    [Serializable]
    public class WindowsFile : IFile
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
            if(!add)
            {
                filter = new List<string>();
                filterName = new List<string>();
            }
            filter.Add("*." + _filter);
            filterName.Add(_filtername);
        }

        public bool Open()
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = ""
            };
            dlg.Filter += filterName[0] + " |" + filter[0];
            for (int i = 1; i < filter.Count; i++) 
                dlg.Filter += "|" + filterName[i] + " |" + filter[i];
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                FileName = dlg.FileName;
            return result == true;
        }

        public bool Save()
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = ""
            };
            for (int i = 0; i < filter.Count; i++)
                dlg.Filter += filterName[i] + " |" + filter[i];
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                FileName = dlg.FileName;
            return result == true;
        }
    }
}
