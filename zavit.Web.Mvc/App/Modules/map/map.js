export class Map {
    
    constructor(options = {zoom:12}) {
        this.zoom = options.zoom;
    }

    initialise(lat, lng) {
        var area = new google.maps.LatLng(lat, lng);

        var gMap = new google.maps.Map(document.getElementById('map'), {
            center: area,
            zoom: this.zoom,
            scrollwheel: false
        });
    }
}
