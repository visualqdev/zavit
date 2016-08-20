export function storeObject(key, value) {
    localStorage.setItem(key, JSON.stringify(value));
}

export function getObject(key, value) {
    const valueString = localStorage.getItem(key);
    return JSON.parse(valueString);
}

export function removeItem(key) {
    localStorage.removeItem(key);
}