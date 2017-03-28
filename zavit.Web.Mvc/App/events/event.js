export function pageLoaded(path, title) {
    dataLayer.push({
        "event": "VirtualPageview",
        "virtualPageURL": path,
        "virtualPageTitle": title
    });
}