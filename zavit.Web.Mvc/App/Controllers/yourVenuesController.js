import * as IndexView from "../views/yourVenues/index";

export function index() {
    const container = $("#mainContent");
    container.empty();

    const view = IndexView.getView({ venues: [
    {
        Name: "Tennis venue", 
        Address: "Somewhere around you",
        NumberOfPlayers: 4
    }] });

    container.append(view);
}