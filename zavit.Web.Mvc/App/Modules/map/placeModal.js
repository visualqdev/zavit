function showNextButton(placeIndex, amountOfPlaces) {
    if (placeIndex < amountOfPlaces -1)  return `<a href="#" data-nextMarker="${placeIndex + 1}"> > </a>`;
    return "";
}

function showPreviousButton(placeIndex) {
    if (placeIndex > 0) return `<a href="#" data-prevMarker="${placeIndex -1}"> < </a>`;
    return "";
}

export function modal(place, placeIndex, amountOfPlaces, map) {

    const left = (map.markerPoint.x -150) + "px",
            top = (map.markerPoint.y - 195) + "px";

    $("#placeModal").remove();

    const placeModal = `<div id="placeModal" style="left:${left}; top:${top};">
            
                                <h3 title="${place.Name}">${place.Name}</h3>

                                <address title="${place.Address}">${place.Address}</address>

                                <button type="button" class="btn btn-primary">Be available to play here</button>

                                <span>
                                
                                    ${showPreviousButton(placeIndex)}

                                    ${placeIndex + 1} of ${amountOfPlaces} 

                                    ${showNextButton(placeIndex, amountOfPlaces)}
                                    
                                </span>

                            </div>`;
    return placeModal;
}