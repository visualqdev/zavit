(function() {
    crossroads.addRoute("/", function () {
        $.homeController().explore();
    });

    crossroads.addRoute("/login", function () {
        $.loginController().login();
    });

    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash);
    hasher.init();
}());