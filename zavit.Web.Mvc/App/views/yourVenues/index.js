export function getView(options) {
    return `
        <div id="yourVenues" class="container">
            <div id="yourVenuesHeading">
                <h2>Your venues</h2>
            </div>
            ${getVenues(options)}
            <div id="yourVenuesAddMoreContainer">
                <a href="">Add more venues</a>
            </div>
        </div>
        `;
}

function getVenues(memberships) {
    let venuesMarkup = "";

    memberships.forEach(membership => {
        venuesMarkup += `
            <div class="row-fluid yourVenue">
                <div class="yourVenueMap col-md-4" data-venue-lat="${membership.Venue.Latitude}" data-venue-lng="${membership.Venue.Longitude}">
                </div>
                <div class="yourVenueDetails col-md-4">
                    <h3>${membership.Venue.Name}</h3>
                    <address>${membership.Venue.Address}</address>
                    <a href="#">View players</a><span> available to play here</span>
                    <div>
                        <button type="button" class="btn btn-primary" id="yourVenuesView">Invite others to play here</button>
                    </div>
                </div>
            </div>
            `;
    });

    return venuesMarkup;
}