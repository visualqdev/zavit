import { html } from "../../modules/htmlUtils/htmlUtil";

export function getView(venueActivities, allOtherActivities, memberActivities) {
    
    const memberActivityIds = memberActivities.map((activity) => activity.Id);
    return `
        <div class="yourVenueActivities col-xs-12 col-sm-12 col-md-8">
            <h3>Choose an activity</h3>
            <div class="inner">
                <ul class="list-group row">
                    ${activityCheckboxes(venueActivities, memberActivityIds)}
                    ${activityCheckboxes(allOtherActivities, memberActivityIds)}
                </ul>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-4 selectedActivities">
            <h3>Your activities</h3>
            <div class="inner">
                <ul>${memberActivityList(memberActivities)}</ul>
            </div>
        </div>
        `;
}

export function updateActivities(memberActivities) {
    $("div.selectedActivities").find('ul').replaceWith('<ul>' + memberActivityList(memberActivities) + '</ul>');
}

function activityCheckboxes(activities, memberActivityIds) {
    let activitiesMarkup = "";

    activities.forEach(activity => {
        const isChecked = memberActivityIds.includes(activity.Id) ? "checked" : "";

        activitiesMarkup += html`
            <li class="list-group-item col-xs-6 col-sm-6 col-md-6 col-lg-4">
                <label class="checkbox">
                    <input type="checkbox" name="venueActivities" value="${activity.Id}" ${isChecked}>${activity.Name}
                </label>
            </li>
            `;
    });
    return activitiesMarkup;
}

function memberActivityList(activities) {
   
    let activitiesMarkup = "";
    activities.forEach(activity => {
        activitiesMarkup += html`<li> ${activity.Name} <a href="#" data-id="${activity.Id}">remove</a></li>`;
    });
    return activitiesMarkup;
}