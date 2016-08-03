export function listEm(listOfObj, value) {
    const arr = listOfObj.map(obj => obj[value]);
    return arr;
}