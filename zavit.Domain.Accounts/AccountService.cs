using System.Collections.Generic;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;

namespace zavit.Domain.Accounts
{
    public class AccountService : IAccountService
    {
        readonly IAccountRegistrationResultFactory _accountRegistrationResultFactory;
        readonly IAccountCreator _accountCreator;
        readonly IAccountRepository _accountRepository;
        readonly IEnumerable<IAccountRegistrationValidator> _accountRegistrationValidators;

        public AccountService(IAccountRegistrationResultFactory accountRegistrationResultFactory, IAccountCreator accountCreator, IAccountRepository accountRepository, IEnumerable<IAccountRegistrationValidator> accountRegistrationValidators)
        {
            _accountRegistrationResultFactory = accountRegistrationResultFactory;
            _accountCreator = accountCreator;
            _accountRepository = accountRepository;
            _accountRegistrationValidators = accountRegistrationValidators;
        }

        public AccountRegistrationResult Register(IAccountRegistration accountRegistration)
        {
            foreach (var accountRegistrationValidator in _accountRegistrationValidators)
            {
                var validationResult = accountRegistrationValidator.Validate(accountRegistration);
                if (validationResult != null) return validationResult;
            }

            var account = _accountCreator.Create(accountRegistration);
            _accountRepository.Save(account);

            var result = _accountRegistrationResultFactory.CreateSuccessful(account);
            return result;
        }
    }
}