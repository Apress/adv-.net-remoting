using System;
using System.Collections;

namespace UrlAuthenticationSink
{

	internal class UrlAuthenticationEntry 
	{
		internal String Username;
		internal String Password;
		internal String UrlBase;

		internal UrlAuthenticationEntry (String urlbase, 
			String user, 
			String password)
		{
			this.Username = user;
			this.Password = password;
			this.UrlBase = urlbase.ToUpper();
		}
	}


	public class UrlAuthenticator
	{
		private static ArrayList _entries = new ArrayList();
		private static UrlAuthenticationEntry _defaultAuthenticationEntry;

		public static void AddAuthenticationEntry(String urlBase,
			String userName,
			String password) 
		{
			_entries.Add(new UrlAuthenticationEntry(
				urlBase,userName,password));
		}

		public static void SetDefaultAuthenticationEntry(String userName, 
			String password) 
		{
			_defaultAuthenticationEntry = new UrlAuthenticationEntry(
				null,userName,password);
		}

		internal static UrlAuthenticationEntry GetAuthenticationEntry(String url) 
		{
			foreach (UrlAuthenticationEntry entr in _entries) 
			{
				// check if a registered entry matches the url-parameter
				if (url.ToUpper().StartsWith(entr.UrlBase)) 
				{
					return entr;
				}
			}

            // if none matched, return the default entry (which can be null as well)
			return _defaultAuthenticationEntry;
		}


	}
}
