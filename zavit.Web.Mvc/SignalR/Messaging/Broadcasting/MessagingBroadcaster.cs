﻿using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Hubs;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting
{
    public class MessagingBroadcaster : IMessagingBroadcaster
    {
        public void ThreadMessageSent(BroadcastRequest<MessageDto> messageBroadcastRequest)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessagingHub>();
            var message = JsonConvert.SerializeObject(messageBroadcastRequest.Dto);
            var groups = context.Clients.Groups(messageBroadcastRequest.GroupIds, messageBroadcastRequest.ConnectionIdsToExclude);
            groups.threadNewMessage(message);
        }

        public void ThreadMessageRead(BroadcastRequest<ReadMessagesDto> readMessagesBroadcastRequest)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessagingHub>();
            var message = JsonConvert.SerializeObject(readMessagesBroadcastRequest.Dto);
            var groups = context.Clients.Groups(readMessagesBroadcastRequest.GroupIds, readMessagesBroadcastRequest.ConnectionIdsToExclude);
            groups.threadMessagesRead(message);
        }

        public void InboxMessageSent(BroadcastRequest<MessageDto> messageBroadcastRequest)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessagingHub>();
            var message = JsonConvert.SerializeObject(messageBroadcastRequest.Dto);
            var groups = context.Clients.Groups(messageBroadcastRequest.GroupIds, "");
            groups.inboxNewMessage(message);
        }
    }
}