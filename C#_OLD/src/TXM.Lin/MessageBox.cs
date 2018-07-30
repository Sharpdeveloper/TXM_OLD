using System;
using Gtk;

namespace TXM.Lin
{
	public class MessageBox
	{
		public static void Show (string Msg)
		{
			MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, Msg);
			md.Run ();
			md.Destroy ();
		}

		public static bool ShowBox(string Msg)
		{
			bool r = false;
			MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Question, ButtonsType.OkCancel, Msg);
			int i = md.Run ();
			md.Destroy ();
			return i == -5;
		}

	}

}

