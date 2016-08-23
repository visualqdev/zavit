export class Map {

    constructor(options = {}) 
    {
        this.zoom = options.zoom || 12;
        this.executeWhenMapFullyLoaded = options.executeWhenMapFullyLoaded || null;
        this.marker = null;
        this.markerPoint = null;
        this.overlay = null;
    }
   
    initialise(lat, lng) {
        
        const area = new google.maps.LatLng(lat, lng);

        const map = new google.maps.Map(document.getElementById("map"), {
            center: area,
            zoom: this.zoom,
            scrollwheel: true
        });
        
        const projectionChanged = new Promise(function(resolve) {
            google.maps.event.addListener(map,
                "projection_changed",
                function() {
                    this.overlay = new google.maps.OverlayView();
                    this.overlay.draw = function() {};
                    this.overlay.setMap(map);
                    resolve();
                }
           );
        });

        const mapHasLoaded = new Promise(function(resolve) {
            google.maps.event.addListenerOnce(map, "idle", () => resolve());
        });

        Promise.all([projectionChanged, mapHasLoaded])
            .then(value => {
                this.executeWhenMapFullyLoaded();
                this.map = map;
            }, 
            reason => {
                console.log(reason);
            }
       );
    }

    addMarker(latLng) {
        this.marker = new google.maps.Marker({
            map: this.map,
            position: latLng
        });
    }

    addPlaceMarkerClickEvent(marker, callback, place) {
        google.maps.event.addListener(marker, "click", function(e) {
            this.map.setCenter(e.latLng);
            this.map.markerPoint = this.map.overlay.getProjection().fromLatLngToContainerPixel(e.latLng);
            callback(place, this.map);
        });
    }

}
