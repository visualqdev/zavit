import * as Routes from "../../routing/routes";
import { htmlEncode } from "../../modules/htmlUtils/htmlEncoder";

export function getView(venueMembers) {
    let venueMembersMarkup = "";

    if (venueMembers.length === 0) {
        return noMembers();
    }

    $.each(venueMembers, (index, venueMember) => {
        const spacingClass = index % 2 === 0 ? "yourVenueMemberRightPadding" : "yourVenueMemberLeftPadding";

        venueMembersMarkup += `
            <div class="col-sm-6 yourVenueMember ${spacingClass}">
                <div class="memberImage">
                    ${memberImage(venueMember)}
                </div>
                <div class="memberDetails">
                    <div class="content">
                       <h3>${htmlEncode(venueMember.DisplayName)}</h3>
                       ${activitiesMarkup(venueMember.Activities)}
                    </div>
                    <div class="memberActions">
                        <a class="btn btn-primary" href="/#/${Routes.messageInbox}?accounts=${venueMember.AccountId}">Arrange</a>
                    </div>
                </div>
            </div>
            `;
    });

    return venueMembersMarkup;
}

function activitiesMarkup(activities) {
    let activitiesListItems = "";

    activities.forEach(activity => {
        activitiesListItems += `
            <li>${htmlEncode(activity.Name)}</li>
            `;
    });

    return `
        <ul>
            ${activitiesListItems}
        </ul>
        `;
}

function memberImage(venueMember) {
    if (venueMember.ProfileImageUrl == null) {
        return `
        <div><i class="fa fa-user" aria-hidden="true"></i></div>`;
    }

    return`
        <div class="profileImageHolder" style='background: url("${venueMember.ProfileImageUrl}") 50% 50% no-repeat;'></div>`;
    
}

function noMembers() {
    return `
        <div class="col-sm-12 venueNoMembersMessage">
            Currently there are no other members who have joined this venue yet. Select your activities above to indicate what are you interested in.
        </div>
        `;
}
