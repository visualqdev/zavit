export function getView(membership) {
    return `
        <div id="yourVenue" class="container">
            <div id="yourVenueHeading">
                <h2 id="mainHeading">${membership.Venue.Name}</h2>
            </div>
            ${venueDetails(membership.Venue)}
            ${venueActivities(membership.Venue.Activities, membership.Activities)}
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

function venueActivities(availableActivities, memberActivities) {
    const memberActivityIds = memberActivities.map((activity) => activity.Id);
    

    return `
        <div class="yourVenueActivities">
            <h3>Activities available to you</h3>
            <ul class="list-group row">
                ${activityCheckboxes(availableActivities, memberActivityIds)}
            </ul>
        </div>
        `;
}

function activityCheckboxes(activities, memberActivityIds) {
    let activitiesMarkup = "";

    activities.forEach(activity => {
        const isChecked = memberActivityIds.includes(activity.Id) ? "checked" : "";

        activitiesMarkup += `
            <li class="list-group-item col-xs-6 col-sm-4 col-md-3">
                <label class="checkbox">
                    <input type="checkbox" name="venueActivities" value="${activity.Id}" ${isChecked}>${activity.Name}
                </label>
            </li>
            `;
    });
    return activitiesMarkup;
}

function venueMembers(parameters) {
    return `
        <div id="yourVenueMembers">
        </div>
        `;
}