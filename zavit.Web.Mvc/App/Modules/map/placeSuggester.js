export function initialise(options) {
    const input = document.getElementById(options.inputElementId);

    const autocomplete = new google.maps.places.Autocomplete(input);

    autocomplete.addListener('place_changed', function() {
        const place = autocomplete.getPlace();

        if (!place.geometry) return;

        $(`#${options.inputElementId}`).val("");
        options.onNewAreaSelected({
            coords: {
                latitude: place.geometry.location.lat(),
                longitude: place.geometry.location.lng()
            }
        });
    });

}