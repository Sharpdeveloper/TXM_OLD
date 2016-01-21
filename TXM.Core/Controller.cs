using System;

namespace TXM.Core
{
	public class Controller
	{
		IO io;

		public Controller (IFile fileManager, IMessage messageManager)
		{
			io = new IO (fileManager, messageManager);
		}
	}
}

