const PROXY_CONFIG = [
  {
    context: ['/api', '/restwebforms/api'],
    target: 'http://localhost:5000',
    secure: false,
    logLevel: 'error',
    pathRewrite: {
      '^/restwebforms': ''
    }
  }
];

module.exports = PROXY_CONFIG;
