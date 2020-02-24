using MayaMaker.Services.Models;
using NHapi.Base.Model;
using NHapi.Model.V23.Message;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A01Builder : BaseAdtMessageBuilder, IBuildMessage
    {
        private ADT_A01 a01Message = null;
        private Patient _patient = null;
        private Encounter _encounter;

        public async Task<IMessage> BuildMessage(DateTime messageTime, Patient patient, Encounter encounter)
        {
            var messageToBuild = CreateMessageWithHeaderValues(MessageType.A01, messageTime);
            _patient = patient;
            _encounter = encounter;
            a01Message = (messageToBuild as ADT_A01);
            CreateEvnSegment(messageTime);
            CreatePidSegment();
            CreatePv1Segment(messageTime);
            CreateNk1Segment();
            return a01Message;
        }

        private void CreateEvnSegment(DateTime messageTime)
        {
            var evn = a01Message.EVN;
            evn.EventTypeCode.Value = "A01";
            evn.RecordedDateTime.TimeOfAnEvent.Value = messageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        private void CreatePidSegment()
        {
            var pid = a01Message.PID;
            var patientName = pid.GetPatientName(0);
            patientName.FamilyName.Value = _patient.LastName;
            patientName.GivenName.Value = _patient.FirstName;
            pid.SetIDPatientID.Value = _patient.Id.ToString();
            var patientAddress = pid.GetPatientAddress(0);
            patientAddress.StreetAddress.Value = _patient.Address;
            patientAddress.City.Value = _patient.City;
            patientAddress.StateOrProvince.Value = _patient.State;
            patientAddress.Country.Value = "USA";
        }

        private void CreatePv1Segment(DateTime messageTime)
        {
            var pv1 = a01Message.PV1;
            pv1.PatientClass.Value = _encounter.Code;
            var assignedPatientLocation = pv1.AssignedPatientLocation;
            assignedPatientLocation.Facility.NamespaceID.Value = _encounter.AssignedDoctor.AssignedHospital.Name;
            assignedPatientLocation.PointOfCare.Value = _encounter.AssignedDoctor.AssignedHospital.Address;
            pv1.AdmissionType.Value = _encounter.EncounterClass;
            var referringDoctor = pv1.GetReferringDoctor(0);
            referringDoctor.IDNumber.Value = _encounter.AssignedDoctor.Id.ToString();
            referringDoctor.FamilyName.Value = _encounter.AssignedDoctor.Name.Split(' ')[0];
            referringDoctor.GivenName.Value = _encounter.AssignedDoctor.Name.Split(' ')[1];
            referringDoctor.IdentifierTypeCode.Value = _encounter.AssignedDoctor.Speciality;
            pv1.AdmitDateTime.TimeOfAnEvent.Value = messageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        private void CreateNk1Segment()
        {
            int i = 0;
            foreach (var kin in _patient.Kins)
            {
                var nk1 = a01Message.GetNK1(i);
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
    }
}
