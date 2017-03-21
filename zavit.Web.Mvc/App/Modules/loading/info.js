﻿export function provide(html) {
    $("body").append(`<div id="infoPopUp">${html}</div>`);

    const $infoPopUp = $("#infoPopUp"),
        adjustMarginLeft = ($infoPopUp.width() / 2),
        adjustMarginTop = ($infoPopUp.height() / 2);

    $infoPopUp.css({ 'margin-left': `-${adjustMarginLeft}px`, 'margin-top': `-${adjustMarginTop}px`, visibility: "visible" });

    setTimeout(() => $infoPopUp.fadeOut("200", function() {
         $infoPopUp.remove();
    }), 3000);
}