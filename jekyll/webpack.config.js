const path = require('path');

module.exports = {
  devtool: 'source-map',
  entry: path.resolve(__dirname, '_scripts/main.js'),
  mode: 'production',
  output: {
    filename: 'script.bundle.js',
    path: path.resolve(__dirname, 'assets/js/'),
  },
};
