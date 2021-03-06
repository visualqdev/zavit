﻿using System.IO;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public class ExternalAccountRegistration : IAccountRegistration
    {
        public string Provider { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password => null;
        public AccountType AccountType => AccountType.External;
        public Gender Gender { get; set; }
        public Stream ProfileImage { get; set; }
    }
}