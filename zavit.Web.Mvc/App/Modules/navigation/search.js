import * as PlaceSuggester from "../map/placeSuggester";

export function initialise(options) {
    $("a[data-input-id]").on("click", function(e) {
        e.preventDefault();

        const selectedOption = $(this);

        const searchShortLabel = selectedOption.attr("data-short-label");

        $("#search_concept").text(searchShortLabel);
        $("[name='searchInputControl']").hide();
        $("#venue_search_input").val("");

        const inputId = selectedOption.attr("data-input-id");
        $(`#${inputId}`).show();
    });

    $("#searchButton").on("click", function(e) {
        e.preventDefault();

        const inputValue = $("#venue_search_input").val();
        options.searchForVenue(inputValue, options);
    });

    $("#venue_search_input").on("keydown", function (e) {
        if (e.which === 13) {
            e.preventDefault();
            $("#searchButton").click();
        }
    });

    PlaceSuggester.initialise({
        onNewAreaSelected: options.sarchForArea,
        inputElementId: "area_search_input"
    });
}

