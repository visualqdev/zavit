import * as Test from './modules/test';

(function() {

    var DEFAULT_HASH = 'home';

    crossroads.addRoute('/',() => { loadNames() });
    crossroads.addRoute('about', () => { loadAddress()});

    //crossroads.routed.add(console.log, console); //log all routes

    //only required if you want to set a default value
    //if(! hasher.getHash()){
    //    hasher.setHash(DEFAULT_HASH);
    //}

    function parseHash(newHash, oldHash){
        // second parameter of crossroads.parse() is the "defaultArguments" and should be an array
        // so we ignore the "oldHash" argument to avoid issues.
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash); //parse initial hash
    hasher.changed.add(parseHash); //parse hash changes

    hasher.init(); //start listening for hash changes


    function capitalise(word) {
        return word.charAt(0).toUpperCase() + word.slice(1);
    }

    $("h1").on("click", () => alert("hey"));

    function loadNames() {

        clear();

        const names = Test.listEm([{ 'name': "george", 'age':10 },{ 'name': "stan", 'age':20 }, {'name': "bill", 'age':33}, {'name':"bob", 'age':17}, {'name':"ben", 'age':12}], 'name');

        names.forEach(name => {
            let listTemplate = `<li> ${capitalise(name)} </li>`;
            $("#names").append(listTemplate);
        });
    }

    function clear() {
        $("#home").empty();
        $("#names").empty();
    }
    
    function loadAddress() {
        clear();
        
        const name = "matt";
        const addressLine1 = "39 slade end";
        const addressLine2 = "Theydon Bois";

        const template = `<h2>${capitalise(name)}</h2>
                        <p>${addressLine1}</p>
                        <p>${addressLine2}</p>`;

        $("#home").append(template);
    }
    
}())