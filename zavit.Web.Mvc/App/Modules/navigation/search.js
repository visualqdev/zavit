import * as Geocode from "../map/geocode";


function searchByArea(inputValue, places) {

    function load(position) {

        places.map.map.markers = [];
        places.removeMarkers();
        places.clearPlaceInfo();

        places.name = "";
        places.map.map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
        places.map.position = position;
        places.getPlaces();
    }

    if (inputValue !== "") Geocode.getGeoCodeByAddress(inputValue, load);
}

function searchByVenueName(inputValue, places) {

    places.map.map.markers = [];
    places.removeMarkers();
    places.clearPlaceInfo();

    places.name = inputValue;
    places.getPlaces();
}

function registerEvents(places) {

    $("a[data-type]").on("click", function(e) {
        e.preventDefault();
        
        $("#search_input").val("");

        const searchType = $(this).attr("data-type"),
             searchTypePlaceholderText = $(this).attr("data-placeHolderText");

        $("#search_concept").text(searchType);
        $("#search_input").attr('placeholder', searchTypePlaceholderText);

    });

    $("#searchButton").on("click", function(e) {
        e.preventDefault();

        const inputValue = $(this).closest("span").prev("input").val(),
            searchConcept = $(this).closest("div.input-group").find("#search_concept").text();

        if (searchConcept === "Area") searchByArea(inputValue, places);

        if (searchConcept === "Venue") searchByVenueName(inputValue, places);

    });
}

export function initialise(places) {
    registerEvents(places);
}

