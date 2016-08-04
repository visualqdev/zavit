import * as Test from './modules/test';

(function() {

    


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