import * as HomeController from "../controllers/homeController";
import * as Geocode from "../modules/map/geocode";


function explore(position) {
    HomeController.explore(position);
}

function searchArea($element) {
    
    $("#placeModal").remove();

    const inputValue = $element.closest("span").prev("input").val();

    if (inputValue !== "") Geocode.getGeoCodeByAddress(inputValue, explore);
}

function registerEvents() {

    $("a[data-type]").on("click", function(e) {
        e.preventDefault();
        $("#search_input").val("");
        const searchType = $(this).attr("data-type"),
             searchTypePlaceholderText = $(this).attr("data-placeHolderText");

        $("#search_concept").text(searchType);
        $("#search_input").attr('placeholder', searchTypePlaceholderText);
    });

    $("#search .btn").on("click", function(e) {

        e.preventDefault();

        const searchConcept = $(this).closest("div.input-group").find("#search_concept").text();

        if (searchConcept === "Area") searchArea($(this));
    });
}

export function initialise() {
    registerEvents();
}

