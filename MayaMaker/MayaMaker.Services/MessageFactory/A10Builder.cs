﻿using NHapi.Base.Model;
using System.Threading.Tasks;

namespace MayaMaker.Services.MessageFactory
{
    internal class A10Builder : IBuildMessage
    {
        public Task<IMessage> BuildMessage(IMessage messageToBuild)
        {
            throw new System.NotImplementedException();
        }
    }
}