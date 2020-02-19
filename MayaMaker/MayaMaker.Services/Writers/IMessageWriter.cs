using NHapi.Base.Model;
using System.Collections.Generic;

namespace MayaMaker.Services.Writers
{
    public interface IMessageWriter
    {
        void WriteAllMessages(List<IMessage> messages);
    }
}
