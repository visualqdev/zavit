"use strict";

// Include gulp
var gulp = require("gulp"),
    jasmine = require("gulp-jasmine"),
    browserify = require("browserify"),
    babelify = require("babelify"),
    source = require("vinyl-source-stream"),
    gutil = require("gulp-util"),
    less = require('gulp-less'),
    path = require('path'),
    watchLess = require('gulp-watch-less'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify');

gulp.task("concat", function () {
    return gulp.src("./app/**/*.js")
        .pipe(concat("app-bundled.js"))
        //.pipe(uglify())
        .pipe(gulp.dest("./"));
});

gulp.task("watch", function () {
    gulp.watch(["./app/**/*.js"], ["concat"]);
});

gulp.task('less-watch', ['less'], function() {
    return gulp.src('./CSS/styles.less')
        .pipe(watchLess('./CSS/styles.less', function() {
            gulp.start('less');
        }));
});

gulp.task('less', function() {
    return gulp.src('./CSS/styles.less')
        .pipe(less())
        .pipe(gulp.dest('./CSS/production'));
});

gulp.task("default", [ "less-watch", "watch", "concat"]);