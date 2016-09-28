import * as MapPositionAdjuster from "../map/mapPositionAdjuster";

function showNextButton(placeIndex, amountOfPlaces) {
    if (placeIndex < amountOfPlaces -1)  return `<a href="#" data-nextMarker="${placeIndex + 1}"> > </a>`;
    return "";
}

function showPreviousButton(placeIndex) {
    if (placeIndex > 0) return `<a href="#" data-prevMarker="${placeIndex -1}"> < </a>`;
    return "";
}

export function show(place, placeIndex, amountOfPlaces, mapClass) {
    $("[data-name=placeModal]").remove();

    let placeModal;
    
    if (place.Venues && place.Venues.length === 1) {
        placeModal = getSingleVenuePlaceModal(place, placeIndex, amountOfPlaces, mapClass);
    } else {
        placeModal = getPlaceModal(place, placeIndex, amountOfPlaces, mapClass);
    }

    $(placeModal).appendTo("body");
}

function getPlaceModal(place, placeIndex, amountOfPlaces, mapClass){
    const width = 300,
        height = 150,
        position = MapPositionAdjuster.adjustMapToShow({
            width,
            height,
            markerX: mapClass.map.markerPoint.x,
            markerY: mapClass.map.markerPoint.y,
            map: mapClass
        });

    const placeModal = `<div id="placeModal" data-name="placeModal" data-redirect-remove class="map-popup" style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;">            
                            <header>
                                <h3 title="${place.Name}">${place.Name}</h3>
                                <address title="${place.Address}">${place.Address}</address>
                            </header>
                            <button type="button" class="btn btn-primary" id="placeModalBeAvailable" data-marker-index="${placeIndex}" data-place-id="${place.PlaceId}">Be available to play here</button>
                            <span>                                
                                ${showPreviousButton(placeIndex)}
                                ${placeIndex + 1} of ${amountOfPlaces} 
                                ${showNextButton(placeIndex, amountOfPlaces)}                                    
                            </span>
                        </div>`;
    return placeModal;
}

function getSingleVenuePlaceModal(place, placeIndex, amountOfPlaces, mapClass){
    const width = 300,
        height = 150,
        venue = place.Venues[0],
        position = MapPositionAdjuster.adjustMapToShow({
            width,
            height,
            markerX: mapClass.map.markerPoint.x,
            markerY: mapClass.map.markerPoint.y,
            map: mapClass
        });

    const placeModal = `<div id="placeModal" data-name="placeModal" class="map-popup" style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;">            
                            <header>                   
                                <h3 title="${place.Name}">${place.Name}</h3>
                                <address title="${place.Address}">${place.Address}</address>
                            </header>
                            ${singleVenueNameHeading(place.Name, venue.Name)}
                            <button type="button" class="btn btn-primary" id="placeModalBeAvailable" data-marker-index="${placeIndex}" data-place-id="${place.PlaceId}" data-venue-id="${venue.Id}">Be available to play here</button>
                            <span>                                
                                ${showPreviousButton(placeIndex)}
                                ${placeIndex + 1} of ${amountOfPlaces} 
                                ${showNextButton(placeIndex, amountOfPlaces)}                                    
                            </span>
                        </div>`;
    return placeModal;
}

function singleVenueNameHeading(placeName, venueName) {
    if (!venueName || placeName === venueName) {
        return "";
    }

    return `<h4>${venueName}</h4>`;
}