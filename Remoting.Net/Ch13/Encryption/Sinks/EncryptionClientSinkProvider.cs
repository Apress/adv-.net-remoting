using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Collections;

namespace EncryptionSink
{
	public class EncryptionClientSinkProvider: IClientChannelSinkProvider
	{
		private IClientChannelSinkProvider _nextProvider;

		private byte[] _encryptionKey;
		private String _encryptionAlgorithm;

		public EncryptionClientSinkProvider(IDictionary properties, ICollection providerData) 
		{
			_encryptionAlgorithm = (String) properties["algorithm"];
			String keyfile = (String) properties["keyfile"];

			if (_encryptionAlgorithm == null || keyfile == null) 
			{
				throw new RemotingException("'algorithm' and 'keyfile' have to " +
					"be specified for EncryptionClientSinkProvider");
			}


			// read the encryption key from the specified fike
			FileInfo fi = new FileInfo(keyfile);

			if (!fi.Exists) 
			{
				throw new RemotingException("Specified keyfile does not exist");
			}

			FileStream fs = new FileStream(keyfile,FileMode.Open);
			_encryptionKey = new Byte[fi.Length];
			fs.Read(_encryptionKey,0,_encryptionKey.Length);
		}

		public IClientChannelSinkProvider Next
		{
			get {return _nextProvider; }
			set {_nextProvider = value;}
		}

		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData) 
		{
			// create other sinks in the chain
			IClientChannelSink next = _nextProvider.CreateSink(channel,
				url,
				remoteChannelData);	
	
			// put our sink on top of the chain and return it				
			return new EncryptionClientSink(next,_encryptionKey,_encryptionAlgorithm);
		}
	}
}
