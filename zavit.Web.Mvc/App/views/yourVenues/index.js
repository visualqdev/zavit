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
                <div class="yourVenueMap" data-venue-lat="${membership.Venue.Lat}" data-venue-lng="${membership.Venue.Lng}">
                </div>
                <div class="yourVenueDetails">
                    <h3>${membership.Venue.Name}</h3>
                    <address>${membership.Venue.Address}</address>
                    <a href="#">${membership.Venue.NumberOfPlayers} players</a><span> available to play here</span>
                    <div>
                        <button type="button" class="btn btn-primary" id="yourVenuesView">Invite others to play here</button>
                    </div>
                </div>
            </div>
            `;
    });

    return venuesMarkup;
}