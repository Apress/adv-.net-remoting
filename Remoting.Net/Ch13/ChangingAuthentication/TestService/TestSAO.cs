using System;
using System.Security.Principal;

namespace Service
{
	public class TestSAO: MarshalByRefObject
	{

		public String GetPrincipalName() 
		{

			IPrincipal principal = 
				System.Threading.Thread.CurrentPrincipal;

			return principal.Identity.Name;

		}

	}

}





