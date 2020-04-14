using System.Collections.Generic;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public interface IMessageManager
    {
        Task<List<string>> GetAdtMessagesForOneEncounter(int scenarioId = 0);

        Task<List<string>> GetAllAdtMessages();
    }
}
