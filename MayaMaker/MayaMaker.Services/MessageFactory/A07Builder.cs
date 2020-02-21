using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A07Builder : IBuildMessage
    {
        public Task<IMessage> BuildMessage(IMessage messageToBuild, DateTime messageTime, Patient patient, Encounter encounter)
        {
            throw new System.NotImplementedException();
        }
    }
}