export function getView(venueMembers) {
    let venueMembersMarkup = "";

    venueMembers.forEach(venueMember => {
        venueMembersMarkup += `
            <div class="col-sm-6 col-xs-12 yourVenueMember">
                <div class="memberImage">
                </div>
                <div class="memberDetails">
                    <h3>${venueMember.DisplayName}</h3>
                </div>
            </div>

            <div class="col-sm-6 yourVenueMember">
                <div class="memberImage"></div>
                <div class="memberDetails">
                    <div class="content">
                       <h3>${venueMember.DisplayName}</h3>
                    </div>
                </div>
            </div>
            `;
    });

    return venueMembersMarkup;
}
