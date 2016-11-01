import * as VenueActivitiesPartial from "./venueActivitiesPartial";

export function getView(membership) {
    return `
        <div id="yourVenue" class="container">
            <div id="yourVenueHeading">
                <h2 id="mainHeading">${membership.Venue.Name}</h2>
            </div>
            ${venueDetails(membership.Venue)}
            ${VenueActivitiesPartial.getView(membership.Venue.Activities, membership.Activities)}
            ${venueMembers()}
        </div>
        `;
}

function venueDetails(venue) {
    return `
            <div class="yourVenueDetails">
                <div class="yourVenueMap" data-venue-lat="${venue.Latitude}" data-venue-lng="${venue.Longitude}"></div>
                <div class="pull-left">
                    <address>${venue.Address}</address>
                </div>
            </div>            
        `;
}

function venueMembers(parameters) {
    return `
        <div id="yourVenueMembers">
        </div>
        `;
}