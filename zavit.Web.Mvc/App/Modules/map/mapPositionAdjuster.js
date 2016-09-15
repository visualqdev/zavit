export function adjustMapToShow(options) {
    const topOffset = 55,
        width = options.width,
        height = options.height;

    let positionX = options.markerX - (width / 2),
        positionY = options.markerY - (height + 45);
    
    if (positionY < topOffset) {
        var yMovingBy = topOffset - positionY;
        positionY = topOffset;
        options.map.panBy(0, -1 * yMovingBy);
    }

    return {
        X: positionX,
        Y: positionY
    };
}