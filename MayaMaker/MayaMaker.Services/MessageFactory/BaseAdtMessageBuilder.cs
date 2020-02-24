using MayaMaker.Services.Models;
using NHapi.Base.Model;
using NHapi.Model.V23.Message;
using NHapi.Model.V23.Segment;
using System;
using System.Globalization;

namespace MayaMaker.Services.MessageFactory
{
    public class BaseAdtMessageBuilder
    {
        public string FieldSeparator { get; set; }
        public string EncodingCharacters { get; set; }
        public string SendingApplicationName { get; set; }
        public string SendingFacilityName { get; set; }
        public string ReceivingApplicationName { get; set; }
        public string ReceivingFacilityName { get; set; }
        public string FacilityNumber { get; set; }
        public string MessageTypeString { get; set; }
        public string Version { get; set; }
        public string ProcessingID { get; set; }

        public BaseAdtMessageBuilder()
        {
            FieldSeparator = "|";
            EncodingCharacters = "^~\\&";
            SendingApplicationName = "MayaMaker";
            SendingFacilityName = "MayaHospital";
            ReceivingApplicationName = "DCC";
            ReceivingFacilityName = "Salford";
            FacilityNumber = "1234";
            MessageTypeString = "ADT";
            Version = "2.3";
            ProcessingID = "P";
        }

        internal IMessage CreateMessageWithHeaderValues(MessageType messageType, DateTime messageTime)
        {
            IMessage message = null;
            MSH header = null;

            switch (messageType)
            {
                case MessageType.A01:
                    message = new ADT_A01();
                    header = (message as ADT_A01).MSH;
                    break;
                case MessageType.A02:
                    message = new ADT_A02();
                    header = (message as ADT_A02).MSH;
                    break;
                case MessageType.A03:
                    message = new ADT_A03();
                    header = (message as ADT_A03).MSH;
                    break;
                case MessageType.A04:
                    message = new ADT_A04();
                    header = (message as ADT_A04).MSH;
                    break;
                case MessageType.A05:
                    message = new ADT_A05();
                    header = (message as ADT_A05).MSH;
                    break;
                case MessageType.A06:
                    message = new ADT_A06();
                    header = (message as ADT_A06).MSH;
                    break;
                case MessageType.A07:
                    message = new ADT_A07();
                    header = (message as ADT_A07).MSH;
                    break;
                case MessageType.A08:
                    message = new ADT_A08();
                    header = (message as ADT_A08).MSH;
                    break;
                case MessageType.A09:
                    message = new ADT_A09();
                    header = (message as ADT_A09).MSH;
                    break;
                case MessageType.A10:
                    message = new ADT_A10();
                    header = (message as ADT_A10).MSH;
                    break;
                case MessageType.A11:
                    message = new ADT_A11();
                    header = (message as ADT_A11).MSH;
                    break;
                case MessageType.A12:
                    message = new ADT_A12();
                    header = (message as ADT_A12).MSH;
                    break;
                case MessageType.A13:
                    message = new ADT_A13();
                    header = (message as ADT_A13).MSH;
                    break;
                case MessageType.A14:
                    message = new ADT_A14();
                    header = (message as ADT_A14).MSH;
                    break;
                case MessageType.A15:
                    message = new ADT_A15();
                    header = (message as ADT_A15).MSH;
                    break;
                case MessageType.A16:
                    message = new ADT_A16();
                    header = (message as ADT_A16).MSH;
                    break;
                case MessageType.A25:
                    message = new ADT_A25();
                    header = (message as ADT_A25).MSH;
                    break;
                case MessageType.A26:
                    message = new ADT_A26();
                    header = (message as ADT_A26).MSH;
                    break;
                case MessageType.A27:
                    message = new ADT_A27();
                    header = (message as ADT_A27).MSH;
                    break;
                case MessageType.A38:
                    message = new ADT_A38();
                    header = (message as ADT_A38).MSH;
                    break;
                default:
                    throw new ArgumentException($"'{messageType}' is not supported yet. Extend this if you need to");
            }

            if (header != null)
            {
                header.FieldSeparator.Value = FieldSeparator;
                header.EncodingCharacters.Value = EncodingCharacters;
                header.SendingApplication.NamespaceID.Value = SendingApplicationName;
                header.SendingFacility.NamespaceID.Value = SendingFacilityName;
                header.ReceivingApplication.NamespaceID.Value = ReceivingApplicationName;
                header.ReceivingFacility.NamespaceID.Value = ReceivingFacilityName;
                header.DateTimeOfMessage.TimeOfAnEvent.Value = messageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                header.MessageControlID.Value = FacilityNumber + messageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                header.MessageType.MessageType.Value = MessageTypeString;
                header.MessageType.TriggerEvent.Value = messageType.ToString();
                header.VersionID.Value = Version;
                header.ProcessingID.ProcessingID.Value = ProcessingID;
            }

            return message;
        }
    }
}
