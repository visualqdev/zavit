import * as MapPositionAdjuster from "../map/mapPositionAdjuster";

function showNextButton(placeIndex, amountOfPlaces) {
    if (placeIndex < amountOfPlaces -1)  return `<a href="#" data-nextMarker="${placeIndex + 1}"> > </a>`;
    return "";
}

function showPreviousButton(placeIndex) {
    if (placeIndex > 0) return `<a href="#" data-prevMarker="${placeIndex -1}"> < </a>`;
    return "";
}

export function modal(place, placeIndex, amountOfPlaces, map) {
    const width = 300,
        height = 150;

    $("#placeModal").remove();

    const position = MapPositionAdjuster.adjustMapToShow({
        width,
        height,
        markerX: map.markerPoint.x,
        markerY: map.markerPoint.y,
        map: map
    });

    const placeModal = `<div id="placeModal" class="map-popup" style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;">            
                            <h3 title="${place.Name}">${place.Name}</h3>
                            <address title="${place.Address}">${place.Address}</address>
                            <button type="button" class="btn btn-primary" id="placeModalBeAvailable" data-marker-index="${placeIndex}" data-place-id="${place.PlaceId}">Be available to play here</button>
                            <span>                                
                                ${showPreviousButton(placeIndex)}
                                ${placeIndex + 1} of ${amountOfPlaces} 
                                ${showNextButton(placeIndex, amountOfPlaces)}                                    
                            </span>
                        </div>`;
    return placeModal;
}