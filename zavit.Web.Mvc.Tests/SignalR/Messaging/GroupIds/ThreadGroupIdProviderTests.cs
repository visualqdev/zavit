using Machine.Specifications;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.GroupIds 
{
    [Subject("ThreadGroupIdProvider")]
    public class ThreadGroupIdProviderTests
    {
        class When_providing_a_group_id
        {
            Because of = () => _result = ThreadGroupIdProvider.Provide(MessageThreadId, AccountId);

            It should_return_a_formed_group_id_containing_message_thread_and_account_ids = 
                () => _result.ShouldEqual("messagethread_1_2");

            static object _result;
            const string AccountId = "1";
            const string MessageThreadId = "2";
        }
    }
}

