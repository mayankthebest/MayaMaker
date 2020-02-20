using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    public interface IMessageFactory
    {
        public Task<IMessage> CreateMessage(MessageType messageType, DateTime messageTime, Patient patient, Encounter encounter);
    }
}
