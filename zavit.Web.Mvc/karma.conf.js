module.exports = function(config) {
    config.set({
        browsers: [
            //'PhantomJS',
            //'Chrome',
            'Chrome_without_security'],
        files: [
            'libraries/jquery-3.1.0.js',
            'CSS/bootstrap/js/bootstrap.min.js',
            "routing/signals.min.js",
            "routing/crossroads.min.js",
            "routing/hasher.min.js",
            'app/**/*.js',
            { pattern: 'test-context.js', watched: false }
        ],
        frameworks: ['jasmine'],
        preprocessors: {
            'test-context.js': ['webpack']
        },
        webpack: {
            module: {
                loaders: [
                    { test: /\.js/, exclude: /node_modules/, loader: 'babel-loader' }
                ]
            },
            watch: true
        },
        webpackServer: {
            noInfo: true
        },
        customLaunchers: {
            Chrome_without_security: {
                base: 'Chrome',
                flags: ['--disable-web-security']
            }
        }
    });
};