import * as AccountService from "../account/accountService";

export function send(options) {
    const currentAccount = AccountService.currentUserAccount();

    if (currentAccount) {
        return new Promise((resolve, reject) => {
            const ajaxOptions = getFirstAttemptOptions(options, currentAccount, resolve, reject);
            $.ajax(ajaxOptions);
        });
    }
}

function getFirstAttemptOptions(baseOptions, account, resolve, reject) {
    let ajaxOptions = {
        beforeSend: (request) => request.setRequestHeader("Authorization", `Bearer ${account.accessToken}`),
        contentType: "application/json; charset=utf-8",
        success: resolve,
        error: (xhr, textStatus, errorThrown) => {
            if (xhr.status === 401) {
                AccountService
                    .refreshUserAccount()
                    .then(() => makeSecondAttempt(baseOptions))
                    .then(resolve)
                    .catch(reject);
            }
        }
    };

    Object.assign(ajaxOptions, baseOptions);
    return ajaxOptions;
}

function makeSecondAttempt(baseOptions) {
    return new Promise((resolve, reject) => {
        const account = AccountService.currentUserAccount();
        let ajaxOptions = {
            beforeSend: (request) => request.setRequestHeader("Authorization", `Bearer ${account.accessToken}`),
            contentType: "application/json; charset=utf-8",
            success: resolve,
            error: reject
        };

        Object.assign(ajaxOptions, baseOptions);
        $.ajax(ajaxOptions);
    });
}