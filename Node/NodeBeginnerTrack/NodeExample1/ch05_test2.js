const http = require('http');

const server = http.createServer();

const port = 3000;
server.listen(port, () => {
    console.log('웹 서버가 시작되었습니다. : %d', port);
});

server.on('connection', (socket) => {   // 'connection' 이벤트 발생 시(on)
    let addr = socket.address();
    console.log('클라이언트가 접속했습니다. : %s:%d', addr.address, addr.port);
});

server.on('request', (req, res) => {
    console.log('클라이언트 요청이 들어왔습니다.');
    console.dir(req);
});

server.on('close', () => {
    console.log('서버가 종료됩니다.');
});