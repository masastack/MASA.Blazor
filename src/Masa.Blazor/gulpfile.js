var gulp = require("gulp");
var concat = require("gulp-concat");
var cssimport = require("gulp-cssimport");
const cleanCSS = require("gulp-clean-css");
var rename = require("gulp-rename");

gulp.task("min", function () {
  return gulp
    .src("wwwroot/css/masa-blazor.css")
    .pipe(cssimport())
    .pipe(rename({ suffix: ".min" }))
    .pipe(cleanCSS())
    .pipe(gulp.dest("wwwroot/css"));
});

gulp.task("default", gulp.parallel("min"));
