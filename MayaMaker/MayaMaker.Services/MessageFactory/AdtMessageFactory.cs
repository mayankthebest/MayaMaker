using MayaMaker.Services.Models;
using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class AdtMessageFactory : IMessageFactory
    {
        public async Task<IMessage> CreateMessage(MessageType messageType, DateTime messageTime, Patient patient, Encounter encounter)
        {
            var messageBuilder = GetMessageBuilder(messageType);
            return await messageBuilder.BuildMessage(messageTime, patient, encounter);
        }

        internal IBuildMessage GetMessageBuilder(MessageType messageType)
        {
            IBuildMessage messageBuilder = null;

            switch (messageType)
            {
                case MessageType.A01:
                    messageBuilder = new A01Builder();
                    break;
                case MessageType.A02:
                    messageBuilder = new A02Builder();
                    break;
                case MessageType.A03:
                    messageBuilder = new A03Builder();
                    break;
                case MessageType.A04:
                    messageBuilder = new A04Builder();
                    break;
                case MessageType.A05:
                    messageBuilder = new A05Builder();
                    break;
                case MessageType.A06:
                    messageBuilder = new A06Builder();
                    break;
                case MessageType.A07:
                    messageBuilder = new A07Builder();
                    break;
                case MessageType.A08:
                    messageBuilder = new A08Builder();
                    break;
                case MessageType.A09:
                    messageBuilder = new A09Builder();
                    break;
                case MessageType.A10:
                    messageBuilder = new A10Builder();
                    break;
                case MessageType.A11:
                    messageBuilder = new A11Builder();
                    break;
                case MessageType.A12:
                    messageBuilder = new A12Builder();
                    break;
                case MessageType.A13:
                    messageBuilder = new A13Builder();
                    break;
                case MessageType.A14:
                    messageBuilder = new A14Builder();
                    break;
                case MessageType.A15:
                    messageBuilder = new A15Builder();
                    break;
                case MessageType.A16:
                    messageBuilder = new A16Builder();
                    break;
                case MessageType.A25:
                    messageBuilder = new A25Builder();
                    break;
                case MessageType.A26:
                    messageBuilder = new A26Builder();
                    break;
                case MessageType.A27:
                    messageBuilder = new A27Builder();
                    break;
                case MessageType.A38:
                    messageBuilder = new A38Builder();
                    break;
                default:
                    throw new ArgumentException($"'{messageType}' is not supported yet. Extend this if you need to");
            }

            return messageBuilder;
        }
    }
}
