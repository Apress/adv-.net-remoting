using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace ContextBound
{

   public class CheckerSink: IMessageSink
   {
      IMessageSink _nextSink;
      String _mType;
      public CheckerSink(IMessageSink nextSink)
      {
         _nextSink = nextSink;
      }

      public IMessage SyncProcessMessage(IMessage msg) 
      {
         Console.WriteLine("CheckerSink is intercepting a call");
         DoCheck(msg);
         return _nextSink.SyncProcessMessage(msg);
      }

      public IMessageCtrl AsyncProcessMessage(IMessage msg, 
         IMessageSink replySink) 
      {
         DoCheck(msg);
         return _nextSink.AsyncProcessMessage(msg,replySink);
      }

      public IMessageSink NextSink 
      {
         get 
         {
            return _nextSink;
         }
      }

      private void DoCheck(IMessage imsg) 
      {
         // not interested in IConstructionCallMessages
         if (imsg as IConstructionCallMessage != null)  return;

         // but only interested in IMethodMessages
         IMethodMessage msg = imsg as IMethodMessage;
         if (msg == null) return;


         // Check for the Attribute
         MemberInfo methodbase = msg.MethodBase;

         object[] attrs = methodbase.GetCustomAttributes(false);

         foreach (Attribute attr in attrs) 
         {
            CheckAttribute check = attr as CheckAttribute;

            // only interested in CheckAttributes 
            if (check == null) continue;
            
            // if the method only has one parameter, place the check directly
            // on it (needed for property set methods)
            if (msg.ArgCount == 1) 
            {
               check.DoCheck(msg.Args[0]);
            }
         }

         // check the Attribute for each parameter of this method
         ParameterInfo[] parms = msg.MethodBase.GetParameters();

         for (int i = 0;i<parms.Length;i++) 
         {
            attrs = parms[i].GetCustomAttributes(false);
            foreach (Attribute attr in attrs) 
            {
               CheckAttribute check = attr as CheckAttribute;

               // only interested in CheckAttributes 
               if (check == null) continue;
            
               // if the method only has one parameter, place the check directly
               // on it (needed for property set methods)

               check.DoCheck(msg.Args[i]);
            }
         }
      }


   }
}
