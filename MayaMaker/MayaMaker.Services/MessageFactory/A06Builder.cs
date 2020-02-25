using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A06Builder : BaseAdtMessageBuilder, IBuildMessage
    {
        public async Task<IMessage> BuildMessage(DateTime messageTime, Patient patient, Encounter encounter)
        {
            MessageTime = messageTime;
            MessageType = MessageType.A06;
            Patient = patient;
            Encounter = encounter;
            CreateMessageWithHeaderValues();
            CreateEvnSegment();
            CreatePidSegment();
            CreatePv1Segment("Inpatient");
            return Message;
        }
    }
}