"use strict";

describe("loginController", function () {
    describe("when loggin in", function () {
        var loginController,
            loginModal;
        beforeEach(function() {
            loginModal = jasmine.createSpyObj("loginModal", ["show"]);
            spyOn($, "loginModal").and.returnValue(loginModal);
            loginController = $.loginController();
        });

        afterEach(function() {
            $.loginModal.and.callThrough();
        });

        it("should display the login form modal", function () {
            loginController.login();
            expect(loginModal.show).toHaveBeenCalled();
        });
    });
});