"use strict";

var test_module = require('../test');

describe("#test", function () {
    it("returns the values of a object determined by the second parameter", function () {
        var test = test_module.listEm([{ "name": "bert", "age": 12 }, { "name": "george", "age": 8 }], "name");
        expect(test).toEqual(['bert', 'george']);
    });
});