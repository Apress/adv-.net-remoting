using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Collections;

namespace EncryptionSink
{
	public class EncryptionServerSinkProvider: IServerChannelSinkProvider
	{
		private byte[] _encryptionKey;
		private String _encryptionAlgorithm;

		private IServerChannelSinkProvider _nextProvider;

		public EncryptionServerSinkProvider(IDictionary properties, ICollection providerData) 
		{ 
			_encryptionAlgorithm = (String) properties["algorithm"];
			String keyfile = (String) properties["keyfile"];

			if (_encryptionAlgorithm == null || keyfile == null) 
			{
				throw new RemotingException("'algorithm' and 'keyfile' have to " +
					"be specified for EncryptionServerSinkProvider");
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

		public IServerChannelSinkProvider Next
		{
			get {return _nextProvider; }
			set {_nextProvider = value;}
		}

		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			// create other sinks in the chain
			IServerChannelSink next = _nextProvider.CreateSink(channel);				
	
			// put our sink on top of the chain and return it				
			return new EncryptionServerSink(next,
				_encryptionKey,_encryptionAlgorithm);
		}

		public void GetChannelData(IChannelDataStore channelData)
		{
			// not yet needed
		}

	}
}
