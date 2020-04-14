using System.Collections.Generic;
using System.Threading.Tasks;
using MayaMaker.Services.Managers;
using MayaMaker.Services.Writers;
using Microsoft.AspNetCore.Mvc;

namespace MayaMaker.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageMakerController : ControllerBase
    {
        readonly IMessageManager _messageManager = null;
        readonly IMessageWriter _messageWriter = null;

        public MessageMakerController(IMessageManager messageManager, IMessageWriter messageWriter)
        {
            _messageManager = messageManager;
            _messageWriter = messageWriter;
        }

        [HttpGet("All")]
        public async Task<List<string>> GetAll()
        {
            var messages = await _messageManager.GetAllAdtMessages();
            _messageWriter.WriteAllMessages(messages);
            return messages;
        }

        [HttpGet]
        public async Task<List<string>> Get()
        {
            var messages = await _messageManager.GetAdtMessagesForOneEncounter();
            _messageWriter.WriteAllMessages(messages);
            return messages;
        }

        [HttpGet("{id}")]
        public async Task<List<string>> Get(int id)
        {
            var messages = await _messageManager.GetAdtMessagesForOneEncounter(id);
            _messageWriter.WriteAllMessages(messages);
            return messages;
        }
    }
}
