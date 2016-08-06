"use strict";
import * as LoginModal from "../app/modules/account/loginModal";
import * as LoginController from "../app/controllers/loginController";

describe("loginController", function () {
    describe("when loggin in", function () {
        beforeEach(function() {
            spyOn(LoginModal, "show");
            LoginController.login();
        });

        it("should display the login form modal", function () {
            expect(LoginModal.show).toHaveBeenCalled();
           // expect(LoginModal.show).toHaveBeenCalled();
        });
    });
});