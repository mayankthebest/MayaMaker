using MayaMaker.Services.MessageFactory;
using MayaMaker.Services.Models;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetAllAdtMessages()
        {
            throw new NotImplementedException();
        }
    }
}
