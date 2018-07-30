using AppKit;
using Foundation;

namespace TXM.Mac
{
	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{
        public ViewController MainViewController { get; set; }

		public override void DidFinishLaunching(NSNotification notification)
		{
			// Insert code here to initialize your application
		}

		[Export("openDocument:")]
		void OpenDialog(NSObject sender)
		{
            MainViewController.Open();
		}

		public override void WillTerminate(NSNotification notification)
		{
			// Insert code here to tear down your application
		}
	}
}
