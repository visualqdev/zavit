namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds
{
    public interface IThreadGroupIdProvider
    {
        string Provide(int messageThreadId, int accountId);
    }
}