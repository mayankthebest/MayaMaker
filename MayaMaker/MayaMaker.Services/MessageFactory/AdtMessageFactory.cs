using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class AdtMessageFactory : BaseAdtMessageFactory, IMessageFactory
    {
        public AdtMessageFactory() : base()
        {

        }

        public IMessage CreateMessage(string messageType, DateTime messageTime, Patient patient, Encounter encounter)
        {
            var message = base.CreateMessageWithHeaderValues(messageType, messageTime);
            return message;
        }
    }
}
