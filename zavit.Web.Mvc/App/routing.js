(function () {
    var DEFAULT_HASH = 'home';

    crossroads.addRoute('/', () => { names.load() });
    crossroads.addRoute('about', () => { loadAddress() });

    //crossroads.routed.add(console.log, console); //log all routes

    //only required if you want to set a default value
    //if(! hasher.getHash()){
    //    hasher.setHash(DEFAULT_HASH);
    //}

    function parseHash(newHash, oldHash) {
        // second parameter of crossroads.parse() is the "defaultArguments" and should be an array
        // so we ignore the "oldHash" argument to avoid issues.
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash); //parse initial hash
    hasher.changed.add(parseHash); //parse hash changes

    hasher.init(); //start listening for hash changes

}())