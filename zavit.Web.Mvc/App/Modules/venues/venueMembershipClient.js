import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function joinVenue(venueId, activities) {
    const postVenueMembershipUrl = `${ApiSettings.apiUrl}api/venuememberships`;
    const activityDtos = activities.map((activityId) => {
        return { id: activityId };
    });

    const data = {
        activities: activityDtos,
        venueId
    };

    return new Promise((resolve, reject) =>
        AuthorizedClient
            .send({
                url: postVenueMembershipUrl,
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data)
            })
            .then(resolve)
            .catch(reject));
}