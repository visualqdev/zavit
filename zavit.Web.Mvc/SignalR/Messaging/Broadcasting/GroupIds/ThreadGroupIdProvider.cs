namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds
{
    public class ThreadGroupIdProvider : IThreadGroupIdProvider
    {
        public string Provide(int messageThreadId, int accountId)
        {
            return Provide(messageThreadId.ToString(), accountId.ToString());
        }

        public static string Provide(string messageThreadId, string accountId)
        {
            return $"messagethread_{accountId}_{messageThreadId}";
        }
    }
}