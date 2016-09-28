export function adjustMapToShow(options) {
    const topOffset = 55,
        width = options.width,
        height = options.height;
    if (options.map.pannedBy && options.venueModal) {
        options.map.panBy(options.map.pannedBy.x, -options.map.pannedBy.y);
    }
    let positionX = options.markerX - (width / 2),
        positionY = options.markerY - (height);
    
    if (positionY < topOffset) {
        const yMovingBy = topOffset - positionY;
        positionY = topOffset;
        if (!options.venueModal) options.map.setPannedBy(0, -1 * yMovingBy);
        options.map.panBy(0, -1 * yMovingBy);
    }

    return {
        X: positionX,
        Y: positionY
    };
}