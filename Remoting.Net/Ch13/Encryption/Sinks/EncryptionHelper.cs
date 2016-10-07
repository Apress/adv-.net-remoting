using System;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionSink
{

	public class EncryptionHelper
	{

		public static Stream ProcessOutboundStream(
			Stream inStream, 
			String algorithm,
			byte[] encryptionkey,
			out byte[] encryptionIV) 
		{
			Stream outStream = new System.IO.MemoryStream();

			// setup the encryption properties
			SymmetricAlgorithm alg = SymmetricAlgorithm.Create(algorithm);
			alg.Key = encryptionkey;
			alg.GenerateIV();
			encryptionIV = alg.IV;

			CryptoStream encryptStream = new CryptoStream(
				outStream,
				alg.CreateEncryptor(),
				CryptoStreamMode.Write);

			// write the whole contents through the new streams
			byte[] buf = new Byte[1000];
			int cnt = inStream.Read(buf,0,1000);
			while (cnt>0) 
			{
				encryptStream.Write(buf,0,cnt);
				cnt = inStream.Read(buf,0,1000);
			}
			encryptStream.FlushFinalBlock();
			outStream.Seek(0,SeekOrigin.Begin);
			return outStream;
		}

		public static Stream ProcessInboundStream(Stream inStream,
			String algorithm,
			byte[] encryptionkey,
			byte[] encryptionIV) 
		{
			// setup decryption properties
			SymmetricAlgorithm alg = SymmetricAlgorithm.Create(algorithm);
			alg.Key = encryptionkey;
			alg.IV = encryptionIV;

			// add the decryptor layer to the stream
			Stream outStream = new CryptoStream(inStream,
				alg.CreateDecryptor(),
				CryptoStreamMode.Read);

			return outStream;
		}

	}
}
