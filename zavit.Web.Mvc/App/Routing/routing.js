(function() {
    crossroads.addRoute("/", () => {
    });

    crossroads.addRoute("/login", () => {
        $.loginController().login();
    });

    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash);
    hasher.init();
})();