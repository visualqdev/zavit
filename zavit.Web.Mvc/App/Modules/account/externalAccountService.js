import * as ApiSettings from "../settings/apiSettings";
import * as ExternalAccountClient from "./externalAccountClient";
import * as AccountService from "./accountService";

export function facebookLogin() {
    const url = `${ApiSettings.apiUrl}api/ExternalAccounts/ExternalLogin?provider=Facebook&response_type=token&client_id=1&redirect_uri=${ApiSettings.apiUrl}`;
    window.location.href = url;
}

export function googleLogin() {
    const url = `${ApiSettings.apiUrl}api/ExternalAccounts/ExternalLogin?provider=Google&response_type=token&client_id=1&redirect_uri=${ApiSettings.apiUrl}`;
    window.location.href = url;
}

export function processExternalLogin(options) {
    if (options.hasLocalAccount.toLowerCase() === "true") {
        return processLogin(options);
    } else {
        return processRegistration(options);
    }
}

function processLogin(options) {
    return new Promise((resolve, reject) => {
        ExternalAccountClient
            .obtainLocalAccessToken(options.externalAccessToken, options.provider)
            .then((data) => {
                authenticationSuccess(data);
                resolve();
            })
            .catch((err) => {
                reject(err.responseText);
            });
    });
}

function processRegistration(options) {
    return new Promise((resolve, reject) => {
        ExternalAccountClient
            .registerExternal(options.externalAccessToken, options.provider, options.externalUsername, options.externalEmail)
            .then((data) => {
                authenticationSuccess(data);
                resolve();
            })
            .catch((err) => {
                if (err.status === 409) {
                    processLogin(options)
                        .then(() => resolve())
                        .catch(loginErr => reject(loginErr));
                } else {
                    reject(err.responseText);
                }
            });
    });
}

function authenticationSuccess(tokenData) {
    AccountService.authenticationSuccess(tokenData);
}