export function storeObject(key, value) {
    storeString(key, JSON.stringify(value));
}

export function getObject(key) {
    const valueString = getString(key);
    return JSON.parse(valueString);
}

export function storeString(key, value) {
    localStorage.setItem(key, value);
}

export function getString(key) {
    return localStorage.getItem(key);
}

export function removeItem(key) {
    localStorage.removeItem(key);
}

export function storeSessionObject(key, value) {
    storeSessionString(key, JSON.stringify(value));
}

export function getSessionObject(key) {
    const valueString = getSessionString(key);
    return JSON.parse(valueString);
}

export function storeSessionString(key, value) {
    sessionStorage.setItem(key, value);
}

export function getSessionString(key) {
    return sessionStorage.getItem(key);
}

export function removeSessionItem(key) {
    sessionStorage.removeItem(key);
}