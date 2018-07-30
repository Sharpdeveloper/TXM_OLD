using System;

namespace TXM.Lin
{
	public class StatisticsTreeNode: Gtk.TreeNode
	{
		[Gtk.TreeNodeValue (Column = 0)]
		public string Name { get; private set;}
		[Gtk.TreeNodeValue (Column = 1)]
		public int Count { get; private set;}

		public StatisticsTreeNode(int count, string name)
		{
			Count = count;
			Name = name;
		}
	}
}

