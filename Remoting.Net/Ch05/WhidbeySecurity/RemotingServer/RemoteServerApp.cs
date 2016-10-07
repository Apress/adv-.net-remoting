#region Using directives

using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

using System.Security;
using System.Security.Principal;

#endregion

namespace RemotingServer
{
    public class RemoteServerApp 
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Configuring server...");
                System.Runtime.Remoting.RemotingConfiguration.Configure("RemotingServer.exe.config");
                System.Console.WriteLine("Server configured, waiting for requests...");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error while configuring server: " + ex.Message);
                System.Console.ReadLine();
            }
        }
    }

    public class MyRemoteObject : MarshalByRefObject, RemotedType.IRemotedType
    {
        #region IRemotedType Members

        public void DoCall(string message, int counter)
        {
            // Get some information about the caller's context
            IIdentity remoteIdentity = CallContext.GetData("__remotePrincipal") as IIdentity;
            if (remoteIdentity != null)
            {
                System.Console.WriteLine("Authenticated user:\n-){0}\n-){1}",
                                            remoteIdentity.Name,
                                            remoteIdentity.AuthenticationType.ToString());

                // Is the principal set on the managed thread?
                IIdentity threadId = System.Threading.Thread.CurrentPrincipal.Identity;
                System.Console.WriteLine("Current threads identity: {0}!", threadId.Name);

                // Get the identity of the process
                WindowsIdentity procId = WindowsIdentity.GetCurrent();
                System.Console.WriteLine("Process-Identity: {0}", procId.Name);
            }
            else
            {
                System.Console.Write("!! Attention, not authenticated !!");
            }

            // Just do the stupid work
            for (int i = 0; i < counter; i++)
            {
                System.Console.WriteLine("You told me to say {0}: {1}!", counter.ToString(), message);
            }
        }

        #endregion
    }
}
