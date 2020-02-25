using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A15Builder : BaseAdtMessageBuilder, IBuildMessage
    {
        public async Task<IMessage> BuildMessage(DateTime messageTime, Patient patient, Encounter encounter)
        {
            MessageTime = messageTime;
            MessageType = MessageType.A15;
            Patient = patient;
            Encounter = encounter;
            CreateMessageWithHeaderValues();
            CreateEvnSegment();
            CreatePidSegment();
            CreatePv1Segment();
            return Message;
        }
    }
}