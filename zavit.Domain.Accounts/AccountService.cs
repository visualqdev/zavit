﻿using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts
{
    public class AccountService : IAccountService
    {
        readonly IAccountRegistrationResultFactory _accountRegistrationResultFactory;
        readonly IAccountCreator _accountCreator;
        readonly IAccountRepository _accountRepository;
        readonly IEnumerable<IAccountRegistrationValidator> _accountRegistrationValidators;
        readonly ILogger _logger;
        readonly IVerifyEmailMailer _verifyEmailMailer;
        readonly IDateTime _dateTime;

        public AccountService(IAccountRegistrationResultFactory accountRegistrationResultFactory, IAccountCreator accountCreator, IAccountRepository accountRepository, IEnumerable<IAccountRegistrationValidator> accountRegistrationValidators, ILogger logger, IVerifyEmailMailer verifyEmailMailer, IDateTime dateTime)
        {
            _accountRegistrationResultFactory = accountRegistrationResultFactory;
            _accountCreator = accountCreator;
            _accountRepository = accountRepository;
            _accountRegistrationValidators = accountRegistrationValidators;
            _logger = logger;
            _verifyEmailMailer = verifyEmailMailer;
            _dateTime = dateTime;
        }

        public async Task<AccountRegistrationResult> Register(IAccountRegistration accountRegistration)
        {
            foreach (var accountRegistrationValidator in _accountRegistrationValidators)
            {
                var validationResult = accountRegistrationValidator.Validate(accountRegistration);
                if (validationResult != null) return validationResult;
            }

            var account = await _accountCreator.Create(accountRegistration);
            _accountRepository.Save(account);
            _logger.Info($"Account registered Id:{account.Id} Username:{account.Username}");

            await _verifyEmailMailer.SendMail(account);

            var result = _accountRegistrationResultFactory.CreateSuccessful(account);
            return result;
        }

        public bool VerifyAccount(string verificationCode)
        {
            var account = _accountRepository.GetByVerificationCode(verificationCode);
            if (account == null) return false;

            account.Verify(_dateTime);
            _accountRepository.Save(account);

            return true;
        }
    }
}