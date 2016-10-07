// DeflaterOutputStream.cs
// Copyright (C) 2001 Mike Krueger
//
// This file was translated from java, it was part of the GNU Classpath
// Copyright (C) 2001 Free Software Foundation, Inc.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// As a special exception, if you link this library with other files to
// produce an executable, this library does not by itself cause the
// resulting executable to be covered by the GNU General Public License.
// This exception does not however invalidate any other reasons why the
// executable file might be covered by the GNU General Public License.


using System;
using System.IO;

using NZlib.Compression;

namespace NZlib.Streams {
	
	/// <summary>
	/// This is a special FilterOutputStream deflating the bytes that are
	/// written through it.  It uses the Deflater for deflating.
	/// 
	/// authors of the original java version : Tom Tromey, Jochen Hoenicke 
	/// </summary>
	public class DeflaterOutputStream : Stream
	{
		/// <summary>
		/// This buffer is used temporarily to retrieve the bytes from the
		/// deflater and write them to the underlying output stream.
		/// </summary>
		protected byte[] buf;
		
		/// <summary>
		/// The deflater which is used to deflate the stream.
		/// </summary>
		protected Deflater def;
		
		/// <summary>
		/// base stream the deflater depends on.
		/// </summary>
		protected Stream baseOutputStream;
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override bool CanRead {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override bool CanSeek {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override bool CanWrite {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override long Length {
			get {
				return 0;
			}
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override long Position {
			get {
				return 0;
			}
			set {
				
			}
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0;
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override void SetLength(long value)
		{
			
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override int ReadByte()
		{
			return -1;
		}
		
		/// <summary>
		/// I needed to implement the abstract member.
		/// </summary>
		public override int Read(byte[] b, int off, int len)
		{
			return -1;
		}
		
		
		/// <summary>
		/// Deflates everything in the def's input buffers.  This will call
		/// <code>def.deflate()</code> until all bytes from the input buffers
		/// are processed.
		/// </summary>
		protected void deflate()
		{
			while (!def.NeedsInput()) {
				int len = def.Deflate(buf, 0, buf.Length);
				
				//	System.err.println("DOS deflated " + len + " baseOutputStream of " + buf.length);
				if (len <= 0) {
					break;
				}
				baseOutputStream.Write(buf, 0, len);
			}
			
			if (! def.NeedsInput()) {
				throw new Exception("Can't deflate all input?");
			}
		}
		
		/// <summary>
		/// Creates a new DeflaterOutputStream with a default Deflater and default buffer size.
		/// </summary>
		/// <param name="baseOutputStream">
		/// the output stream where deflated output should be written.
		/// </param>
		public DeflaterOutputStream(Stream baseOutputStream) : this(baseOutputStream, new Deflater(), 512)
		{
			
		}
		
		/// <summary>
		/// Creates a new DeflaterOutputStream with the given Deflater and
		/// default buffer size.
		/// </summary>
		/// <param name="baseOutputStream">
		/// the output stream where deflated output should be written.
		/// </param>
		/// <param name="defl">
		/// the underlying deflater.
		/// </param>
		public DeflaterOutputStream(Stream baseOutputStream, Deflater defl) :this(baseOutputStream, defl, 512)
		{
		}
		
		/// <summary>
		/// Creates a new DeflaterOutputStream with the given Deflater and
		/// buffer size.
		/// </summary>
		/// <param name="baseOutputStream">
		/// the output stream where deflated output should be written.
		/// </param>
		/// <param name="defl">
		/// the underlying deflater.
		/// </param>
		/// <param name="bufsize">
		/// the buffer size.
		/// </param>
		/// <exception cref="System.InvalidOperationException">
		/// if bufsize isn't positive.
		/// </exception>
		public DeflaterOutputStream(Stream baseOutputStream, Deflater defl, int bufsize)
		{
			this.baseOutputStream = baseOutputStream;
			if (bufsize <= 0) {
				throw new InvalidOperationException("bufsize <= 0");
			}
			buf = new byte[bufsize];
			def = defl;
		}
		
		/// <summary>
		/// Flushes the stream by calling flush() on the deflater and then
		/// on the underlying stream.  This ensures that all bytes are
		/// flushed.
		/// </summary>
		public override void Flush()
		{
			def.Flush();
			deflate();
			baseOutputStream.Flush();
		}
		
		/// <summary>
		/// Finishes the stream by calling finish() on the deflater. 
		/// </summary>
		public virtual void Finish()
		{
			def.Finish();
			while (!def.Finished()) {
				int len = def.Deflate(buf, 0, buf.Length);
				if (len <= 0) {
					break;
				}
				baseOutputStream.Write(buf, 0, len);
			}
			if (!def.Finished()) {
				throw new Exception("Can't deflate all input?");
			}
			baseOutputStream.Flush();
		}
		
		/// <summary>
		/// Calls finish () and closes the stream.
		/// </summary>
		public override void Close()
		{
			Finish();
			baseOutputStream.Close();
		}
		
		/// <summary>
		/// Writes a single byte to the compressed output stream.
		/// </summary>
		/// <param name="bval">
		/// the byte value.
		/// </param>
		public override void WriteByte(byte bval)
		{
			byte[] b = new byte[1];
			b[0] = (byte) bval;
			Write(b, 0, 1);
		}
		
		/// <summary>
		/// Writes a len bytes from an array to the compressed stream.
		/// </summary>
		/// <param name="buf">
		/// the byte array.
		/// </param>
		/// <param name="off">
		/// the offset into the byte array where to start.
		/// </param>
		/// <param name="len">
		/// the number of bytes to write.
		/// </param>
		public override void Write(byte[] buf, int off, int len)
		{
			//    System.err.println("DOS with off " + off + " and len " + len);
			def.SetInput(buf, off, len);
			deflate();
		}
	}
}

