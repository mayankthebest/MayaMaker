using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A04Builder : BaseAdtMessageBuilder, IBuildMessage
    {
        public async Task<IMessage> BuildMessage(DateTime messageTime, Patient patient, Encounter encounter)
        {
            MessageTime = messageTime;
            MessageType = MessageType.A04;
            Patient = patient;
            Encounter = encounter;
            CreateMessageWithHeaderValues();
            CreateProcedureGroup();
            CreateEvnSegment();
            CreatePidSegment();
            CreatePv1Segment();
            CreatePd1Segment();
            CreateNk1Segment();
            CreateOBX();
            return Message;
        }
    }
}