using System.Collections.Generic;

namespace MayaMaker.Services.Writers
{
    public interface IMessageWriter
    {
        void WriteAllMessages(List<string> messages);
    }
}
