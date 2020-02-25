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
        public IMessage Message { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime MessageTime { get; set; }
        public Patient Patient { get; set; }
        public Encounter Encounter { get; set; }

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

        internal Type GetMessageType()
        {
            switch (MessageType)
            {
                case MessageType.A01:
                    return typeof(ADT_A01);

                case MessageType.A02:
                    return typeof(ADT_A02);

                case MessageType.A03:
                    return typeof(ADT_A03);

                case MessageType.A04:
                    return typeof(ADT_A04);

                case MessageType.A05:
                    return typeof(ADT_A05);

                case MessageType.A06:
                    return typeof(ADT_A06);

                case MessageType.A07:
                    return typeof(ADT_A07);

                case MessageType.A08:
                    return typeof(ADT_A08);

                case MessageType.A09:
                    return typeof(ADT_A09);

                case MessageType.A10:
                    return typeof(ADT_A10);

                case MessageType.A11:
                    return typeof(ADT_A11);

                case MessageType.A12:
                    return typeof(ADT_A12);

                case MessageType.A13:
                    return typeof(ADT_A13);

                case MessageType.A14:
                    return typeof(ADT_A14);

                case MessageType.A15:
                    return typeof(ADT_A15);

                case MessageType.A16:
                    return typeof(ADT_A16);

                case MessageType.A25:
                    return typeof(ADT_A25);

                case MessageType.A26:
                    return typeof(ADT_A26);

                case MessageType.A27:
                    return typeof(ADT_A27);

                case MessageType.A38:
                    return typeof(ADT_A38);

                default:
                    throw new ArgumentException($"'{MessageType}' is not supported yet. Extend this if you need to");
            }
        }

        internal dynamic GetProperty(string propertyName)
        {
            var type = GetMessageType();
            var propInfo = type.GetProperty(propertyName);
            return propInfo.GetValue(Message);
        }

        internal dynamic GetMethodWithSingleIntParameter(string methodName, object[] objArray)
        {
            var type = GetMessageType();
            var methodInfo = type.GetMethod(methodName, new Type[] { typeof(int) });
            return methodInfo.Invoke(Message, objArray);
        }

        internal void CreateMessageWithHeaderValues()
        {
            var type = GetMessageType();
            Message = Activator.CreateInstance(type) as IMessage;
            var header = GetProperty("MSH") as MSH;

            if (header != null)
            {
                header.FieldSeparator.Value = FieldSeparator;
                header.EncodingCharacters.Value = EncodingCharacters;
                header.SendingApplication.NamespaceID.Value = SendingApplicationName;
                header.SendingFacility.NamespaceID.Value = SendingFacilityName;
                header.ReceivingApplication.NamespaceID.Value = ReceivingApplicationName;
                header.ReceivingFacility.NamespaceID.Value = ReceivingFacilityName;
                header.DateTimeOfMessage.TimeOfAnEvent.Value = MessageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                header.MessageControlID.Value = FacilityNumber + MessageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                header.MessageType.MessageType.Value = MessageTypeString;
                header.MessageType.TriggerEvent.Value = MessageType.ToString();
                header.VersionID.Value = Version;
                header.ProcessingID.ProcessingID.Value = ProcessingID;
            }
        }

        internal void CreateEvnSegment()
        {
            var evn = GetProperty("EVN") as EVN;
            evn.EventTypeCode.Value = MessageType.ToString();
            evn.RecordedDateTime.TimeOfAnEvent.Value = MessageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        internal void CreatePidSegment()
        {
            var pid = GetProperty("PID") as PID;
            var patientName = pid.GetPatientName(0);
            patientName.FamilyName.Value = Patient.LastName;
            patientName.GivenName.Value = Patient.FirstName;
            pid.SetIDPatientID.Value = Patient.Id.ToString();
            var patientAddress = pid.GetPatientAddress(0);
            patientAddress.StreetAddress.Value = Patient.Address;
            patientAddress.City.Value = Patient.City;
            patientAddress.StateOrProvince.Value = Patient.State;
            patientAddress.Country.Value = "USA";
        }

        internal void CreatePv1Segment()
        {
            var pv1 = GetProperty("PV1") as PV1;
            pv1.PatientClass.Value = Encounter.Code;
            var assignedPatientLocation = pv1.AssignedPatientLocation;
            assignedPatientLocation.Facility.NamespaceID.Value = Encounter.AssignedDoctor.AssignedHospital.Name;
            assignedPatientLocation.PointOfCare.Value = Encounter.AssignedDoctor.AssignedHospital.Address;
            pv1.AdmissionType.Value = Encounter.EncounterClass;
            var referringDoctor = pv1.GetReferringDoctor(0);
            referringDoctor.IDNumber.Value = Encounter.AssignedDoctor.Id.ToString();
            referringDoctor.FamilyName.Value = Encounter.AssignedDoctor.Name.Split(' ')[0];
            referringDoctor.GivenName.Value = Encounter.AssignedDoctor.Name.Split(' ')[1];
            referringDoctor.IdentifierTypeCode.Value = Encounter.AssignedDoctor.Speciality;
            pv1.AdmitDateTime.TimeOfAnEvent.Value = MessageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        internal void CreateNk1Segment()
        {
            int i = 0;
            foreach (var kin in Patient.Kins)
            {
                var nk1 = GetMethodWithSingleIntParameter("GetNK1", new object[] { i }) as NK1;
                var name = nk1.GetName(0);
                name.GivenName.Value = kin.FirstName;
                name.FamilyName.Value = kin.LastName;
                nk1.ContactRole.Identifier.Value = kin.Role;
                nk1.Relationship.Identifier.Value = kin.Relationship;
                var address = nk1.GetAddress(0);
                address.StreetAddress.Value = kin.Street;
                address.City.Value = kin.City;
                address.StateOrProvince.Value = kin.State;
                address.Country.Value = kin.Country;
                address.ZipOrPostalCode.Value = kin.Zip;
                var homePhone = nk1.GetPhoneNumber(0);
                homePhone.PhoneNumber.Value = kin.HomePhoneNumber;
                var businessPhone = nk1.GetPhoneNumber(0);
                businessPhone.PhoneNumber.Value = kin.BusinessPhoneNumber;
                i++;
            }
        }

        internal void CreateOBX()
        {
            var obx = GetMethodWithSingleIntParameter("GetOBX", new object[] { 0 }) as OBX;
            obx.ObservationIdentifier.Identifier.Value = Encounter.Code;
            obx.ObservationIdentifier.Text.Value = Encounter.Description;
        }

        internal void CreatePd1Segment()
        {
            var pd1 = GetProperty("PD1") as PD1;
            pd1.OrganDonor.Value = "true";
            pd1.Handicap.Value = "false";
            pd1.SeparateBill.Value = "true";
        }
    }
}
