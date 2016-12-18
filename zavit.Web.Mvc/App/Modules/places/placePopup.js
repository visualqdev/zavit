import * as MainContent from "../../layout/mainContent";
import * as Routes from "../../routing/routes";
import * as MapPositionAdjuster from "../map/mapPositionAdjuster";
import { html } from "../htmlUtils/htmlUtil";

export function show(options) {
    $("[data-name=placeModal]").remove();

    if (!MainContent.isOnPage(Routes.home)) return;

    const width = 300,
        height = 150,
        position = MapPositionAdjuster.adjustMapToShow({
            width,
            height,
            markerX: options.placesMap.map.markerPoint.x,
            markerY: options.placesMap.map.markerPoint.y,
            map: options.placesMap
        });

    const placeModal = html`
        <div id="placeModal" data-name="placeModal" data-redirect-remove class="map-popup" style="width:${width}px; height:${height}px; left:${position.X}px; top:${position.Y}px;">                        
            <button type="button" class="btn btn-primary" id="placeModalBeAvailable" data-marker-index="${options.placeIndex}" data-place-id="${options.place.PublicPlaceId}" data-venue-id="${options.place.Id}">Be available to play here</button>            
        </div>
        `;

    $(placeModal).appendTo("#exploreMap");
}