"use strict";

import * as LoginController from "../app/controllers/loginController";

describe("loginController", function () {
    describe("when loggin in", function () {
        var loginModal;
        beforeEach(function() {
           
            loginModal = {
                show :function(name) {
                    
                }
            }

            spyOn(loginModal, "show");

            loginModal.show("matt");

        });
        
        it("should display the login form modal", function () {
            var result = LoginController.login("matt");
            expect(loginModal.show).toHaveBeenCalled();
            expect(loginModal.show).toHaveBeenCalledWith("matt");
            expect(result).toEqual("matt_A");
        });
    });
});