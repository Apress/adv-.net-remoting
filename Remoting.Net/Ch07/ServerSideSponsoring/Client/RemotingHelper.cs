using System;
using System.Collections;
using System.Runtime.Remoting;

namespace Client
{
  class RemotingHelper 
  {
    private static IDictionary _wellKnownTypes;

    public static Object CreateProxy(Type type) 
    {
      if (_wellKnownTypes==null) InitTypeCache();
      WellKnownClientTypeEntry entr = (WellKnownClientTypeEntry) _wellKnownTypes[type];

      if (entr == null) 
      {
        throw new RemotingException("Type not found!");
      }

      return Activator.GetObject(entr.ObjectType,entr.ObjectUrl);
    }

    public static void InitTypeCache() 
    {
      Hashtable types= new Hashtable();
      foreach (WellKnownClientTypeEntry entr in 
        RemotingConfiguration.GetRegisteredWellKnownClientTypes()) 
      {
	
        if (entr.ObjectType == null) 
        {
          throw new RemotingException("A configured type could not " +
            "be found. Please check spelling in your configuration file.");
        }
        types.Add (entr.ObjectType,entr);
      }
		
      _wellKnownTypes = types;
    }
  }
}
