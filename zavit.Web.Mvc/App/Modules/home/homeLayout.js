const buttonWidth = 100,
    buttonHeight = 30,
    buttonMargin = 15;

export function showLoadPlacesHere(options) {
    const exploreMap = $("#exploreMap");

    const loadPlacesButton = `
        <span id='loadPlacesHere' class='btn btn-primary' style='width:${buttonWidth}px; height:${buttonHeight}px; position:absolute;'>
            Load places
        </span>`;
    exploreMap.append(loadPlacesButton);
    positionButton();

    $(window).resize(positionButton);
    exploreMap.on("click", "#loadPlacesHere", function() {
        options.onLoadPlaces();
    });
}

function positionButton() {
    const exploreMap = $("#exploreMap"),
        button = $("#loadPlacesHere"),
        positionLeft = exploreMap.width() - buttonWidth - buttonMargin;

    button.css({ top: buttonMargin, left: positionLeft });
}