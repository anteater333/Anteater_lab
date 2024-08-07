const http = require('http');
const fs = require('fs');

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
    
    const filename = 'Icon.png';
    const infile = fs.createReadStream(filename, {flags: 'r'});
    
    let filelength = 0;
    let curlength = 0;

    fs.stat(filename, (err, stats) => {
        filelength = stats.size;
    });

    res.writeHead(200, {"Content-Type": "image/png"});

    infile.on('readable', () => {
        let chunk;
        while (null !== (chunk = infile.read())) {
            console.log('읽어 들인 데이터 크기 : %d 바이트', chunk.length);
            curlength += chunk.length;
            res.write(chunk, 'utf8', err => {
                console.log('파일 부분 쓰기 완료 : %d/%d', curlength, filelength);
                if (curlength >= filelength) {
                    res.end();
                }
            });
        }
    });
});

server.on('close', () => {
    console.log('서버가 종료됩니다.');
});