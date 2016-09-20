import * as Storage from "../storage/storage";

const urlStorageKey = "post_login_redirect";

export function processRedirect() {
    const url = Storage.getString(urlStorageKey);
    Storage.removeItem(urlStorageKey);
    if (url) {
        window.location.replace(url);
        return true;
    }
    return false;
}

export function storeRedirectUrl(url) {
    Storage.storeString(urlStorageKey, url);
}