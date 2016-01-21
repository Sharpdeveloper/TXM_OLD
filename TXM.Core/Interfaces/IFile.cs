using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXM.Core
{
    public interface IFile
    {
        string FileName { get; set; }

        void AddFilter(string _filter, string _filtername, bool add = false);
		bool Open(string openText, string CancelText);
		bool Save(string saveText, string CancelText);
    }
}
