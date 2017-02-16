import * as Geocode from "../map/geocode";

export function initialise(options) {
    registerEvents(options);
}

function registerEvents(options) {

    $("a[data-type]").on("click", function(e) {
        e.preventDefault();
        
        $("#search_input").val("");

        const searchType = $(this).attr("data-type"),
             searchTypePlaceholderText = $(this).attr("data-placeHolderText");

        $("#search_concept").text(searchType);
        $("#search_input").attr('placeholder', searchTypePlaceholderText);

    });

    $("#searchButton").on("click", function(e) {
        e.preventDefault();

        const inputValue = $(this).closest("span").prev("input").val(),
            searchConcept = $(this).closest("div.input-group").find("#search_concept").text();

        if (searchConcept === "Area") options.sarchForArea(inputValue);

        if (searchConcept === "Venue") options.searchForVenue(inputValue, options);

    });

    $("#search_input").on("keydown", function (e) {            
        if (e.which === 13) {
            e.preventDefault();
            $("#searchButton").click();
        }
    });
}

