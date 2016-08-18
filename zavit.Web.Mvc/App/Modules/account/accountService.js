import * as AccountClient from "./accountClient";

export function logIn(email, password) {
    AccountClient
        .getAuthenticationTokens(email, password)
        .then(authenticationSuccess, authenticationError);
}

function authenticationSuccess(tokenData) {
    debugger;
}

function authenticationError(error) {
    debugger;
}