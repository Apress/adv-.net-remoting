using System;
using System.Collections;
using System.Runtime.Remoting;

namespace RemotingTools
{
	public class RemotingHelper {
		private static bool _isInit;
		private static IDictionary _wellKnownTypes;

		public static Object GetObject(Type type) {
			if (! _isInit) InitTypeCache();
			WellKnownClientTypeEntry entr = (WellKnownClientTypeEntry) _wellKnownTypes[type];

			if (entr == null) {
				throw new RemotingException("Type not found!");
			}

			return Activator.GetObject(entr.ObjectType,entr.ObjectUrl);
		}

		public static void InitTypeCache() {
			_wellKnownTypes= new Hashtable();
			foreach (WellKnownClientTypeEntry entr in 
				RemotingConfiguration.GetRegisteredWellKnownClientTypes()) {
				_wellKnownTypes.Add (entr.ObjectType,entr);
			}
			
		}
	}
	
}
