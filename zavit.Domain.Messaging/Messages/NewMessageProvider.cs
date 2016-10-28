using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Messages
{
    public class NewMessageProvider : INewMessageProvider
    {
        readonly IDateTime _dateTime;

        public NewMessageProvider(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public Message Provide(NewMessageRequest newMessageRequest)
        {
            return new Message
            {
                Body = newMessageRequest.Body,
                Sender = newMessageRequest.Sender,
                SentOn = _dateTime.UtcNow
            };
        }
    }
}