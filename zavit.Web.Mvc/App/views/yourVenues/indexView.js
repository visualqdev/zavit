import * as Routes from "../../routing/routes";
import { html } from "../../modules/htmlUtils/htmlUtil";

export function getView(options) {
    return `
        <div id="yourVenues" class="container">
            ${getVenues(options)}
        </div>
        `;
}

function getVenues(memberships) {

    let venuesMarkup = "";

    memberships.forEach(membership => {
        venuesMarkup += html`
            <div class="col-sm-6 yourVenue">
                <div class="yourVenueMap" data-venue-lat="${membership.Venue.Latitude}" data-venue-lng="${membership.Venue.Longitude}"></div>
                <div class="yourVenueDetails">
                    <div class="content">
                        <h3><a href="#/${Routes.yourVenue}/${membership.Venue.Id}">${membership.Venue.Name}</a></h3>
                        <address>${membership.Venue.Address}</address>
                        <span><a href="#">View players</a>  available to play here</span>
                        <button type="button" class="btn btn-primary" id="yourVenuesView">Invite others to play here</button>
                    </div>
                </div>
            </div>
        `;
    });

    venuesMarkup += `
            <div class="col-sm-6 addVenue">
                <a href="/"><i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                <div class="addDetails">
                    <div class="content">
                        <h3>Add more venues</h3>
                        <span>Making yourself available at other venues is a great way to find new competion, remember they can provide you with a guest pass!</span>
                    </div>
                </div>
            </div>
        `;

    return venuesMarkup;
}