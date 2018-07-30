using TXM.Core;

namespace TXM
{
    public class WindowsClipboard : IClipboard
    {
        public void SetText(string text)
        {
            System.Windows.Clipboard.SetText(text);
        }
    }
}
