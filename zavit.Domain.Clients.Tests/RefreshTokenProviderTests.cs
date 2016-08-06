using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Clients.Tokens;
using zavit.Domain.Shared;

namespace zavit.Domain.Clients.Tests 
{
    [Subject("RefreshTokenProvider")]
    public class RefreshTokenProviderTests : TestOf<RefreshTokenProvider>
    {
        class When_creating_a_refresh_token
        {
            Because of = () => _result = Subject.Create(TokenId, ClientId, UserName, ProtectedTicket);

            It should_set_the_client_to_be_the_client_from_repository_with_matching_id = () => _result.Client.ShouldEqual(_client);

            It should_set_the_subject_to_be_the_provided_username = () => _result.Subject.ShouldEqual(UserName);

            It should_set_the_protected_ticket_to_be_the_provided_protected_ticket = () => _result.ProtectedTicket.ShouldEqual(ProtectedTicket);

            It should_set_the_issued_utc_date_to_be_the_current_utc_date_and_time =
                () => _result.IssuedDateUtc.ShouldEqual(Injected<IDateTime>().UtcNow);

            It should_set_the_expiry_of_token_to_the_expiry_calculated_by_client =
                () => _result.ExpectedExpiryDateUtc.ShouldEqual(_expiryDate);

            It should_set_the_hashed_token_id_to_be_the_token_id_that_has_been_hashed =
                () => _result.HashedTokenId.ShouldEqual(HashedTokenId);

            Establish context = () =>
            {
                Injected<IDateTime>().Stub(d => d.UtcNow).Return(new DateTime(2015, 1, 1, 12, 0, 59));

                _expiryDate = new DateTime(2016, 1, 1, 12, 0, 59);
                _client = NewInstanceOf<Client>();
                _client.Stub(c => c.CalculateTokenExpiry(Injected<IDateTime>().UtcNow)).Return(_expiryDate);

                Injected<IClientRepository>().Stub(r => r.FindClient(ClientId)).Return(_client);

                Injected<ITokenSecurity>().Stub(s => s.HashTokenId(TokenId)).Return(HashedTokenId);
            };

            static RefreshToken _result;
            static Client _client;
            static DateTime _expiryDate;
            static string TokenId = "token ID";
            static string HashedTokenId = "Hashed Token ID";
            const int ClientId = 123;
            const string UserName = "Username";
            const string ProtectedTicket = "Protected Ticket";
        }

        class When_finding_an_exisitng_refresh_token
        {
            Because of = () => _result = Subject.FindExisting(RefreshTokenId);

            It should_return_the_refresh_token_retrieved_using_hashed_token_id = () => _result.ShouldEqual(_refreshToken);

            Establish context = () =>
            {
                const string hashedTokenId = "Hashed Refresh Token ID";
                Injected<ITokenSecurity>().Stub(s => s.HashTokenId(RefreshTokenId)).Return(hashedTokenId);

                _refreshToken = NewInstanceOf<RefreshToken>();
                Injected<IRefreshTokenRepository>().Stub(r => r.Find(hashedTokenId)).Return(_refreshToken);
            };

            static RefreshToken _result;
            static RefreshToken _refreshToken;
            const string RefreshTokenId = "Refresh Token ID";
        }
    }
}

