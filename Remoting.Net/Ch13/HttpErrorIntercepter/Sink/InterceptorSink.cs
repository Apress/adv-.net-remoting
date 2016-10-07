using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;

// This sink was inspired by an idea of Richard Blewett, and his weblog post at 
// http://staff.develop.com/richardb/weblog/commentview.aspx/827189d3-ee0e-444f-b01d-bf9ce9f70f5c

// I admit that I even had a quick glance at his source code, but decided to solve
// the task differently ;-)

namespace HttpErrorInterceptor
{
   public class InterceptorSink : BaseChannelObjectWithProperties, IClientChannelSink
   {	
      private IClientChannelSink _next;

      public InterceptorSink (IClientChannelSink next) 
      {
         _next = next;
      }

      public IClientChannelSink NextChannelSink {get{return _next;}}

      // Methods
      public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, 
         IMessage msg, 
         ITransportHeaders headers, 
         Stream stream) 
      {
         sinkStack.Push(this,null);
         _next.AsyncProcessRequest( sinkStack, msg, headers, stream);
      }

      public void AsyncProcessResponse(
         IClientResponseChannelSinkStack sinkStack, 
         object state, 
         ITransportHeaders headers, 
         Stream stream)
      {
         Exception ex = GetExceptionIfNecessary(ref headers, ref stream);
         if (ex!=null) 
         {
            sinkStack.DispatchException(ex);
         }
         else
         {
            sinkStack.AsyncProcessResponse(headers, stream);
         }
      }

      public System.IO.Stream GetRequestStream(IMessage msg, 
         ITransportHeaders headers)
      {
         return null;
      }

      public void ProcessMessage(IMessage msg, 
         ITransportHeaders requestHeaders, 
         Stream requestStream, 
         out ITransportHeaders responseHeaders, 
         out Stream responseStream)
      {
         _next.ProcessMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream);
         Exception ex = GetExceptionIfNecessary(ref responseHeaders, ref responseStream);
         if (ex!=null) throw ex;
      }

      private Exception GetExceptionIfNecessary(ref ITransportHeaders headers, ref Stream stream)
      {
         int chunksize=0x400;
         MemoryStream ms = new MemoryStream();

         string ct = headers["Content-Type"] as String;

         if (ct==null || ct != "application/octet-stream")
         {
            byte[] buf = new byte[chunksize];
            StringBuilder bld = new StringBuilder();
            for (int size = stream.Read(buf, 0, chunksize); size > 0; size = stream.Read(buf, 0, chunksize))
            {
               bld.Append(Encoding.ASCII.GetString(buf, 0, size));
            }
            return new RemotingException(bld.ToString());
         }

         return null;
      }
   }
}
