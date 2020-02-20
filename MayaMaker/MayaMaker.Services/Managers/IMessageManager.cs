using System.Collections.Generic;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public interface IMessageManager
    {
        Task<List<string>> GetAdtMessagesForOneEncounter();

        Task<List<string>> GetAllAdtMessages();
    }
}
