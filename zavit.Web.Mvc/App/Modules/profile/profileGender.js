export const allGenderOptions = [
    "Not specified",
    "Male",
    "Female"
];

export function displayName(index) {
    return allGenderOptions[index];
}

export function index(genderDisplaName) {
    const indexOfItem = allGenderOptions.indexOf(genderDisplaName);
    return indexOfItem;
}