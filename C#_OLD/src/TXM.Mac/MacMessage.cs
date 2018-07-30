using AppKit;

using TXM.Core;

namespace TXM.Mac
{
    public class MacMessage : IMessage
    {
		public void Show(string text)
		{
			var alert = new NSAlert()
			{
				AlertStyle = NSAlertStyle.Informational,
                InformativeText = text,
                MessageText = "Information"
			};
			alert.RunModal();
		}

		public bool ShowWithOKCancel(string text)
        {
			var alert = new NSAlert()
			{
				AlertStyle = NSAlertStyle.Informational,
				InformativeText = text,
				MessageText = "Warning",
			};
			alert.AddButton("OK");
			alert.AddButton("Cancel");
			var result = alert.RunModal();

			return result == 1000;
		}
    }
}
