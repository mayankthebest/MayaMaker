using NHapi.Base.Model;
using NHapi.Model.V23.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal abstract class BaseAdtMessageFactory
    {
        public string SendingApplicationName { get; set; }
        public string ReceivingApplicationName { get; set; }

        internal IMessage CreateMessageWithHeaderValues(string messageType, DateTime messageTime)
        {
            throw new NotImplementedException();
        }
    }
}
