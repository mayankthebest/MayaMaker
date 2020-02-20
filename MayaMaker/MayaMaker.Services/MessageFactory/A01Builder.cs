using NHapi.Base.Model;
using System;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A01Builder : IBuildMessage
    {
        public async Task<IMessage> BuildMessage(IMessage messageToBuild)
        {
            throw new NotImplementedException();
        }
    }
}
