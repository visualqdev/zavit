﻿import * as VenueService from "./venueService";
import * as MapPositionAdjuster from "../map/mapPositionAdjuster";

export function show(options = {}) {
    const width = 300,
        height = 260,
        placeId = options.placeId || null;

    const position = MapPositionAdjuster.adjustMapToShow({
        width,
        height,
        markerX: options.markerX,
        markerY: options.markerY,
        map: options.map
    });

    const modal = $(`<div id="joinVenueModal" class="map-popup" data-name="placeModal" style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;"></div>`);
    $("#joinVenueModal").remove();
    modal.appendTo("#home");
    
    VenueService.getVenueAtPlace(placeId)
        .then(venue => showVenue(venue));
}

function showVenue(venue) {
    const activitiesInColumn = Math.ceil((venue.Activities.length) / 2);
    const leftColumnActivities = venue.Activities;
    const rightColumnActivities = leftColumnActivities.splice(activitiesInColumn, leftColumnActivities.length);

    const venueContainer = $(`
        <div id="joinVenueContainer">
            <header>
                <h3 title="${venue.Name}">${venue.Name}</h3>
                <address title=${venue.Address}>${venue.Address}</address>            
            </header>
            <h4>I am available for some</h4>
            <div class="control-group">                
                <div class="controls span2">
                    ${activityCheckboxes(leftColumnActivities)}
                </div>
                <div class="controls span2">
                    ${activityCheckboxes(rightColumnActivities)}                    
                </div>
                <a id="joinVenueOtherActivities">Some other activity</a>
            </div>
            <button type="button" class="btn btn-primary" id="joinVenueSubmit">Make me available here</button>
        </div>`);
    $("#joinVenueModal").html("");
    venueContainer.appendTo("#joinVenueModal");
}

function activityCheckboxes(activities) {
    let activitiesMarkup = "";
    activities.forEach(activity => {
        activitiesMarkup += ` 
            <label class="checkbox">
                <input type="checkbox" name="venueActivities" value="${activity.Id}">${activity.Name}
            </label>
            `;
    });
    return activitiesMarkup;
}