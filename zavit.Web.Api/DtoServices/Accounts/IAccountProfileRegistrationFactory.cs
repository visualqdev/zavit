﻿using zavit.Domain.Profiles.Registration;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public interface IAccountProfileRegistrationFactory
    {
        IAccountProfileRegistration CreateItem(AccountRegistrationDto accountRegistrationDto);
    }
}