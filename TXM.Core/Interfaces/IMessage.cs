using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXM.Core
{
    public interface IMessage
    {
        void Show(string text);
        bool ShowWithOKCancel(string text);
    }
}
