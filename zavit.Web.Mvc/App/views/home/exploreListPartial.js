import { htmlEncode } from "../../modules/htmlUtils/htmlEncoder";

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
                    <h3 title="${htmlEncode(venue.Name)}">${htmlEncode(venue.Name)}</h3>
                    <address title="${htmlEncode(venue.Address)}">${htmlEncode(venue.Address)}</address>
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
                <label>${htmlEncode(activity.Name)}</label>
            </li>`;
    });
    return activitiesMarkup;
}