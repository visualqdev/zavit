using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Tests.MessageThreads 
{
    [Subject("NewMessageThreadProvider")]
    public class NewMessageThreadProviderTests : TestOf<NewMessageThreadProvider>
    {
        class When_providing_a_new_message_thread
        {
            Because of = () => _result = Subject.Provide(_newMessageThreadRequest);

            It should_set_the_thread_participants_to_all_participants_specified_by_the_request = 
                () => _result.Participants.ShouldContainOnly(_participant, _otherParticipant);

            It should_set_the_thread_creation_date_and_time_to_the_current_utc_date_time =
                () => _result.CreatedOn.ShouldEqual(Injected<IDateTime>().UtcNow);

            Establish context = () =>
            {
                _newMessageThreadRequest = NewInstanceOf<NewMessageThreadRequest>();
                _newMessageThreadRequest.ParticipantIds = new[] { 12, 15 };
                
                _participant = NewInstanceOf<Account>();
                _otherParticipant = NewInstanceOf<Account>();
                Injected<IAccountRepository>()
                    .Stub(r => r.GetAccounts(_newMessageThreadRequest.ParticipantIds))
                    .Return(new List<Account> {_participant, _otherParticipant});

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(new DateTime(2011, 10, 11));
            };

            static NewMessageThreadRequest _newMessageThreadRequest;
            static MessageThread _result;
            static Account _participant;
            static Account _otherParticipant;
        }
    }
}

