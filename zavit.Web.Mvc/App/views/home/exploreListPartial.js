export function getView(venues) {
    return `
        <ul class="exploreListVenues">
            ${getPlaceListItems(venues)}
        </ul>
        `;
}

function getPlaceListItems(venues) {
    let venuesMarkup = "";

    venues.forEach((venue, index) => {
        venuesMarkup += `
            <li class="exploreListVenue" data-marker-index="${index}">
                <header>
                    <h3 title="${venue.Name}">${venue.Name}</h3>
                    <address title="${venue.Address}">${venue.Address}</address>
                    <ul class="list-group row venueActivities">
                        ${activityListItems(venue.Activities)}
                    </ul>
                </header>
            </li>`;
    });

    return venuesMarkup;
}

function activityListItems(activities) {
    let activitiesMarkup = "";
    activities.forEach(activity => {
        activitiesMarkup += ` 
            <li class="list-group-item col-xs-12 col-sm-6">
                <label>${activity.Name}</label>
            </li>`;
    });
    return activitiesMarkup;
}