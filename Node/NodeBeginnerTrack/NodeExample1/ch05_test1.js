const http = require('http');

const server = http.createServer();

// const port = 3000;
// server.listen(port, () => {
//     console.log('웹 서버가 시작되었습니다. : %d', port);
// });

const host = '192.168.0.11';
const port = 3000;
server.listen(port, host , '50000', () => {
    console.log('웹 서버가 시작되었습니다. : %s:%d', host, port);
});