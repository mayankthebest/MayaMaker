using System;
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

        [HttpGet("GetAll")]
        public async Task<string> GetAll()
        {
            var messages = await _messageManager.GetAllAdtMessages();
            _messageWriter.WriteAllMessages(messages);
            return string.Join(Environment.NewLine, messages);
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var messages = await _messageManager.GetAdtMessagesForOneEncounter();
            _messageWriter.WriteAllMessages(messages);
            return string.Join(Environment.NewLine, messages);
        }
    }
}
