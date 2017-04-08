import * as Storage from "../storage/storage";

const urlStorageKey = "post_login_redirect";

export function processRedirect() {
    const url = Storage.getSessionString(urlStorageKey);
    Storage.removeSessionItem(urlStorageKey);
    if (url) {
        window.location.replace(url);
        return true;
    }
    return false;
}

export function storeRedirectUrl(url) {
    Storage.storeSessionString(urlStorageKey, url);
}

export function clearRedirects() {
    Storage.removeSessionItem(urlStorageKey);
}