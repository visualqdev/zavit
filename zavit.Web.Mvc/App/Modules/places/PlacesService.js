import * as PlacesClient from "./placesClient";

export function getPlaces(options) {
    const radius = options.radius || 3000,
        name = options.name || "";

    return PlacesClient.getPlaces(
        options.map.position.coords.latitude,
        options.map.position.coords.longitude,
        radius, name);
}