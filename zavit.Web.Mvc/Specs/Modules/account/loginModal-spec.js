"use strict";

describe("loginModal", function () {
    var loginModal = $.loginModal({});
    var form;

    describe("when asked to show", function () {
        beforeEach(function () {
            form = jasmine.createSpyObj("form", ["modal"]);
            loginModal.form = form;
            loginModal.show();
        });

        it("should display the login modal form", function () {
            expect(form.modal).toHaveBeenCalledWith("show");
        });
    });
});