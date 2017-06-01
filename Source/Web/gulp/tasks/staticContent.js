import gulp from "gulp";
import config from "../config";

export function staticContentPipeline(stream) {
    stream.pipe(gulp.dest(config.paths.outputDir));
    return stream;
}

gulp.task("staticContent", () => {
    var stream = gulp.src(config.paths.content,{base:config.paths.sourceDir});
    staticContentPipeline(stream);
    return stream;
});
