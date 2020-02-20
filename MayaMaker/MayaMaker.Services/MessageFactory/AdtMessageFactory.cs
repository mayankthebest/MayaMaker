using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class AdtMessageFactory : BaseAdtMessageFactory, IMessageFactory
    {
        public async Task<IMessage> CreateMessage(MessageType messageType, DateTime messageTime, Patient patient, Encounter encounter)
        {
            var message = base.CreateMessageWithHeaderValues(messageType, messageTime);
            var messageBuilder = base.GetMessageBuilder(messageType);
            return await messageBuilder.BuildMessage(message);
        }
    }
}
