"use strict";

describe("homeController", function () {

    describe("when exploring", function () {

        var homeController;

        beforeEach(function () {

            spyOn(navigator.geolocation, "getCurrentPosition").and.callFake(function() {
                debugger;
            });
            homeController = $.homeController();
        });


        it("should ask navigator to for the users current location", function () {
            homeController.explore();
            expect(navigator.geolocation.getCurrentPosition).toHaveBeenCalledWith(homeController.centerMapAtLocation);
        });
    });
});