import * as AccountClient from "./accountClient";
import * as Storage from "../storage/storage";

const tokenStorageKey = "userAuthenticationToken";

export function register(displayName, email, password) {
    return new Promise((resolve, reject) => {
        AccountClient
            .register(displayName, email, password)
            .then(() => logIn(email, password))
            .then(() => resolve())
            .catch((error) => reject(error));
    });
}

export function logIn(email, password) {
    return new Promise((resolve, reject) => {
        AccountClient
            .getAuthenticationTokens(email, password)
            .then(
                (data) => {
                    authenticationSuccess(data);
                    resolve();
                })
            .catch(
                (error) => {
                    authenticationError(error);
                    reject(error);
                });
    });
}

export function logOut() {
    Storage.removeItem(tokenStorageKey);
}

export function currentUserAccount() {
    const tokenData = Storage.getObject(tokenStorageKey);
    if (tokenData) {
        return {
            email: tokenData.userName,
            displayName: tokenData.displayName,
            accessToken: tokenData.access_token
        };
    }
}

export function refreshUserAccount() {
    return new Promise((resolve, reject) => {
        const tokenData = Storage.getObject(tokenStorageKey);

        if (tokenData) {
            AccountClient
                .refreshAccessToken(tokenData.refresh_token)
                .then(
                    (data) => {
                        authenticationSuccess(data);
                        resolve();
                    })
                .catch(reject);
        } else {
            reject();
        }
    });
}

export function authenticationSuccess(tokenData) {
    Storage.storeObject(tokenStorageKey, tokenData);
}

function authenticationError(error) {
    debugger;
}