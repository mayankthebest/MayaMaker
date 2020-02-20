using NHapi.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    public interface IBuildMessage
    {
        Task<IMessage> BuildMessage(IMessage messageToBuild);
    }
}
