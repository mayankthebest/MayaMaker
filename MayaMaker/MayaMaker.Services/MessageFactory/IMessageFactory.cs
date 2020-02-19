using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    public interface IMessageFactory
    {
        public IMessage CreateMessage(string messageType, DateTime messageTime, Patient patient, Encounter encounter);
    }
}
