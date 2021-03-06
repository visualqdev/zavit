﻿import * as VenueMembershipClient from "./venueMembershipClient";
import * as VenueService from "./venueService";
import * as ActivitiesClient from "../activities/activityClient";

export function getVenueMembership(options) {
    let membershipPromise = null;
    if (options.venueId) {
        membershipPromise = VenueMembershipClient.getMembershipForVenue(options.venueId);
    }

    if (options.publicPlaceId) {
        membershipPromise = VenueMembershipClient.getMembershipForPlace(options.publicPlaceId);
    }

    if (membershipPromise == null) return;
    
    function sortActivities(membership, allActivities) {
        var venueActivityIds = membership.Venue.Activities.map(activity => { return activity.Id }),
            allActivitiesMinusVenueActivities = allActivities.filter(activity => {return venueActivityIds.indexOf(activity.Id) === -1});
        return allActivitiesMinusVenueActivities;
    }

    return new Promise((resolve, reject) => {
        Promise.all([membershipPromise, ActivitiesClient.getAllActivities()])  
        .then(results => resolve({
          membershipDetails : results[0],
          allOtherActivities : sortActivities(results[0], results[1])
        }))
        .catch(reject);
    });
}

export function getVenueMembers(options, skip, take) {
    if (options.venueId) {
        return VenueMembershipClient.getVenueMembers(options.venueId, skip, take);
    }
}