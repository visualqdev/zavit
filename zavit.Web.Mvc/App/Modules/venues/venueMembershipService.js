import * as VenueMembershipClient from "./venueMembershipClient";
import * as VenueService from "./venueService";

export function getVenueMembership(options) {
    if (options.venueId) {
        return VenueMembershipClient.getVenueMembership(options.venueId);
    }

    if (options.publicPlaceId) {
        return new Promise((resolve, reject) => {
            VenueService
                .getVenueAtPlace(options.publicPlaceId)
                .then(venue => resolve({
                    Activities: [],
                    Venue: venue
                }));
        });
    }
}

export function getVenueMembers(options, skip, take) {
    if (options.venueId) {
        VenueMembershipClient.getVenueMembers(options.venueId, skip, take);
    }
}