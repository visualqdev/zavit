import * as VenueMembershipClient from "./venueMembershipClient";
import * as VenueService from "./venueService";

export function getVenueMembership(options) {
    if (options.venueId) {
        return VenueMembershipClient.getMembershipForVenue(options.venueId);
    }

    if (options.publicPlaceId) {
        return VenueMembershipClient.getMembershipForPlace(options.publicPlaceId);
    }
}

export function getVenueMembers(options, skip, take) {
    if (options.venueId) {
        VenueMembershipClient.getVenueMembers(options.venueId, skip, take);
    }
}