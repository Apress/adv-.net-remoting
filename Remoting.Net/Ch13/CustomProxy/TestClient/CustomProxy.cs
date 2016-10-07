using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;

namespace Client
{
	public class CustomProxy: RealProxy
	{
		String _url;
		String _uri;
		IMessageSink _sinkChain;

		public CustomProxy(Type type, String url) : base(type)
		{
			_url = url;


			// check each registered channel if it accepts the
			// given URL
			IChannel[] registeredChannels = ChannelServices.RegisteredChannels;
			foreach (IChannel channel in registeredChannels )
			{
				if (channel is IChannelSender)
				{
					IChannelSender channelSender = (IChannelSender)channel;

					// try to create the sink
					_sinkChain = channelSender.CreateMessageSink(_url, 
						null, out _uri);
					
					// if the channel returned a sink chain, exit the loop
					if (_sinkChain != null)
						break;
				}
			}

			// no registered channel accepted the URL
			if (_sinkChain == null)
			{
				throw new Exception("No channel has been found for " + _url);
			}
		}

		public override IMessage Invoke(IMessage msg)
		{
			DumpMessageContents(msg);
			msg.Properties["__Uri"] = _url;

			IMessage retMsg = _sinkChain.SyncProcessMessage(msg);

			DumpMessageContents(retMsg);

			return retMsg;
		}

		private String GetPaddedString(String str) 
		{
			String ret = str + "                  ";
			return ret.Substring(0,17);
		}

		private void DumpMessageContents(IMessage msg) 
		{
			Console.WriteLine("========================================");
			Console.WriteLine("============ Message Dump ==============");
			Console.WriteLine("========================================");

			Console.WriteLine("Type: {0}", msg.GetType().ToString());

			Console.WriteLine("--- Properties ---");
			IDictionary dict = msg.Properties;
			IDictionaryEnumerator enm = (IDictionaryEnumerator) dict.GetEnumerator();

			while (enm.MoveNext())
			{
				Object key = enm.Key;
				String keyName = key.ToString();
				Object val = enm.Value;

				Console.WriteLine("{0}: {1}", GetPaddedString(keyName), val);

				// check if it's an object array
				Object[] objval = val as Object[];
				if (objval != null)
				{
					DumpObjectArray(objval);
				}

			}

			Console.WriteLine();
			Console.WriteLine();
		}

		private void DumpObjectArray(object[] data) 
		{
			// if empty -> return
			if (data.Length == 0) return;
			
			Console.WriteLine("\t --- Array Contents ---");
			for (int i = 0; i < data.Length; i++) 
			{
				Console.WriteLine("\t{0}: {1}", i, data[i]);
			}
		}
	}
}
