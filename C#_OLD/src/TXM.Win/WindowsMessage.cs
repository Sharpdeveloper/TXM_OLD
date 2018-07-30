using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using TXM.Core;

namespace TXM
{
    [Serializable]
    public class WindowsMessage : IMessage
    {

        public void Show(string text)
        {
            MessageBox.Show(text);
        }

        public bool ShowWithOKCancel(string text)
        {
            MessageBoxResult mbr = MessageBox.Show(text, "Warning", MessageBoxButton.OKCancel);
            return mbr == MessageBoxResult.OK;
        }
    }
}
