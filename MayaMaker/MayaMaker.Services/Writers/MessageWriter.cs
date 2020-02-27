using System;
using System.Collections.Generic;
using System.IO;

namespace MayaMaker.Services.Writers
{
    public class MessageWriter : IMessageWriter
    {
        /// <summary>
        /// This write all messages to a txt file on server. Change this method if you need messages to be stored elsewhere (Azure Blob Storage, Database, etc.)
        /// </summary>
        /// <param name="messages">HL7 Messages to write</param>
        public void WriteAllMessages(List<string> messages)
        {
            //TO DO: Add code to write to a central location
        }
    }
}
