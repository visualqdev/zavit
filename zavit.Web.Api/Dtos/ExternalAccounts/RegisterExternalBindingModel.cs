﻿namespace zavit.Web.Api.Dtos.ExternalAccounts
{
    public class RegisterExternalBindingModel
    {
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string ExternalAccessToken { get; set; }
    }
}