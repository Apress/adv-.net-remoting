using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
namespace Server 
{
	[Serializable, SoapType(XmlNamespace="http://schemas.microsoft.com/clr/nsassem/Server/Server%2C%20Version%3D1.0.678.38058%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Dnull", XmlTypeNamespace="http://schemas.microsoft.com/clr/nsassem/Server/Server%2C%20Version%3D1.0.678.38058%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Dnull")]
	public class CustomerManager : System.MarshalByRefObject
	{
		[SoapMethod(SoapAction="http://schemas.microsoft.com/clr/nsassem/Server.CustomerManager/Server#GetCustomer")]
		public General.Customer GetCustomer(Int32 id)
		{
			return((General.Customer) (Object) null);
		}

	}
}
