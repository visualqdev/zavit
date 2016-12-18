using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Recipients;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.MessageRecipients
{
    public class MessageRecipientDtoService : IMessageRecipientDtoService
    {
        readonly IAccountRepository _accountRepository;
        readonly IMessageRecipientDtoFactory _messageRecipientDtoFactory;
        readonly IMessageRecipientService _messageRecipientService;
        readonly IMessageRecipientCollectionDtoFactory _messageRecipientCollectionDtoFactory;
        readonly IUserContext _userContext;

        public MessageRecipientDtoService(IAccountRepository accountRepository, IMessageRecipientDtoFactory messageRecipientDtoFactory, IMessageRecipientCollectionDtoFactory messageRecipientCollectionDtoFactory, IMessageRecipientService messageRecipientService, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _messageRecipientDtoFactory = messageRecipientDtoFactory;
            _messageRecipientCollectionDtoFactory = messageRecipientCollectionDtoFactory;
            _messageRecipientService = messageRecipientService;
            _userContext = userContext;
        }

        public IEnumerable<MessageRecipientDto> GetRecipients(IEnumerable<int> accountIds)
        {
            var accounts = _accountRepository.GetAccounts(accountIds);
            return accounts.Select(a => _messageRecipientDtoFactory.CreateItem(a));
        }

        public MessageRecipientCollectionDto Suggest(string searchTerm, int skip, int take)
        {
            var recipients = _messageRecipientService.SuggestRecipients(searchTerm, skip, take, _userContext.Account);
            return _messageRecipientCollectionDtoFactory.CreateItem(recipients);
        }
    }
}