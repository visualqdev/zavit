using System;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Messages
{
    public class NewMessageProvider : INewMessageProvider
    {
        readonly IDateTime _dateTime;
        readonly IGuid _guid;

        public NewMessageProvider(IDateTime dateTime, IGuid guid)
        {
            _dateTime = dateTime;
            _guid = guid;
        }

        public Message Provide(NewMessageRequest newMessageRequest)
        {
            return new Message
            {
                Body = newMessageRequest.Body,
                Sender = newMessageRequest.Sender,
                SentOn = _dateTime.UtcNow,
                Stamp = newMessageRequest.Stamp == Guid.Empty ? _guid.NewGuid() : newMessageRequest.Stamp
            };
        }
    }
}