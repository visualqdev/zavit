"use strict";

describe("loginController", function () {
    describe("when loggin in", function () {
        var loginController,
            loginModal;
        beforeEach(function() {
            loginModal = { 
                show :function() {}
            }

            loginController = $.loginController({ loginModal: loginModal });

            spyOn(loginModal, "show");
        });
        
        it("should display the login form modal", function () {
            loginController.login();
            expect(loginModal.show).toHaveBeenCalled();
        });
    });
});