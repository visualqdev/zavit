export function getView(places) {
    return `
        <ul>
            ${getPlaceListItems(places)}
        </ul>
        `;
}

function getPlaceListItems(places) {
    let placesMarkup = "";

    places.forEach(place => {
        placesMarkup += `
            <li>
                <header>
                    <h3 title="${place.Name}">${place.Name}</h3>
                    <address title="${place.Address}">${place.Address}</address>
                </header>
            </li>`;
    });

    return placesMarkup;
}