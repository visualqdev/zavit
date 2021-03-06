﻿using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles.Registration;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public interface IAccountProfileRegistrationFactory
    {
        IAccountRegistration CreateItem(AccountRegistrationDto accountRegistrationDto);
    }
}