using NHapi.Base.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MayaMaker.Services.Managers
{
    public interface IMessageManager
    {
        Task<List<IMessage>> GetAdtMessagesForOneEncounter();

        Task<List<IMessage>> GetAllAdtMessages();
    }
}
