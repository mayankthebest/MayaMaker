using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MayaMaker.Services.Models
{
    public static class DbInitializer
    {
        public static void Initialize(MayaMakerContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Patients.Any())
            {
                var patients = new List<Patient>();
                var nks = new List<PatientKin>();
                var allPatientRawData = File.ReadAllLines("SampleData\\patients.csv");
                var femaleNames = File.ReadAllLines("SampleData\\FemaleGivenName.txt");
                var maleNames = File.ReadAllLines("SampleData\\MaleGivenName.txt");
                var lastNames = File.ReadAllLines("SampleData\\FamilyName.txt");
                var phoneNumbers = File.ReadAllLines("SampleData\\PhoneNumber.txt");
                var streetNumbers = File.ReadAllLines("SampleData\\Street Number.txt");
                var streetNames = File.ReadAllLines("SampleData\\Street.txt");
                var cityNames = File.ReadAllLines("SampleData\\City.txt");
                var stateNames = File.ReadAllLines("SampleData\\State.txt");
                var countryNames = File.ReadAllLines("SampleData\\Country.txt");
                var zipCodes = File.ReadAllLines("SampleData\\ZipCode.txt");
                for (int i = 1; i < allPatientRawData.Length; i++)
                {
                    var rawData = allPatientRawData[i].Split(',');
                    DateTime.TryParse(rawData[1], out DateTime birthDate);
                    DateTime.TryParse(rawData[2], out DateTime deathDate);
                    var patient = new Patient
                    {
                        Id = Guid.Parse(rawData[0]),
                        Birthdate = birthDate,
                        Deathdate = deathDate,
                        Ssn = rawData[3],
                        Passport = rawData[5],
                        Prefix = rawData[6],
                        FirstName = rawData[7],
                        LastName = rawData[8],
                        Suffix = rawData[9],
                        MaidenName = rawData[10],
                        MaritalStatus = rawData[11],
                        Race = rawData[12],
                        Ethnicity = rawData[13],
                        Gender = rawData[14],
                        Birthplace = rawData[15],
                        Address = rawData[16],
                        City = rawData[17],
                        State = rawData[18],
                        County = rawData[19],
                        Zip = rawData[20]
                    };
                    Random rand = new Random();
                    int numberOfKins = rand.Next(0, 5);
                    if(numberOfKins > 0)
                    {
                        for (int j = 0; j < numberOfKins; j++)
                        {
                            PatientKin kin = new PatientKin();
                            kin.Patient = patient;
                            kin.LastName = lastNames[rand.Next(0, lastNames.Length - 1)];
                            kin.Role = "Next of Kin";
                            kin.Street = $"{streetNumbers[rand.Next(0, streetNumbers.Length - 1)]}, {streetNames[rand.Next(0, streetNames.Length - 1)]}";
                            kin.City = cityNames[rand.Next(0, cityNames.Length - 1)];
                            kin.State = stateNames[rand.Next(0, stateNames.Length - 1)];
                            kin.Country = countryNames[rand.Next(0, countryNames.Length - 1)];
                            kin.Zip = zipCodes[rand.Next(0, zipCodes.Length - 1)];
                            kin.HomePhoneNumber = phoneNumbers[rand.Next(0, phoneNumbers.Length - 1)];
                            kin.BusinessPhoneNumber = phoneNumbers[rand.Next(0, phoneNumbers.Length - 1)];

                            if (j % 2==0)
                            {
                                kin.FirstName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
                                kin.Relationship = "Sister";
                            }
                            else
                            {
                                kin.FirstName = maleNames[rand.Next(0, maleNames.Length - 1)];
                                kin.Relationship = "Brother";
                            }

                            nks.Add(kin);
                        }
                        
                    }

                    patients.Add(patient);
                }

                foreach (Patient p in patients)
                {
                    context.Patients.Add(p);
                }
                foreach (PatientKin patientKin in nks)
                {
                    context.PatientKins.Add(patientKin);
                }
            }

            if (!context.Hospitals.Any())
            {
                var hospitals = new List<Hospital>();
                var allHospitalRawData = File.ReadAllLines("SampleData\\organizations.csv");
                for (int i = 1; i < allHospitalRawData.Length; i++)
                {
                    var rawData = allHospitalRawData[i].Split(',');
                    DateTime.TryParse(rawData[1], out DateTime birthDate);
                    DateTime.TryParse(rawData[2], out DateTime deathDate);
                    var hospital = new Hospital
                    {
                        Id = Guid.Parse(rawData[0]),
                        Name = rawData[1],
                        Phone = rawData[8],
                        Address = rawData[2],
                        City = rawData[3],
                        State = rawData[4],
                        Zip = rawData[5]
                    };
                    hospitals.Add(hospital);
                }

                foreach (Hospital h in hospitals)
                {
                    context.Hospitals.Add(h);
                }
            }

            if (!context.Doctors.Any())
            {
                var Doctors = new List<Doctor>();
                var allDoctorRawData = File.ReadAllLines("SampleData\\providers.csv");
                for (int i = 1; i < allDoctorRawData.Length; i++)
                {
                    var rawData = allDoctorRawData[i].Split(',');
                    DateTime.TryParse(rawData[1], out DateTime birthDate);
                    DateTime.TryParse(rawData[2], out DateTime deathDate);
                    var Doctor = new Doctor
                    {
                        Id = Guid.Parse(rawData[0]),
                        AssignedHospital = context.Hospitals.Where(x=>x.Id == Guid.Parse(rawData[1])).FirstOrDefault(),
                        Name = rawData[2],
                        Speciality = rawData[4],
                        Gender = rawData[3],
                        Address = rawData[5],
                        City = rawData[6],
                        State = rawData[7],
                        Zip = rawData[8]
                    };
                    Doctors.Add(Doctor);
                }

                foreach (Doctor p in Doctors)
                {
                    context.Doctors.Add(p);
                }
            }

            if (!context.Encounters.Any())
            {
                var Encounters = new List<Encounter>();
                var allEncounterRawData = File.ReadAllLines("SampleData\\encounters.csv");
                for (int i = 1; i < allEncounterRawData.Length; i++)
                {
                    var rawData = allEncounterRawData[i].Split(',');
                    DateTime.TryParse(rawData[1], out DateTime startDate);
                    DateTime.TryParse(rawData[2], out DateTime endDate);
                    Guid.TryParse(rawData[4], out Guid doctorId);
                    Guid.TryParse(rawData[3], out Guid patientId);
                    var encounter = new Encounter
                    {
                        Id = Guid.Parse(rawData[0]),
                        Code = rawData[7],
                        Description = rawData[8],
                        AssignedDoctor = context.Doctors.Where(x => x.AssignedHospital.Id == doctorId).FirstOrDefault(),
                        EncounterClass = rawData[6],
                        EndDate = endDate,
                        Patient = context.Patients.Where(x => x.Id == patientId).FirstOrDefault(),
                        ReasonCode = rawData[12],
                        ReasonDescription = rawData[13],
                        StartDate = startDate
                    };

                    Encounters.Add(encounter);
                }

                foreach (Encounter p in Encounters)
                {
                    context.Encounters.Add(p);
                }
            }

            if(!context.Scenarios.Any())
            {
                var scenarios = new List<Scenario>();
                scenarios.Add(new Scenario
                {
                    Name = "Stanard Life cycle",
                    Description = "Standard life cycle of an outpatient in a hospital",
                    MessageTypes = "A01,A04,A03"
                });
                scenarios.Add(new Scenario
                {
                    Name = "Outpatient to inpatient",
                    Description = "Outpatient to inpatient",
                    MessageTypes = "A01,A06,A02,A08,A03"
                });
                scenarios.Add(new Scenario
                {
                    Name = "Patient discharge cancelled and restarted",
                    Description = "Patient discharge cancelled and restarted",
                    MessageTypes = "A01,A02,A08,A03,A13,A03"
                });
                scenarios.Add(new Scenario
                {
                    Name = "Patient Admit cancelled",
                    Description = "Patient Admit cancelled",
                    MessageTypes = "A01,A11,A01,A03"
                });
                scenarios.Add(new Scenario
                {
                    Name = "Pending discharge scenario",
                    Description = "Pending discharge scenario",
                    MessageTypes = "A01,A16,A15,A02,A08,A03"
                });

                foreach (Scenario s in scenarios)
                {
                    context.Scenarios.Add(s);
                }
            }

            context.SaveChanges();
        }
    }
}
