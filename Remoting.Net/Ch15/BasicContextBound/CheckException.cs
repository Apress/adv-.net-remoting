using System;

namespace ContextBound
{

	public class CheckException : Exception
	{
		public CheckException(){
		}

		public CheckException(String msg): base (msg) {
		}
	}
}
