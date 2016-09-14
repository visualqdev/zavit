import * as VenueService from "./venueService";

export function show(options = {}) {
    const width = 300,
        height = 250,
        positionX = `${options.markerX - (width / 2)}px`,
        positionY = `${options.markerY - (height - 5)}px`,
        placeId = options.placeId || null,
        modal = $(`<div id="joinVenueModal" style="width:${width}px; height:${height}px; left:${positionX}; top:${positionY};"></div>`);

    $("#joinVenueModal").remove();
    modal.appendTo("#home");    

    VenueService.getVenueAtPlace(placeId)
        .then(venue => showVenue(venue));
}

function showVenue(venue) {
    const venueContainer = $(`
        <div id="joinVenueContainer">
            <h3 title="${venue.Name}">${venue.Name}</h3>
            <address title=${venue.Address}>${venue.Address}</address>
            <div class="control-group">
                <h4>Activities</h4>
                <div class="controls span2">
                    ${activityCheckboxes(venue.Activities)}
                </div>
            </div>
        </div>`);
    venueContainer.appendTo("#joinVenueModal");
}

function activityCheckboxes(activities) {
    let activitiesMarkup = "";
    activities.forEach(activity => {
        activitiesMarkup += ` 
            <div>
                <label><input type="checkbox" name="venueActivities" value="${activity.Id}">${activity.Name}</label>
            </div>`;
    });
    return activitiesMarkup;
}