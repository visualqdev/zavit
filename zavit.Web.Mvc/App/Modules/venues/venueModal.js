import * as VenueService from "./venueService";
import * as MapPositionAdjuster from "../map/mapPositionAdjuster";
import * as ActivityClient from "../activities/activityClient";

export function show(options = {}) {
    const width = 300,
        height = 260,
        placeId = options.placeId || null,
        venueId = options.venueId;

    const position = MapPositionAdjuster.adjustMapToShow({
        width,
        height,
        markerX: options.markerX,
        markerY: options.markerY,
        map: options.map,
        venueModal:true
    });

    const modal = $(`<div id="joinVenueModal" class="map-popup" data-name="placeModal" data-redirect-remove style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;"></div>`);
    
    $("[data-name=placeModal]").remove();
    
    modal.appendTo("body");

    if (venueId) {
        VenueService.getVenue(venueId)
            .then(venue => showVenue(venue, placeId));
    } else {
        VenueService.getVenueAtPlace(placeId)
           .then(venue => showVenue(venue, placeId));
    }
}

function showVenue(venue, placeId) {
    const venueContainer = $(`
        <div id="joinVenueContainer">
            <header>
                <h3 title="${venue.Name}">${venue.Name}</h3>
                <address title="${venue.Address}">${venue.Address}</address>            
            </header>
            <h4>I am available for some</h4>
            <div class="control-group" id="joinVenueChooseActivities">                
                ${getActivitiesMarkup(venue.Activities)}
                <div class="span4 joinVenueOtherActivitiesContainer">
                    <a href="" id="joinVenueOtherActivities">Other activity</a>
                </div>
            </div>
            <button type="button" class="btn btn-primary" id="joinVenueSubmit">Make me available here</button>
        </div>`);

    $("#joinVenueModal").html("");
    venueContainer.appendTo("#joinVenueModal");

    venueContainer.find("#joinVenueSubmit").click((e) => {
        e.preventDefault();
        joinVenue(placeId, venue.Id);
    });

    venueContainer.find("#joinVenueOtherActivities").click((e) => {
        e.preventDefault();
        loadAllActivities();
    });
}

function getActivitiesMarkup(activities) {
    //const activitiesInColumn = Math.ceil((activities.length) / 2);
    //const leftColumnActivities = activities;
    //const rightColumnActivities = leftColumnActivities.splice(activitiesInColumn, leftColumnActivities.length);

    return `
        <ul class="list-group row">
            ${activityCheckboxes(activities)}
        </ul>`;
}

function activityCheckboxes(activities) {
    let activitiesMarkup = "";
    activities.forEach(activity => {
        activitiesMarkup += ` 
            <li class="list-group-item col-xs-12 col-sm-6">
                <label class="checkbox">
                    <input type="checkbox" name="venueActivities" value="${activity.Id}">${activity.Name}
                </label>
            </li>`;
    });
    return activitiesMarkup;
}

function joinVenue(placeId, venueId) {
    const activitiCheckboxes = $("#joinVenueContainer [name='venueActivities']:checked");
    const activities = activitiCheckboxes
        .map((index, checkbox) => $(checkbox).val())
        .get();

    if (!activities || activities.length === 0) {
        $("#joinVenueChooseActivities").addClass("warning-border");
        return;
    }

    VenueService.joinVenue({
        activities,
        placeId,
        venueId
    });
}

function loadAllActivities() {
    ActivityClient
        .getAllActivities()
        .then(activities => {
            $("#joinVenueChooseActivities").html(getActivitiesMarkup(activities));
        });
}