using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyCulture("")] // default
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("mykey.key")]

namespace VersionedSAO
{
	public class SomeSAO: MarshalByRefObject
	{
		public String getSAOVersion() 
		{
			return "Called Version 1.0.0.1 SAO";
		}
	}
}
