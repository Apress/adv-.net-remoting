using System;
using System.IO;
using NZlib.Compression;
using NZlib.Streams;

namespace CompressionSink {

	public class CompressionHelper {

		public static Stream GetCompressedStreamCopy(Stream inStream) {
			Stream outStream = new System.IO.MemoryStream();
			DeflaterOutputStream compressStream = new DeflaterOutputStream(outStream,
				new Deflater(Deflater.BEST_COMPRESSION));

			byte[] buf = new Byte[1000];
			int cnt = inStream.Read(buf,0,1000);
			while (cnt>0) {
				compressStream.Write(buf,0,cnt);
				cnt = inStream.Read(buf,0,1000);
			}
			compressStream.Finish();
			compressStream.Flush();
			return outStream;
		}

		public static Stream GetUncompressedStreamCopy(Stream inStream) {
			return new InflaterInputStream(inStream);
		}
	}
}
