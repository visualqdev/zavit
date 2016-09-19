import * as VenueClient from "./venueClient";
import * as AccountService from "../account/accountService";

export function getVenueAtPlace(placeId) {
    return new Promise((resolve, reject) => {
        VenueClient.getVenueAtPlace(placeId)
            .then(venue => resolve(venue));
    });
}

export function joinVenue(options) {
    VenueClient
        .addVenue(options.placeId, "test venue name", options.activities)
        .then((venue) => letUserJoinVenue(venue.Id, options.activities))
        .catch();
}

function letUserJoinVenue(venueId, activities) {
    UserVenueClient
        .joinVenue(venueId, activities)
        .then(() => window.href = "/");
}