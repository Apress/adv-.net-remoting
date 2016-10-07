#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace RemotedType
{
    public interface IRemotedType
	{
        void DoCall(string message, int counter);
    } 
}
