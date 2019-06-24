const gulp = require('gulp');
const ts = require('gulp-typescript');
const nodemon = require('gulp-nodemon');

const typescriptSources = 'src/**/*.ts';
gulp.task('build', () => {
	const project = ts.createProject('tsconfig.json');
	return gulp.src(typescriptSources)
	.pipe(project())
	.pipe(gulp.dest('dist'));
});
gulp.task('watch', () => {
	return gulp.watch(typescriptSources, gulp.series('build'));
});
gulp.task('server', () => {
	nodemon({
		script: 'dist/server.js',
	})
});
gulp.task('debug', gulp.parallel('watch', 'server'));