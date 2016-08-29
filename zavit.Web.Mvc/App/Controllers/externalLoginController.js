import * as ExternalAccountService from "../modules/account/externalAccountService";

export function processExternalLogin(options = {}) {
    const externalAccessToken = options.externalAccessToken || null,
        provider = options.provider || null,
        externalUsername = options.externalUsername || null,
        externalEmail = options.externalEmail || null,
        hasLocalAccount = options.hasLocalAccount || false;

    ExternalAccountService.processExternalLogin({
        externalAccessToken,
        provider,
        externalUsername,
        externalEmail,
        hasLocalAccount
    })
    .then(() => {
            window.location.href = "/";
        });
}