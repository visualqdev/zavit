import * as Progress from "../loading/progress";

export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 5000;
    }

    getPlaces() {
        const latitude = this.map.position.coords.latitude,
            longitude = this.map.position.coords.longitude,
            url = `/api/places?latitude=${latitude}&longitude=${longitude}&radius=${this.radius}`;
        Progress.start();
        fetch(url).then(response => {  return response.json(); }).then(json => this.addPlaces(json));
    }

    addPlaces(json) {
        json.forEach(place => this.addPlaceMarker(place));
        Progress.done();
    }

    addPlaceMarker(place) {
        this.map.addMarker({ lat: place.Latitude, lng: place.Longitude });
        this.map.addPlaceMarkerClickEvent(this.map.marker, this.showPlaceInfo, place, this.map.overlay);
    }

    showPlaceInfo(place, map) {

        const left = (map.markerPoint.x + 20) + "px",
            topPoint = (map.markerPoint.y - 60) + "px";

        $("#mapModal").remove();

        const mapModal = `<div id="mapModal" style="left:${left}; top:${topPoint};"> <h3>${place.Name}</h3></div>`;

        $(mapModal).appendTo("#home");
    }
}