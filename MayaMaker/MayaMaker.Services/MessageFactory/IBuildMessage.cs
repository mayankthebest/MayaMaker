using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    public interface IBuildMessage
    {
        Task<IMessage> BuildMessage(IMessage messageToBuild, DateTime messageTime, Patient patient, Encounter encounter);
    }
}
