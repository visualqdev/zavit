namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds
{
    public class InboxGroupIdProvider : IInboxGroupIdProvider
    {
        public static string Provide(string accountId)
        {
            return $"messageinbox_{accountId}";
        }

        public string Provide(int accountId)
        {
            return Provide(accountId.ToString());
        }
    }
}