const fs = require('fs');
const http = require('http');
const server = http.createServer((req, res) => {
    const instream = fs.createReadStream('./output.txt');
    instream.pipe(res);
})

server.listen(7001, '127.0.0.1');