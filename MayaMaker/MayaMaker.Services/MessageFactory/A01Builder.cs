using NHapi.Base.Model;
using NHapi.Model.V23.Message;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A01Builder : IBuildMessage
    {
        public async Task<IMessage> BuildMessage(IMessage messageToBuild)
        {
            var a01Message = (messageToBuild as ADT_A01);
            var nk1 = a01Message.AddNK1();
            return a01Message;
        }
    }
}
