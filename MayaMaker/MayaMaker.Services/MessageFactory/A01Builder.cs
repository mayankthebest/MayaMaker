using MayaMaker.Services.Models;
using NHapi.Base.Model;
using NHapi.Model.V23.Message;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A01Builder : IBuildMessage
    {
        private ADT_A01 a01Message = null;

        public async Task<IMessage> BuildMessage(IMessage messageToBuild, DateTime messageTime, Patient patient, Encounter encounter)
        {
            a01Message = (messageToBuild as ADT_A01);
            CreateEvnSegment(messageTime);
            CreatePidSegment();
            CreatePv1Segment(messageTime);
            var nk1 = a01Message.AddNK1();
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
            patientName.FamilyName.Value = "Mouse";
            patientName.GivenName.Value = "Mickey";
            pid.SetIDPatientID.Value = "378785433211";
            var patientAddress = pid.GetPatientAddress(0);
            patientAddress.StreetAddress.Value = "123 Main Street";
            patientAddress.City.Value = "Lake Buena Vista";
            patientAddress.StateOrProvince.Value = "FL";
            patientAddress.Country.Value = "USA";
        }

        private void CreatePv1Segment(DateTime messageTime)
        {
            var pv1 = a01Message.PV1;
            pv1.PatientClass.Value = "O"; // to represent an 'Outpatient'
            var assignedPatientLocation = pv1.AssignedPatientLocation;
            assignedPatientLocation.Facility.NamespaceID.Value = "Some Treatment Facility";
            assignedPatientLocation.PointOfCare.Value = "Some Point of Care";
            pv1.AdmissionType.Value = "ALERT";
            var referringDoctor = pv1.GetReferringDoctor(0);
            referringDoctor.IDNumber.Value = "99999999";
            referringDoctor.FamilyName.Value = "Smith";
            referringDoctor.GivenName.Value = "Jack";
            referringDoctor.IdentifierTypeCode.Value = "456789";
            pv1.AdmitDateTime.TimeOfAnEvent.Value = messageTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }
    }
}
