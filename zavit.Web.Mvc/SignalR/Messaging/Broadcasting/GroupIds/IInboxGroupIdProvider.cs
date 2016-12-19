namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds
{
    public interface IInboxGroupIdProvider
    {
        string Provide(int accountId);
    }
}