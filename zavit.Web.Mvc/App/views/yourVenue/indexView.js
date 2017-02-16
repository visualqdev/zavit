import * as VenueActivitiesPartial from "./venueActivitiesPartial";
import { html } from "../../modules/htmlUtils/htmlUtil";
import { htmlEncode } from "../../modules/htmlUtils/htmlEncoder";

export function getView(membership, allOtherActivities) {
    return `
        <div id="yourVenue" class="container">
            <div id="yourVenueHeading">
                <h2 id="mainHeading">${htmlEncode(membership.Venue.Name)}</h2>
            </div>
            ${venueDetails(membership.Venue)}
            ${VenueActivitiesPartial.getView(membership.Venue.Activities, allOtherActivities, membership.Activities)}
            ${venueMembers()}
        </div>
        `;
}

function venueDetails(venue) {

    return html`
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