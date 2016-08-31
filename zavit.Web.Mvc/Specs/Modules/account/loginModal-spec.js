"use strict";

describe("loginModal", function () {
    var loginModal,
        form;

    beforeEach(function() {
        loginModal = $.loginModal();
    });

    describe("when asked to show", function () {
        beforeEach(function () {
            form = jasmine.createSpyObj("form", ["modal", "on"]);
            loginModal.form = form;
            loginModal.show();
        });

        it("should display the login modal form", function () {
            expect(form.modal).toHaveBeenCalledWith("show");
        });

        it("should bind a click event to log in submit button", function() {
            expect(form.on).toHaveBeenCalledWith("#loginSubmit", "click", loginModal.submitLogin);
        });
    });

    describe("when asked to show after being shown before", function () {
        var existingModal,
            getElementSpy;

        beforeEach(function () {
            existingModal = {
                modal: function (){}
            };

            spyOn(existingModal, "modal");

            getElementSpy = spyOn(document, "getElementById");
            getElementSpy.and.callFake(function (expression) {
                if (expression === "loginModal")
                    return existingModal;
            });

            loginModal.show();
        });

        afterEach(function() {
            getElementSpy.and.callThrough();
        });

        it("should toggle the exisitng modal", function() {
            expect(existingModal.modal).toHaveBeenCalledWith("toggle");
        });
    });

    describe("when asked to submit login", function () {
        var accountService,
            emailValue = "Email value",
            passwordValue = "Password value";

        beforeEach(function () {
            form = jasmine.createSpyObj("form", ["modal", "on", "find"]);
            loginModal.form = form;

            form.find.and.callFake(function(selector) {
                if (selector === "#loginEmail") {
                    return { val: function () { return emailValue } };
                }
                if (selector === "#loginPassword") {
                    return { val: function () { return passwordValue } };
                }
            });

            accountService = jasmine.createSpyObj("accountService", ["logIn"]);

            spyOn($, "accountService").and.returnValue(accountService);

            loginModal.submitLogin();
        });

        afterEach(function() {
            $.accountService.and.callThrough();
        });

        it("should call the login service with inputted username and password", function() {
            expect(accountService.logIn).toHaveBeenCalledWith(emailValue, passwordValue);
        });
    });
});