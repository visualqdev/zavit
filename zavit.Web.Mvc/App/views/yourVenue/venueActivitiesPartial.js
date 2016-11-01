export function getView(availableActivities, memberActivities, isFullActivityList) {
    const memberActivityIds = memberActivities.map((activity) => activity.Id);
    
    return `
        <div class="yourVenueActivities">
            <h3>Activities available to you</h3>
            <ul class="list-group row">
                ${activityCheckboxes(availableActivities, memberActivityIds)}
            </ul>
            ${showAllActivities(isFullActivityList)}            
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

function showAllActivities(isFullActivityList) {
    if (isFullActivityList) return "";

    return `
        <div>
            <a href="#" id="yourVenueOtherActivities">All other activities</a>
        </div>
        `;
}