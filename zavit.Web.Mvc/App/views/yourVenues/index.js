export function getView(options) {
    return `
        <div id="yourVenues" class="container">
            <div id="yourVenuesHeading">
                <h2>Your venues</h2>
            </div>
            ${getVenues(options.venues)}
            <div id="yourVenuesAddMoreContainer">
                <a href="">Add more venues</a>
            </div>
        </div>
        `;
}

function getVenues(venues) {
    let venuesMarkup = "";

    venues.forEach(venue => {
        venuesMarkup += `
            <div class="row-fluid yourVenue">
                <h3>${venue.Name}</h3>
                <address>${venue.Address}</address>
                <a href="#">${venue.NumberOfPlayers} players</a><span> available to play here</span>
                <div>
                    <button type="button" class="btn btn-primary" id="yourVenuesView">Invite others to play here</button>
                </div>
            </div>
            `;
    });

    return venuesMarkup;
}