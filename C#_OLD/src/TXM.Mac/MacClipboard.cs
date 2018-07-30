using AppKit;
using Foundation;

using TXM.Core;

namespace TXM.Mac
{
    public class MacClipboard : IClipboard
    {
		public void SetText(string text)
		{
			// Get the standard pasteboard
			var pasteboard = NSPasteboard.GeneralPasteboard;

			// Empty the current contents
			pasteboard.ClearContents();

			// Add the current image to the pasteboard
            pasteboard.WriteObjects(new NSString[] { (NSString) NSObject.FromObject(text) });
		}
    }
}
