using MayaMaker.Services.MessageFactory;
using MayaMaker.Services.Models;
using Microsoft.EntityFrameworkCore;
using NHapi.Base.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public class MessageManager : IMessageManager
    {
        readonly MayaMakerContext _dbContext = null;
        readonly IMessageFactory _messageFactory = null;

        public MessageManager(MayaMakerContext dbContext, IMessageFactory messageFactory)
        {
            _dbContext = dbContext;
            _messageFactory = messageFactory;
        }

        public async Task<List<string>> GetAdtMessagesForOneEncounter()
        {
            Random rand = new Random();
            var patientCount = _dbContext.Patients.Count();
            var patient = _dbContext.Patients.Include(x => x.Kins).Skip(rand.Next(0, patientCount - 2)).Take(1).First();
            return await GetParsedMessage(patient, 1);
        }

        public async Task<List<string>> GetAllAdtMessages()
        {
            Random rand = new Random();
            var patientCount = _dbContext.Patients.Count();
            var patient = _dbContext.Patients.Skip(rand.Next(0, patientCount - 2)).Take(1).First();
            return await GetParsedMessage(patient, _dbContext.Encounters.Where(x => x.Patient == patient).Count());
        }

        private async Task<List<string>> GetParsedMessage(Patient patient, int noOfEncountersToProcess = 1)
        {
            List<string> outputs = new List<string>();
            PipeParser parser = new PipeParser();
            Random rand = new Random();
            int currentlyProcessing = 0;
            while (currentlyProcessing < noOfEncountersToProcess)
            {
                var encounters = GetConsecutiveEncounters(patient, currentlyProcessing);
                var timeDiff = (encounters[1].StartDate - encounters[0].StartDate).Ticks;
                var scenariosCount = _dbContext.Scenarios.Count();
                var scenarioToExecute = _dbContext.Scenarios.Skip(rand.Next(0, scenariosCount - 2)).Take(1).First();
                var messageTypes = scenarioToExecute.MessageTypes.Split(',');
                int interval = 0;
                foreach (var messageType in messageTypes)
                {
                    var parsedType = (MessageType)Enum.Parse(typeof(MessageType), messageType);
                    var message = await _messageFactory.CreateMessage(parsedType, encounters[0].StartDate.AddTicks((timeDiff / messageTypes.Count()) * interval), patient, encounters[0]);
                    outputs.Add(parser.Encode(message));
                    interval++;
                }
            }

            return outputs;
        }

        private List<Encounter> GetConsecutiveEncounters(Patient patient, int skip)
        {
            return _dbContext.Encounters.
                Include(x => x.AssignedDoctor).
                Include(x => x.AssignedDoctor.AssignedHospital).
                Where(x => x.Patient == patient).
                OrderBy(y => y.StartDate).
                Skip(skip).
                Take(2).
                ToList();
        }
    }
}
