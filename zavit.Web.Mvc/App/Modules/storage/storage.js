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