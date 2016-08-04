"use strict";

var homeController = require('../app/controllers/homeController');

describe("#homeController", function () {
    it("should retun name", function () {
        var test = homeController.explore();
        expect(test).toEqual("dog");
    });
});