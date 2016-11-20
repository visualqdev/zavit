import * as ExternalAccountService from "../modules/account/externalAccountService";
import * as Progress from "../modules/loading/progress";

export function processExternalLogin(options = {}) {
    const externalAccessToken = options.externalAccessToken || null,
        provider = options.provider || null,
        externalUsername = options.externalUsername || null,
        externalEmail = options.externalEmail || null,
        hasLocalAccount = options.hasLocalAccount || false;

    Progress.start();

    ExternalAccountService.processExternalLogin({
        externalAccessToken,
        provider,
        externalUsername,
        externalEmail,
        hasLocalAccount
    })
    .then(() => {
            Progress.done();
            window.location.href = "/";
    });
}