import * as AccountClient from "./accountClient";
import * as Storage from "../storage/storage";

const tokenStorageKey = "userAuthenticationToken";

export function register(displayName, email, password) {
    return new Promise((resolve, reject) => {
        AccountClient
            .register(displayName, email, password)
            .then(() => { return logIn(email, password); })
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
            email: tokenData.userName
        };
    }
}

function authenticationSuccess(tokenData) {
    Storage.storeObject(tokenStorageKey, tokenData);
}

function authenticationError(error) {
    debugger;
}