using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoServices.MessageRecipients
{
    public class MessageRecipientDtoService : IMessageRecipientDtoService
    {
        readonly IAccountRepository _accountRepository;
        readonly IMessageRecipientDtoFactory _messageRecipientDtoFactory;

        public MessageRecipientDtoService(IAccountRepository accountRepository, IMessageRecipientDtoFactory messageRecipientDtoFactory)
        {
            _accountRepository = accountRepository;
            _messageRecipientDtoFactory = messageRecipientDtoFactory;
        }

        public IEnumerable<MessageRecipientDto> GetRecipients(IEnumerable<int> accountIds)
        {
            var accounts = _accountRepository.GetAccounts(accountIds);
            return accounts.Select(a => _messageRecipientDtoFactory.CreateItem(a));
        }
    }
}