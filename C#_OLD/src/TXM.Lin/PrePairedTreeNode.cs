using System;

namespace TXM.Lin
{
	public class PrePairedTreeNode: Gtk.TreeNode
	{
		[Gtk.TreeNodeValue (Column = 0)]
		public int Nr { get; private set;}
		[Gtk.TreeNodeValue (Column = 1)]
		public string Text { get; private set;}

		public PrePairedTreeNode(int nr, string text)
		{
			Nr = nr;
			Text = text;
		}
	}
}

