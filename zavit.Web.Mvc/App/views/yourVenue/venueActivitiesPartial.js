import { html } from "../../modules/htmlUtils/htmlUtil";

export function getView(venueActivities, allOtherActivities, memberActivities) {
    
    const memberActivityIds = memberActivities.map((activity) => activity.Id);
    return `
        <div class="yourVenueActivities col-md-8" id="exTab1">
            <div class="tab-content clearfix">
                <ul class="nav nav-pills">
                    ${getTabs(memberActivities.length > 0)}
                </ul>
                <ul class="list-group row tab-pane active" id="1a">
                    ${activityCheckboxes(venueActivities, memberActivityIds)}
                </ul>
                <ul class="list-group row tab-pane" id="2a">
                    ${activityCheckboxes(allOtherActivities, memberActivityIds)}
                </ul>   
            </div>
        </div>
        <div class="col-md-4 selectedActivities">
            
            <div class="inner">
                <h3>Your activities</h3>
                <ul>${memberActivityList(memberActivities)}</ul>
            </div>
        </div>
        `;
}

function getTabs(hasMemberActivities) {
    if (hasMemberActivities)
        return `<li class="active"><a href="#1a" data-toggle="tab">Member activities</a><li>
                        <li><a href="#2a" data-toggle="tab">All other activites</a></li>`;
    return `<li class="active"><a href="#1a" data-toggle="tab">Suggested activities</a><li>
                        <li><a href="#2a" data-toggle="tab">All other activites</a></li>`;
}

export function updateActivities(memberActivities) {
    $("div.selectedActivities").find('ul').replaceWith('<ul>' + memberActivityList(memberActivities) + '</ul>');
}

function activityCheckboxes(activities, memberActivityIds) {
    let activitiesMarkup = "";

    activities.forEach(activity => {
        const isChecked = memberActivityIds.includes(activity.Id) ? "checked" : "";

        activitiesMarkup += html`
            <li class="list-group-item col-xs-6 col-sm-4 col-md-3">
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