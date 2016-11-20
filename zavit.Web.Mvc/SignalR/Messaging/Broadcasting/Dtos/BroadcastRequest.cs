using System.Collections.Generic;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos
{
    public class BroadcastRequest<TDto>
    {
        public IList<string> GroupIds { get; set; }  
        public string[] ConnectionIdsToExclude { get; set; } 
        public TDto Dto { get; set; }
    }
}