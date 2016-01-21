﻿using System;
using Gtk;

using TXM.Core;

namespace TXM
{
	[Serializable]
	public class MultiMessage:IMessage
	{
		public void Show(string text)
		{
			MessageBox.Show(text);
		}

		public bool ShowWithOKCancel(string text)
		{
			return MessageBox.ShowBox(text);
		}
	}

	[Serializable]
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
			MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Question, ButtonsType.OkCancel, Msg);
			int i = md.Run ();
			md.Destroy ();
			return i == -5;
		}
	}
}

